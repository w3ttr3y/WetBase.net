using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;

namespace Wet.Dal
{
    /// <summary>
    /// An interface for parsing text
    /// </summary>
    [ContractClass(typeof(IParserContract))]
    public interface IParser
    {
        /// <summary>
        /// Parses text from a StreamReader
        /// </summary>
        /// <param name="streamReader">The streamReader to use as the source</param>
        void Parse(StreamReader streamReader);
    }

    /// <summary>
    /// An interface for parsing text into objects.
    /// </summary>
    /// <typeparam name="T">The type of object to generate</typeparam>
    public interface IParser<T> : IParser, IGetAll<T>
    {
    }

    [ContractClassFor(typeof(IParser))]
    internal abstract class IParserContract : IParser
    {
        public void Parse(StreamReader streamReader)
        {
            Contract.Requires(streamReader != null);
            Contract.Requires(!streamReader.EndOfStream);
        }
    }
}