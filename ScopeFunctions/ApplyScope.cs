namespace ScopeFunctions
{
    public static class ApplyScope
    {
        /// <summary>
        /// Performs an action on a the caller if it is not null,
        /// then returns the caller.
        /// </summary>
        /// <typeparam name="T">The caller's type.</typeparam>
        /// <param name="caller">The object invoking the function.</param>
        /// <param name="action">The action to perform on the caller.</param>
        /// <returns>The caller.</returns>
        public static T Apply<T>(this T caller, Action<T> action)
        {
            if (caller is not null) action(caller);
            return caller;
        }

        /// <summary>
        /// Performs the first action on the caller if the caller is not null,
        /// performs the second cation if the caller is null.
        /// </summary>
        /// <typeparam name="T">The caller's type.</typeparam>
        /// <param name="caller">The invoking object.</param>
        /// <param name="nonNullAction">The action to perform if the caller is not null.</param>
        /// <param name="nullAction">The action to perform if the caller is null.</param>
        /// <returns>The caller.</returns>
        public static T Apply<T>(this T caller, Action<T> nonNullAction, Action nullAction)
        {
            if (caller is not null) nonNullAction(caller);
            else nullAction();
            return caller;
        }
    }
}