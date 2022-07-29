using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScopeFunctions
{
    public static class LetScope
    {
        /// <summary>
        /// Performs a function on the caller if it is not null,
        /// then returns the result of the function.
        /// </summary>
        /// <typeparam name="T">The caller's type.</typeparam>
        /// <typeparam name="TReturn">The function's return type.</typeparam>
        /// <param name="caller">The invoking object.</param>
        /// <param name="function">The function to perform.</param>
        /// <returns>
        /// If the caller is not null, returns the result of the function.
        /// Else returns null.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the caller is a non-nullable type (see remarks).
        /// </exception>
        /// <remarks>
        /// This function is intended for use only by nullable types.
        /// There is no way to constrain this to only nullable types,
        /// so instead it throws an exception if a non-nullable type is detected.
        /// Use Let(caller, function, defaultReturn) for non-nullable types.
        /// /remarks>
        public static TReturn? Let<T, TReturn>(this T caller, Func<T, TReturn?> function)
        {
            if (caller != null) return function(caller);
            else
            {
                // JIT-compile time check, so it doesn't even have to evaluate.
                if (default(TReturn) != null) throw new InvalidOperationException(
                    "Let<T, TReturn> requires TReturn to be a nullable type.");
                else return default;
            }
        }

        /// <summary>
        /// Performs a function on the caller if it is not null,
        /// then returns the result of the function.
        /// Else returns the defaultReturn value.
        /// </summary>
        /// <typeparam name="T">The caller's type.</typeparam>
        /// <typeparam name="TReturn">The function's return type.</typeparam>
        /// <param name="caller">The invoking object.</param>
        /// <param name="function">The function to perform.</param>
        /// <param name="defaultReturn">The default value to return if the caller is null.</param>
        /// <returns>
        /// If the caller is not null, returns the result of the function.
        /// Else, returns defaultReturn.
        /// </returns>
        public static TReturn Let<T, TReturn>(this T caller, Func<T, TReturn> function, TReturn defaultReturn)
        {
            if (caller is not null) return function(caller);
            else return defaultReturn;
        }

        /// <summary>
        /// Performs nonNullFunction on the caller if the caller is not null,
        /// else performss nullFunction.
        /// Returns the result of the performed function.
        /// </summary>
        /// <typeparam name="T">The caller's type.</typeparam>
        /// <typeparam name="TReturn">The return type of the function.</typeparam>
        /// <param name="caller">The invoking object.</param>
        /// <param name="nonNullFunction">The function to call if the caller is not null.</param>
        /// <param name="nullFunction">The function to call if the caller is null.</param>
        /// <returns>
        /// If the caller is not null, returns the result of nonNullFunction.
        /// Else, returns the result of nullFunction.
        /// </returns>
        public static TReturn Let<T, TReturn>(this T caller, Func<T, TReturn> nonNullFunction, Func<T, TReturn> nullFunction)
        {
            if (caller is not null) return nonNullFunction(caller);
            else return nullFunction(caller);
        }
    }
}
