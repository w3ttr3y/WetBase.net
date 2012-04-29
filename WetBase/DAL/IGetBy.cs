using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Wet.Dal
{
    /// <summary>
    /// Retrieves an object by a key.
    /// </summary>
    /// <typeparam name="T">The type of object to retrieve.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    [ContractClass(typeof(IGetByContract<,>))]
    public interface IGetBy<out T, in TKey>
    {
        /// <summary>
        /// Gets the object identified by key.
        /// </summary>
        /// <param name="key">The key to use in retrieving the object.</param>
        /// <returns>The object identified by the provided key.</returns>
        T GetBy(TKey key);
    }

    [ContractClassFor(typeof(IGetBy<,>))]
    internal abstract class IGetByContract<T, TKey> : IGetBy<T, TKey>
    {
        public T GetBy(TKey key)
        {
            Contract.Requires(key != null);
            throw new NotImplementedException();
        }
    }
}