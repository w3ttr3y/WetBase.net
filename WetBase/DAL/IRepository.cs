using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wet.Dal
{
    /// <summary>
    /// A traditional repository pattern layered over the repository capabilities.
    /// </summary>
    /// <typeparam name="TKey">The type used to uniquely identify objects.</typeparam>
    /// <typeparam name="T">The type of object to create, read, update, and delete from the persistence layer.</typeparam>
    public interface IRepository<TKey, T> : IGetBy<T, TKey>, IGetAll<T>, IAdd<T>, IRemove<TKey>, IUpdate<T>
    {
    }
}