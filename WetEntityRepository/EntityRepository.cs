using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics.Contracts;
using System.Linq;
using Wet;
using Wet.Dal;

namespace WetEntityRepository
{
    /// <summary>
    /// A repository layered over the Entity Framework
    /// </summary>
    /// <typeparam name="T">The type of object to be created, read, updated, &amp; deleted from the database.</typeparam>
    /// <typeparam name="TKey">The unique key to use when looking up objects.</typeparam>
    /// <typeparam name="TContext">The DbContext to use when interacting with the persistence layers.</typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1005:AvoidExcessiveParametersOnGenericTypes", Justification = "Saying 2 is okay, but 3 is too many is fairly arbitrary. Interfaces require two and I want to allow for a generic context, so I really feel three is best.")]
    public class EntityRepository<T, TKey, TContext> : IRepository<TKey, T>
        where T : class
        where TContext : DbContext
    {
        /// <summary>
        /// A Factory create instance on the Entity Context
        /// </summary>
        protected IFactory<TContext> ContextFactory { get; private set; }

        /// <summary>
        /// A transform to get a DbSet from a context.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        protected ITransform<TContext, DbSet<T>> ContextToDBSetTransformation { get; private set; }

        /// <summary>
        /// A list of actions to be performed
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Required nesting; its not as complicated as it looks.")]
        protected Queue<Action<TContext>> ActionQueue { get; private set; }

        /// <summary>
        /// A transform to convert from
        /// </summary>
        protected ITransform<TKey, T> Transform { get; private set; }

        /// <summary>
        /// A function to return an object based on the key.
        /// </summary>
        /// <remarks>While we default to using GetBy, a more efficient optimization can create a default object and just set the key values.</remarks>
        protected Func<TKey, T> LookupFunc { get; private set; }

        /// <summary>
        /// Standard constructor.
        /// </summary>
        /// <param name="contextFactory">A context factory to use when contexts are required</param>
        /// <param name="contextToDBSetTransform">A function to get a dbset from a context</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Its a necessary use of nesting; it may look complicated, but its really not.")]
        public EntityRepository(IFactory<TContext> contextFactory, ITransform<TContext, DbSet<T>> contextToDBSetTransform)
        {
            Contract.Requires(contextFactory != null);
            Contract.Requires(contextToDBSetTransform != null);
            Contract.Ensures(contextFactory == this.ContextFactory);
            Contract.Ensures(contextToDBSetTransform == this.ContextToDBSetTransformation);
            Contract.Ensures(LookupFunc != null);
            ActionQueue = new Queue<Action<TContext>>();
            LookupFunc = GetBy;
            this.ContextToDBSetTransformation = contextToDBSetTransform;
            this.ContextFactory = contextFactory;
        }

        /// <summary>
        /// Gets an object by the key.
        /// </summary>
        /// <param name="key">The key to use when looking up objects.</param>
        /// <returns>The object which correpsonds to the key</returns>
        public T GetBy(TKey key)
        {
            using (var context = ContextFactory.Create())
            {
                return ContextToDBSetTransformation.Transform(context).Find(key);
            }
        }

        /// <summary>
        /// Gets all objects of a type from the persistence layer (subject to filtering)
        /// </summary>
        /// <param name="filter">The filter to apply when selecting objets</param>
        /// <returns>An IEnumerable of objects which match the filter</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "Not sure how to validate transform; Interface contracts requires its not null, we ensure if it returns null we return an empty list. Can't think of anything else we could do (which could easily be my own ignorance).")]
        public IEnumerable<T> GetAll(Func<IQueryable<T>, IEnumerable<T>> filter)
        {
            using (var context = ContextFactory.Create())
            {
                return filter(ContextToDBSetTransformation.Transform(context)) ?? new List<T>();
            }
        }

        /// <summary>
        /// Queues an object to be added to the persistence layer
        /// </summary>
        /// <remarks>The Save method must be called to persist the addition.</remarks>
        /// <param name="entity">The object to add to the persistence layer</param>
        /// <returns>A refernce to itself.</returns>
        public IAdd<T> Add(T entity)
        {
            Contract.Ensures(Contract.Result<IAdd<T>>() == this);
            lock (ActionQueue)
            {
                ActionQueue.Enqueue(ctx => ContextToDBSetTransformation.Transform(ctx).Add(entity));
            }
            return this;
        }

        /// <summary>
        /// Commits the queued changes to the persistence layer
        /// </summary>
        public void Save()
        {
            lock (ActionQueue)
            {
                using (var context = ContextFactory.Create())
                {
                    ActionQueue.Execute(context);
                }
                ActionQueue.Clear();
            }
        }

        /// <summary>
        /// Removes the object which correpsonds to the provided key from the persistence layer.
        /// </summary>
        /// <param name="key">The key to use when deciding which object to remove</param>
        public void Remove(TKey key)
        {
            lock (ActionQueue)
            {
                ActionQueue.Enqueue(ctx => ContextToDBSetTransformation.Transform(ctx).Remove(LookupFunc(key)));
            }
        }

        /// <summary>
        /// Queues an object to be updated in the persistence layer.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IUpdate<T> Update(T entity)
        {
            Contract.Ensures(Contract.Result<IUpdate<T>>() == this);
            lock (ActionQueue)
            {
                ActionQueue.Enqueue(context => context.Entry(entity).State = EntityState.Modified);
            }
            return this;
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(this.ContextToDBSetTransformation != null);
            Contract.Invariant(this.ContextFactory != null);
            Contract.Invariant(this.ActionQueue != null);
        }
    }
}