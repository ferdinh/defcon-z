using System;
using System.Collections;
using System.Collections.Generic;
using DefconZ;
using DefconZ.Units;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class SelectionTest
    {
        /// <summary>
		/// Selected props should be able to be accessed for information
		/// </summary>
        [Test]
        [Ignore("Tests need re-writing to accept new format")]
        public void SelectedObjectAction()
        {
            // Arange
            GameObject manager = new GameObject();
            manager.AddComponent<GameManager>();
            GameObject playerTestObj = new GameObject();
			playerTestObj.AddComponent<Player>();
			Player player = playerTestObj.GetComponent<Player>();
			player.selectedObjects.Add(new GameObject());
			player.selectedObjects[0].AddComponent<Prop>();
			player.selectedObjects[0].GetComponent<Prop>().objName = "TEST PROP";

			// Act
			// Try use selected object action
			player.SelectedObjectAction();

			// Assert
			Assert.That(player.selectedObjects[0].GetComponent<Prop>().objName, Is.EqualTo("TEST PROP"));
		}

		/// <summary>
		/// Players selected unit should succesfully be able to be nullified
		/// </summary>
		/// <returns></returns>
		[Test]
        [Ignore("Tests need re-writing to accept new format")]
        public void SelectionDeSelectTest()
		{
            // Arange
            Player player = new Player();
            GameObject manager = new GameObject();
            manager.AddComponent<GameManager>();
            player.selectedObjects.Add(new GameObject());

            // Act
            // Try clear the players selected objects
            player.selectedObjects.Clear();

			// Assert
			Assert.That(player.selectedObjects.Count, Is.EqualTo(0));
		}

        /// <summary>
		/// Test that players with no unit selected do not throw errors when issuing orders  with nothing selected
		/// </summary>
        [Test]
        [Ignore("Tests need re-writing to accept new format")]
        public void NullUnitActionTest()
        {
			// Arange
			Player player = new Player();
            GameObject manager = new GameObject();
            manager.AddComponent<GameManager>();
            player.selectedObjects.Add(new GameObject());

            // Act
            // Assert
            try
            {
				player.SelectedObjectAction();
			} catch (Exception)
			{
				Assert.Fail(); // If an exception was caught, the test has failed
			}
		}

		/// <summary>
		/// Test that players are not able to give orders to units without factions
		/// </summary>
		[Test]
        [Ignore("Tests need re-writing to accept new format")]
        public void UnitOrderNoFactionTest()
		{
            Player player = new Player();
            GameObject manager = new GameObject();
            manager.AddComponent<GameManager>();
            player.selectedObjects.Add(new GameObject());
            player.selectedObjects[0].AddComponent<Zombie>();

			// Act
			// Assert
			try
			{
				player.SelectedObjectAction();
				Assert.Fail(); // If this line is reached, the assertion has failed
			} catch (Exception)
			{
				Assert.Pass();
			}
		}
	}
}
