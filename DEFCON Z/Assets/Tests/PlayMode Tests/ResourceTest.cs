using System.Collections;
using System.Collections.Generic;
using DefconZ.Simulation;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ResourceTest
    {
        
        /// <summary>
        /// Resources should not exceed maximum when being gathered.
        /// </summary>
        [Test]
        public void Resource_ShouldNot_Exceed_Max()
        {
            // Arrange
            Resource resource = new Resource();
            resource.CalculateMaxPoints();
            resource.ComputeStartingValue();

            // Act
            // Try adding the resources 1500 times/days. The base
            // recovery rate to max manpower is at 1095 days/3years.
            for (int i = 0; i < 1500; i++)
            {
                resource.GatherResource();
            }

            // Assert
            Assert.AreEqual(resource.MaxResourcePoint, resource.ResourcePoint);

        }

    }
}
