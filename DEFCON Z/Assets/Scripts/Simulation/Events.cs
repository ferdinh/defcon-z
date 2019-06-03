using UnityEngine;

namespace DefconZ.Simulation
{
    /// <summary>
    /// An game event.
    /// </summary>
    /// <seealso cref="UnityEngine.ScriptableObject" />
    public class Events : ScriptableObject
    {
        public Modifier modifier;
        public string description;
        public int duration;
    }
}