using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    /// <summary>
    /// Base Class for tests. It provide the basic components that frequently
    /// occurs when writing tests for the project.
    /// </summary>
    /// <remarks>
    /// SetUp and TearDown attribute are safe to override. Include the call to
    /// base class.
    ///
    /// GameObjects will be set to "active" by default.
    /// </remarks>
    public class BaseTest
    {
        protected GameObject _gameObject;

        /// <summary>
        /// Sets up test for every run.
        /// </summary>
        [SetUp]
        public virtual void SetUp()
        {
            _gameObject = new GameObject();
        }

        /// <summary>
        /// Clean up test after each run.
        /// </summary>
        [TearDown]
        public virtual void TearDown()
        {
            _gameObject = null;
        }
    }
}