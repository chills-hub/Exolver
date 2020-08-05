using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeStats
{
    /// <summary>
    /// The number of upgrade points the player currently has
    /// </summary>
    public int NumOfUpgradePoints { get; set; } = 0;
    /// <summary>
    /// How much EXP 1 upgrade point is worth
    /// </summary>
    public int UpgradePointWorth { get; set; } = 100;
    /// <summary>
    /// How many health points are added per upgrade point
    /// </summary>
    public int NumToIncrementHealth { get; set; } = 5;
    /// <summary>
    /// How many damage points are added per upgrade point
    /// </summary>
    public int NumToIncrementDamage { get; set; } = 1;
    /// <summary>
    /// The number to decrease the damage defence multipler by when hit
    /// e.g starts at 0.95 so damage will be: incoming damage x 0.95 to get the final damage amount
    /// </summary>
    public int NumToIncrementDefence { get; set; } = 1;

    public UpgradeStats() { }
}
