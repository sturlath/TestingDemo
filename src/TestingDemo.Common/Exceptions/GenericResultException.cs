using System;
using TestingDemo.Core.Infrastructure;

namespace TestingDemo.Common
{
    //---------------------------------------------------------------------------------------------
    /// <summary>
    /// Generic Result Exception
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class GenericResultException : Exception
    {
        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericResultException"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
        public GenericResultException(GenericResult result)
            : base(result.ErrorMessage)
        {
            Result = result;
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        public GenericResult Result { get; set; }
    }

    //---------------------------------------------------------------------------------------------
    /// <summary>
    /// GenericResult Exception
    /// </summary>
    /// <typeparam name="T">Type of T</typeparam>
    /// <seealso cref="Exception" />
    public class GenericResultException<T> : Exception
    {
        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericResultException{T}"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
        public GenericResultException(GenericResult<T> result)
            : base(result.ErrorMessage)
        {
            Result = result;
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        public GenericResult<T> Result { get; set; }
    }
}
