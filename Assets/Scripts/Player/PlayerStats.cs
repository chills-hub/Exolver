[System.Serializable]
public class PlayerStats
{
    /// <summary>
    /// The player's total health
    /// </summary>
    public float MaxHealth { get; set; } = 100f;
    /// <summary>
    /// The player's damage modifier
    /// </summary>
    public float Damage { get; set; } = 50f;
    /// <summary>
    /// The player's defence modifier, base is full damage e.g 100%
    /// </summary>
    public int Defense { get; set; } = 1;
    /// <summary>
    /// The player's total level
    /// </summary>
    public int Level { get; set; } = 0;
    /// <summary>
    /// The player's number of upgrade points
    /// </summary>
    public int AvailableUpgradePoints { get; set; } = 0;
    public float CurrentExpTowardsExpPoint { get; set; } = 0;
    public float ExpPointThreshold { get; set; } = 100;
    public PlayerStats()
    {
    }

    /// <summary>
    /// Purpose of this method will be to increase the amount of experience needed to get a single upgrade point as your level increases
    /// </summary>
    public void ShouldIncrementCostOfUpgradePoint()
    {
        if (CurrentExpTowardsExpPoint >= ExpPointThreshold) 
        {
            ExpPointThreshold += 100;
            AvailableUpgradePoints++;
        }
    }

}
