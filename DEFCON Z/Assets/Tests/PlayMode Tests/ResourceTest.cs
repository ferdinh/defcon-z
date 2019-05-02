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


        /// <summary>
        /// The test ensure that maximum points for available resources follow
        /// the modifiers.
        /// </summary>
        [Test]
        public void Resource_CalculateMaxPoints_Should_Follow_Modifiers()
        {
            // Arrange 
            float expectedMaxValue = 18000.0f;

            Resource resource = new Resource();

            Modifier mod = new Modifier
            {
                Name = "Test Modifier",
                Type = ModifierType.Event,
                Value = 0.5f
            };

            Modifier mod2 = new Modifier
            {
                Name = "Test Modifier",
                Type = ModifierType.Event,
                Value = 0.3f
            };
    
            // The additional modifier value will increase the max value
            // from its base value by 80 percent.
            resource.Modifiers.Add(mod);
            resource.Modifiers.Add(mod2);


            // Act
            resource.CalculateMaxPoints();

            // Assert
            Assert.AreEqual(expectedMaxValue, resource.MaxResourcePoint);
        }

        [Test]
        public void Resource_GatherResource_Should_Follow_Modifiers()
        {
            // Arrange
            Resource resource = new Resource();
            float margin = 0.001f;

            Modifier mod = new Modifier
            {
                Name = "Test Modifier",
                Type = ModifierType.Event,
                Value = 0.5f
            };

            Modifier mod2 = new Modifier
            {
                Name = "Test Modifier",
                Type = ModifierType.Event,
                Value = 0.3f
            };

            resource.CalculateMaxPoints();
            resource.ComputeStartingValue();

            float expectedIncrease = resource.MaxResourcePoint / 1095.0f * 1.8f;

            // The additional modifier value will increase the gathering value
            // from its base value by 80 percent.
            resource.Modifiers.Add(mod);
            resource.Modifiers.Add(mod2);

            // Act
            float startingResource = resource.ResourcePoint;
            resource.GatherResource();

            // Assert
            var actualIncrease = resource.ResourcePoint - startingResource;
            Assert.That(expectedIncrease, Is.EqualTo(actualIncrease).Within(margin));
        }
    }
}
