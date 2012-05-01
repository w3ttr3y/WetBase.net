using System;
using System.Diagnostics.Contracts;

namespace Wet.Dal
{
    /// <summary>
    /// Removes an object from the persistence layer.
    /// </summary>
    /// <typeparam name="TKey">The key which identifies the object to remove.</typeparam>
    [ContractClass(typeof(RemoveContract<>))]
    public interface IRemove<in TKey> : ISave
    {
        /// <summary>
        /// Removes the object specified by the key.
        /// </summary>
        /// <param name="key">The key which identifies the object to remove.</param>
        void Remove(TKey key);
    }

    [ContractClassFor(typeof(IRemove<>))]
    internal abstract class RemoveContract<TKey> : IRemove<TKey>
    {
        public void Remove(TKey key)
        {
            Contract.Requires(key != null);
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}