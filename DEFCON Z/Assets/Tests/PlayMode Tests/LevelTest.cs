using System.Collections;
using System.Collections.Generic;
using DefconZ.Simulation;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class LevelTest
    {

        /// <summary>
        /// Tests if XP Earned is increased when added.
        /// </summary>
        [Test]
        public void AddXP_Should_Increase_AmountOfXPEarned()
        {
            // Arrange
            var level = new Level();
            int expectedXp = 100;
            int xpToAdd = 100;

            // Act
            level.AddXP(xpToAdd);


            // Assert
            Assert.AreEqual(expectedXp, level.TotalXPEarned);
        }


        /// <summary>
        /// This tests whether the level is incremented when XP threshold
        /// is reached. The starting level and xp is zero.
        /// </summary>
        /// <param name="xpToAdd">The xp to add.</param>
        /// <param name="expectedLevel">The expected level.</param>
        [TestCase(100, 1)]
        [TestCase(200, 2)]
        [TestCase(50, 0)]
        [TestCase(150, 1)]
        [TestCase(1000, 10)]
        public void AddXP_Should_Increase_Level_When_XPThreshold_Reached(int xpToAdd, int expectedLevel)
        {
            // Arrange
            var level = new Level();

            // Act
            level.AddXP(xpToAdd);


            // Assert
            Assert.That(expectedLevel, Is.EqualTo(level.CurrentLevel));
        }

        /// <summary>
        /// The level increase should increase modifier value appropriately.
        /// </summary>
        /// <param name="xpToAdd">The xp to add.</param>
        /// <param name="expectedModValue">The expected mod value.</param>
        [TestCase(100, 0.01f)]
        [TestCase(500, 0.05f)]
        [TestCase(1000, 0.075f)]
        public void AddXp_Should_Increase_ModifierValue_Appropriately(int xpToAdd, float expectedModValue)
        {
            // Arrange
            var level = new Level();
            float margin = 0.001f;

            // Act
            level.AddXP(xpToAdd);

            // Assert
            Assert.That(expectedModValue, Is.EqualTo(level.LevelModifier.Value).Within(margin));
        }
    }
}
