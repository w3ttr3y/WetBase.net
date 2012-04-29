using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Wet
{
    /// <summary>
    /// Factory pattern interface.
    /// </summary>
    /// <typeparam name="T">The type of object to be created.</typeparam>
    [ContractClass(typeof(FactoryContract<>))]
    public interface IFactory<T>
    {
        /// <summary>
        /// Creates a new object of type T
        /// </summary>
        /// <returns>A new object of type T</returns>
        T Create();
    }

    [ContractClassFor(typeof(IFactory<>))]
    internal abstract class FactoryContract<T> : IFactory<T>
    {
        public T Create()
        {
            Contract.Ensures(Contract.Result<T>() != null);
            throw new NotImplementedException();
        }
    }
}