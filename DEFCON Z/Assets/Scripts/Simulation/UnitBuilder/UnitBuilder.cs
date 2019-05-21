using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefconZ.Simulation.UnitBuilder
{
    /// <summary>
    /// A class that processes a Unit Order and returning it to the caller/owner.
    /// </summary>
    /// <seealso cref="UnityEngine.MonoBehaviour" />
    public class UnitBuilder : MonoBehaviour
    {
        public Queue<UnitOrder> buildQueue;
        private Clock _clock;
        private UnitOrder _currentOrder;
        private int _currentOrderProgress;

        /// <summary>
        /// Occurs when [on build start].
        /// </summary>
        public event EventHandler OnBuildStart;

        /// <summary>
        /// Occurs when [on build progress update].
        /// </summary>
        public event EventHandler<BuildProgressEventArgs> OnBuildProgressUpdate;

        /// <summary>
        /// Occurs when [on build finish].
        /// </summary>
        public event EventHandler<BuildFinishedEventArgs> OnBuildFinish;

        private void Awake()
        {
            buildQueue = new Queue<UnitOrder>();
            _clock = GameObject.FindGameObjectWithTag(nameof(GameManager)).GetComponent<Clock>();

            _clock.GameCycleElapsed += ProcessOrder;
        }

        /// <summary>
        /// Processes the order.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ProcessOrder(object sender, EventArgs e)
        {
            // Check if there is a build queue if we are not currently
            // processing an order.
            if (_currentOrder == null)
            {
                // Get the order if available.
                if (buildQueue.Count > 0)
                {
                    // Start processing order.
                    _currentOrder = buildQueue.Dequeue();
                    _currentOrderProgress = 1;
                    OnBuildStarted(EventArgs.Empty);
                }
            }
            else
            {
                _currentOrderProgress++;

                // Sends in the created unit when it is completed, else,
                // send the progress update to the caller.
                if (_currentOrderProgress >= _currentOrder.recruitTime)
                {
                    var buildFinishEventArgs = new BuildFinishedEventArgs(Instantiate(_currentOrder.unitPrefab, _currentOrder.spawnPoint, Quaternion.identity));

                    OnBuildFinished(buildFinishEventArgs);
                    _currentOrder = null;
                }
                else
                {
                    OnUpdatedBuildProgress(new BuildProgressEventArgs(_currentOrderProgress, _currentOrder.recruitTime));
                }
            }
        }

        /// <summary>
        /// Raises the <see cref="E:OnBuildStart" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected virtual void OnBuildStarted(EventArgs e)
        {
            OnBuildStart?.Invoke(this, e);
        }

        /// <summary>
        /// Raises the <see cref="E:OnBuildProgressUpdate" /> event.
        /// </summary>
        /// <param name="buildProgressEventArgs">The <see cref="BuildProgressEventArgs"/> instance containing the event data.</param>
        protected virtual void OnUpdatedBuildProgress(BuildProgressEventArgs buildProgressEventArgs)
        {
            OnBuildProgressUpdate?.Invoke(this, buildProgressEventArgs);
        }

        /// <summary>
        /// Raises the <see cref="E:OnBuildFinish" /> event.
        /// </summary>
        /// <param name="buildProgressEventArgs">The <see cref="BuildProgressEventArgs"/> instance containing the event data.</param>
        protected virtual void OnBuildFinished(BuildFinishedEventArgs buildFinishedEventArgs)
        {
            OnBuildFinish?.Invoke(this, buildFinishedEventArgs);
        }

        /// <summary>
        /// Add a unit order to the queue.
        /// </summary>
        /// <param name="unitOrder">The unit order.</param>
        public void AddToBuildQueue(UnitOrder unitOrder)
        {
            buildQueue.Enqueue(unitOrder);
        }
    }
}