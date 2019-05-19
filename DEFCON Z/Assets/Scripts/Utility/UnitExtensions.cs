namespace DefconZ.Utility
{
    public static class UnitExtensions
    {
        /// <summary>
        /// Determines whether the specified other unit is hostile.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="otherUnit">The other unit.</param>
        /// <returns>
        ///   <c>true</c> if the specified other unit is hostile; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsHostile(this UnitBase unit, UnitBase otherUnit)
        {
            bool isHostile = false;

            if (unit.FactionOwner != otherUnit.FactionOwner)
            {
                isHostile = true;
            }

            return isHostile;
        }
    }
}