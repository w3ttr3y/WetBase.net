﻿namespace Wet.Dal
{
    /// <summary>
    /// A repository capabilitiy which commits the queued changes.
    /// </summary>
    public interface ISave
    {
        /// <summary>
        /// Commit the queued changes in a unit-of-work style pattern.
        /// </summary>
        void Save();
    }
}