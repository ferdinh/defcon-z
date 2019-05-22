using System;

namespace DefconZ.Simulation.UnitBuilder
{
    /// <summary>
    /// The <see cref="UnitOrder"/> progress information.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class BuildProgressEventArgs : EventArgs
    {
        public int orderProgress;
        public int recruitTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildProgressEventArgs"/> class.
        /// </summary>
        /// <param name="progress">The progress.</param>
        /// <param name="recruitTime">The recruit time.</param>
        public BuildProgressEventArgs(int progress, int recruitTime)
        {
            orderProgress = progress;
            this.recruitTime = recruitTime;
        }
    }
}