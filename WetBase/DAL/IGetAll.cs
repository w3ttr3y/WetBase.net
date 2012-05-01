using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Wet.Dal
{
    /// <summary>
    /// An interface for retreiving all objects, subject to filtering via the provided function.
    /// </summary>
    /// <typeparam name="T">The type of object to retrieve.</typeparam>
    [ContractClass(typeof(GetAllContract<>))]
    public interface IGetAll<T>
    {
        /// <summary>
        /// A method to retrieve all objects from the persistence layer, subject to filtering via the filter function.
        /// </summary>
        /// <remarks>
        /// The filter function changes the return type from IQueryable to IEnumerable so that it can perofmr filtering on the server.
        /// </remarks>
        /// <param name="filter">A function which can perform filtering.</param>
        /// <returns></returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        IEnumerable<T> GetAll(Func<IQueryable<T>, IEnumerable<T>> filter);
    }

    /// <summary>
    /// Contract class for the IGetAll interface
    /// </summary>
    /// <typeparam name="T">The type of object to retrieve.</typeparam>
    [ContractClassFor(typeof(IGetAll<>))]
    internal abstract class GetAllContract<T> : IGetAll<T>
    {
        /// <summary>
        /// A method to retrieve all objects from the persistence layer, subject to filtering via the filter function.
        /// </summary>
        /// <remarks>
        /// The filter function changes the return type from IQueryable to IEnumerable so that it can perofmr filtering on the server.
        /// </remarks>
        /// <param name="filter">A function which can perform filtering.</param>
        /// <returns></returns>
        public IEnumerable<T> GetAll(Func<IQueryable<T>, IEnumerable<T>> filter)
        {
            Contract.Requires(filter != null);
            Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);
            throw new NotImplementedException();
        }
    }
}