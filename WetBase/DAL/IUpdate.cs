using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Wet.Dal
{
    /// <summary>
    /// Updates an object in the persistence layer.
    /// </summary>
    /// <typeparam name="T">The type of object to update.</typeparam>
    [ContractClass(typeof(UpdateContract<>))]
    public interface IUpdate<T> : ISave
    {
        /// <summary>
        /// Updates an object in the persistence layer.
        /// </summary>
        /// <param name="entity">The object to update.</param>
        /// <returns>A reference to this object for easy commiting via save.</returns>
        IUpdate<T> Update(T entity);
    }

    [ContractClassFor(typeof(IUpdate<>))]
    internal abstract class UpdateContract<T> : IUpdate<T>
    {
        public IUpdate<T> Update(T entity)
        {
            Contract.Requires(entity != null);
            //Should require entity exists in the set, but I'm not sure how
            Contract.Ensures(Contract.Result<IUpdate<T>>() != null);
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}