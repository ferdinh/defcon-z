using System.Collections;
using System.Collections.Generic;
using DefconZ;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class PropTests
    {
		/// <summary>
		/// A prop should not take damage if dealt 0 damage
		/// </summary>
		[Test]
		public void TestPropDamageZero()
		{
			// Arange
			GameObject testProp = new GameObject();
			testProp.AddComponent<Prop>();
			Prop prop = testProp.GetComponent<Prop>();
			prop.health = 15.0f;

			// Act
			// Damage the prop by 0
			prop.TakeDamage(0.0f);

			// Assert
			Assert.That(prop.health, Is.EqualTo(15.0f));
		}

		/// <summary>
		/// A prop should take damage when dealt damage
		/// </summary>
		[Test]
		public void TestPropDamageSmall()
		{
			// Arange
			GameObject testProp = new GameObject();
			testProp.AddComponent<Prop>();
			Prop prop = testProp.GetComponent<Prop>();
			prop.health = 15.0f;

			// Act
			// Damage the prop by 0
			prop.TakeDamage(1.0f);

			// Assert
			Assert.That(prop.health, Is.LessThan(15.0f));
		}

		/// <summary>
		/// A prop should be destroyed if dealt damage more than it's life  total
		/// </summary>
		[Test]
		public void TestPropDamageMoreThanTotalHealth()
		{
			// Arange
			GameObject testProp = new GameObject();
			testProp.AddComponent<Prop>();
			Prop prop = testProp.GetComponent<Prop>();
			prop.health = 15.0f;
			prop.objName = "TEST PROP";

			// Act
			// Damage the prop by 0
			prop.TakeDamage(150.0f);

			// Assert
			// Check that the unit has removed itself
			//Assert.That(prop.health, Is.EqualTo(null));
			LogAssert.Expect(LogType.Log, "TEST PROP has reached 0 or less health and has been destroyed");
		}


		/// <summary>
		/// Prop should destroy itself when DestroySelf() is called.
		/// </summary>
		[Test]
        public void PropDestroySelf()
        {
			// Arange
			GameObject testProp = new GameObject();
			testProp.AddComponent<Prop>();
			Prop prop = testProp.GetComponent<Prop>();
			prop.health = 15.0f;
			prop.objName = "TEST PROP";

			// Act
			// Damage the prop by 0
			prop.DestroySelf();

			// Assert
			// Check that the unit has removed itself
			//Assert.That(prop.health, Is.EqualTo(null));
			LogAssert.Expect(LogType.Log, "TEST PROP has reached 0 or less health and has been destroyed");
		}
    }
}
