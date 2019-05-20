using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefconZ.Simulation.UnitBuilder
{
    public class UnitBuilder : MonoBehaviour
    {
        public Queue<UnitOrder> buildQueue;
        private Clock _clock;
        private UnitOrder _currentOrder;
        private int _currentOrderProgress;

        public event EventHandler OnBuildStart;
        public event EventHandler<BuildProgressEventArgs> OnBuildProgressUpdate;
        public event EventHandler<BuildFinishedEventArgs> OnBuildFinish;

        private void Awake()
        {
            buildQueue = new Queue<UnitOrder>();
            _clock = GameObject.FindGameObjectWithTag(nameof(GameManager)).GetComponent<Clock>();

            _clock.GameCycleElapsed += ProcessOrder;
        }

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
                    _currentOrderProgress = 0;
                    OnBuildStart(this, EventArgs.Empty);
                }
            }
            else
            {
                _currentOrderProgress++;
                if (_currentOrderProgress >= _currentOrder.recruitTime)
                {
                    OnBuildFinish(this, new BuildFinishedEventArgs(Instantiate(_currentOrder.unitPrefab, _currentOrder.spawnPoint, Quaternion.identity)));
                    _currentOrder = null;
                }
                else
                {

                }
            }
        }

        public void AddToBuildQueue(UnitOrder unitOrder)
        {
            buildQueue.Enqueue(unitOrder);
        }

        public bool RemoveFromBuildQueue(UnitOrder unitOrder)
        {
            buildQueue.Dequeue();
            return true;
        }
    }
}