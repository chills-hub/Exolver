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
    public float Damage { get; set; } = 5f;
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
    public PlayerStats()
    {
    }

}
