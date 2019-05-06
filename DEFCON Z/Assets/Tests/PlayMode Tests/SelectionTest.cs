using System;
using System.Collections;
using System.Collections.Generic;
using DefconZ;
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
        public void SelectedObjectAction()
        {
			// Arange
			GameObject playerTestObj = new GameObject();
			playerTestObj.AddComponent<Player>();
			Player player = playerTestObj.GetComponent<Player>();
			player.selectedObject = new GameObject();
			player.selectedObject.AddComponent<Prop>();
			player.selectedObject.GetComponent<Prop>().objName = "TEST PROP";

			// Act
			// Try use selected object action
			player.SelectedObjectAction();

			// Assert
			Assert.That(player.selectedObject.GetComponent<Prop>().objName, Is.EqualTo("TEST PROP"));
		}

		/// <summary>
		/// Players selected unit should succesfully be able to be nullified
		/// </summary>
		/// <returns></returns>
		[Test]
		public void SelectionDeSelectTest()
		{
			// Arange
			Player player = new Player();
			player.selectedObject = new GameObject();

			// Act
			// Try set player selected unit to null
			player.selectedObject = null;

			// Assert
			Assert.That(player.selectedObject, Is.EqualTo(null));
		}

        /// <summary>
		/// Test that players with no unit selected do not throw errors when issuing orders  with nothing selected
		/// </summary>
        [Test]
        public void NullUnitActionTest()
        {
			// Arange
			Player player = new Player();
			player.selectedObject = null;

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
		public void UnitOrderNoFactionTest()
		{
			Player player = new Player();
			player.selectedObject = new GameObject();
			player.selectedObject.AddComponent<Zombie>();

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
