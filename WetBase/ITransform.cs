using System;
using System.Diagnostics.Contracts;

namespace Wet
{
    /// <summary>
    /// An interface for standardizing objects which can perform type transformations
    /// </summary>
    /// <typeparam name="TSource">The source type</typeparam>
    /// <typeparam name="TResult">The type to convert to</typeparam>
    [ContractClass(typeof(TransformContract<,>))]
    public interface ITransform<TSource, TResult>
    {
        /// <summary>
        /// The method to convert one type to another.
        /// </summary>
        /// <param name="entity">The object to be converted</param>
        /// <returns>The converted object</returns>
        TResult Transform(TSource entity);
    }

    [ContractClassFor(typeof(ITransform<,>))]
    internal abstract class TransformContract<TSource, TResult> : ITransform<TSource, TResult>
    {
        public TResult Transform(TSource entity)
        {
            Contract.Requires(entity != null);
            Contract.Ensures(Contract.Result<TResult>() != null);
            throw new NotImplementedException("Code contract, so not implementing");
        }
    }
}