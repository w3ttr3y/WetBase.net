using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Wet;

namespace Wet
{
    /// <summary>
    /// A transformation wrapper around a Func call
    /// </summary>
    /// <typeparam name="TSource">The type of the source object</typeparam>
    /// <typeparam name="TResult">The type of object to be produced from the source.</typeparam>
    public class FuncTransformation<TSource, TResult> : ITransform<TSource, TResult>
    {
        /// <summary>
        /// The function used to perform the transformation
        /// </summary>
        private readonly Func<TSource, TResult> Transformation;

        /// <summary>
        /// Standard constructor
        /// </summary>
        /// <param name="transformation">The function to use to perform the transformation.</param>
        public FuncTransformation(Func<TSource, TResult> transformation)
        {
            Contract.Requires(transformation != null);
            Contract.Ensures(transformation == this.Transformation);
            this.Transformation = transformation;
        }

        /// <summary>
        /// Transforms the provided entity into a TResult object
        /// </summary>
        /// <param name="entity">The source object</param>
        /// <returns>The transformed object</returns>
        public TResult Transform(TSource entity)
        {
            var result = Transformation(entity);
            if (result == null)
                throw new InvalidOperationException("Failed to convert source object");
            return result;
        }

        [ContractInvariantMethod]
        private void ObjectVarient()
        {
            Contract.Invariant(Transformation != null);
        }
    }
}