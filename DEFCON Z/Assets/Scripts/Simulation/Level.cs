namespace DefconZ.Simulation
{
    /// <summary>
    /// The faction's level. This represents how capable the factions are.
    ///
    /// There are two ways in the game to increase level.
    ///
    /// 1. By gaining xp points.
    /// 2. Trading science for Level.
    ///
    /// Player increase level by gaining 100 experience point.
    /// </summary>
    public class Level
    {
        public int CurrentLevel { get; protected set; } = 0;
        public int TotalXPEarned { get; protected set; }
        public Modifier LevelModifier { get; }

        public Level()
        {
            // Starting modifier for level 0.
            LevelModifier = new Modifier
            {
                Name = nameof(Level),
                Type = ModifierType.Level,
                Value = 0.0f
            };
        }

        /// <summary>
        /// Adds experience point.
        /// </summary>
        /// <param name="xpToAdd">The xp to add.</param>
        public void AddXP(int xpToAdd)
        {
            TotalXPEarned += xpToAdd;

            int newLevel = TotalXPEarned / 100;

            int increaseInLevel = newLevel - CurrentLevel;

            if (increaseInLevel > 0)
            {
                IncreaseLevel(increaseInLevel);
            }
        }

        /// <summary>
        /// Increases the level.
        /// </summary>
        /// <param name="levelToIncrease">The level to increase.</param>
        private void IncreaseLevel(int levelToIncrease)
        {
            for (int i = 0; i < levelToIncrease; i++)
            {
                IncreaseLevel();
            }
        }

        /// <summary>
        /// Increases the level.
        /// </summary>
        private void IncreaseLevel()
        {
            float modToIncrease = 0.0f;

            if (CurrentLevel < 5)
            {
                modToIncrease = 0.01f;
            }
            else
            {
                modToIncrease = 0.005f;
            }

            LevelModifier.Value += modToIncrease;
            CurrentLevel++;
        }
    }
}