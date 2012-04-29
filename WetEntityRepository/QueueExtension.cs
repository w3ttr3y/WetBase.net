using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wet.Dal
{
    /// <summary>
    /// Extensions to queue objects
    /// </summary>
    public static class QueueExtensions
    {
        /// <summary>
        /// perform each action in a queue of actions
        /// </summary>
        /// <typeparam name="T">The type of object the action is performed on</typeparam>
        /// <param name="queue">The queue of actions</param>
        /// <param name="target">The object to perform the objects on</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "Its a necessary nesting and is actually fairly simple.")]
        public static void Execute<T>(this Queue<Action<T>> queue, T target)
        {
            if (queue == null)
                return;

            foreach (var action in queue)
            {
                if (action != null)
                    action(target);
            }
        }
    }
}