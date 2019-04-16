using TestingDemo.Common;

namespace TestingDemo.Core.Infrastructure
{
    //-----------------------------------------------------------------------------------------
    /// <summary>
    /// Extension methods for GenericResult.
    /// </summary>
    public static class GenericResultExtensions
    {
        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// To the error result.
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <typeparam name="TDest">The type of the dest.</typeparam>
        /// <param name="genericResult">The generic result.</param>
        /// <returns>A generic result of type TDest.</returns>
        public static GenericResult<TDest> ToErrorResult<TSource, TDest>(this GenericResult<TSource> genericResult)
        {
            return new GenericResult<TDest>(genericResult.ErrorMessage);
        }

        //-----------------------------------------------------------------------------------------
        /// <summary>
        /// Ensures the success result.
        /// </summary>
        /// <param name="result">The result.</param>
        public static void EnsureSuccessResult(this GenericResult result)
        {
            if (result.HasError)
                throw new GenericResultException(result);
        }
    }
}
