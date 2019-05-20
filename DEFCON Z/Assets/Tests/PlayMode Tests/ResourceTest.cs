using DefconZ.Simulation;
using NUnit.Framework;
using System.Collections.Generic;

namespace Tests
{
    public class ResourceTest
    {
        protected Resource resource;
        protected ICollection<Modifier> Modifiers;

        /// <summary>
        /// Sets up test on first test initiation..
        /// </summary>
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            Modifiers = new List<Modifier>();
        }

        /// <summary>
        /// Sets up data before each test.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            resource = new Resource(Modifiers);
        }

        /// <summary>
        /// Clean up data after each test.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            Modifiers.Clear();
            resource = null;
        }

        /// <summary>
        /// Resources should not exceed maximum when being gathered.
        /// </summary>
        [Test]
        public void Resource_ShouldNot_Exceed_Max()
        {
            // Arrange

            // Increase resource recovery rate by huge amount.
            Modifiers.Add(new Modifier
            {
                Value = 1000
            });

            // Act
            // Try adding gather resources.
            for (int i = 0; i < 5; i++)
            {
                resource.GatherResource();
            }

            // Assert
            Assert.That(resource.GetMaxResourcePoint, Is.EqualTo(resource.ResourcePoint));
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
            Modifiers.Add(mod);
            Modifiers.Add(mod2);

            // Act
            float actualMaxResourcePoint = resource.GetMaxResourcePoint;

            // Assert
            Assert.That(expectedMaxValue, Is.EqualTo(actualMaxResourcePoint));
        }

        [Test]
        public void Resource_GatherResource_Should_Follow_Modifiers()
        {
            // Arrange
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

            // The additional modifier value will increase the gathering value
            // from its base value by 80 percent.
            Modifiers.Add(mod);
            Modifiers.Add(mod2);

            float expectedIncrease = resource.GetMaxResourcePoint / 1095.0f * 1.8f;

            // Act
            float startingResource = resource.ResourcePoint;
            var actualIncrease = resource.GatherResource();

            // Assert
            Assert.That(expectedIncrease, Is.EqualTo(actualIncrease).Within(margin));
        }
    }
}