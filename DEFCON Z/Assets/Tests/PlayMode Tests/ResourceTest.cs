using DefconZ.Simulation;
using NUnit.Framework;

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

            // Increase resource recovery rate by huge amount.
            resource.Modifiers.Add(new Modifier
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
            Assert.That(resource.MaxResourcePoint, Is.EqualTo(resource.ResourcePoint));
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
            float actualMaxResourcePoint = resource.MaxResourcePoint;

            // Assert
            Assert.That(expectedMaxValue, Is.EqualTo(actualMaxResourcePoint));
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

            // The additional modifier value will increase the gathering value
            // from its base value by 80 percent.
            resource.Modifiers.Add(mod);
            resource.Modifiers.Add(mod2);

            float expectedIncrease = resource.MaxResourcePoint / 1095.0f * 1.8f;

            // Act
            float startingResource = resource.ResourcePoint;
            var actualIncrease = resource.GatherResource();

            // Assert
            Assert.That(expectedIncrease, Is.EqualTo(actualIncrease).Within(margin));
        }
    }
}