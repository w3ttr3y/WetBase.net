using System;
using System.Diagnostics.Contracts;

namespace Wet.Dal
{
    /// <summary>
    /// Interface for adding objects to the persistence layer.
    /// </summary>
    /// <typeparam name="T">The type of object to add.</typeparam>
    [ContractClass(typeof(AddContract<>))]
    public interface IAdd<in T> : ISave
    {
        /// <summary>
        /// A method to add an object to the persistence layer
        /// </summary>
        /// <param name="entity">The object to add.</param>
        /// <returns>A reference to this interface; allows for easily valling Save</returns>
        IAdd<T> Add(T entity);
    }

    [ContractClassFor(typeof(IAdd<>))]
    internal abstract class AddContract<T> : IAdd<T>
    {
        public IAdd<T> Add(T entity)
        {
            Contract.Requires(entity != null);
            Contract.Ensures(Contract.Result<IAdd<T>>() == this);
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}