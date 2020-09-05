using System;
using UnityEngine;
using TMPro;

public class UpgradeHelper : MonoBehaviour
{
    [HideInInspector]
    public TextMeshProUGUI HealthValueText;
    [HideInInspector]
    public TextMeshProUGUI DamageValueText;
    [HideInInspector]
    public TextMeshProUGUI DefenseValueText;
    [HideInInspector]
    public TextMeshProUGUI LevelValueText;
    [HideInInspector]
    public TextMeshProUGUI UpgradePointsValueText;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.UpgradePlayerHealth += IncrementHealth;
        EventManager.UpgradePlayerDamage += IncrementDamage;
        EventManager.UpgradePlayerDefense += IncrementDefense;
    }

    private void OnDisable()
    {
        EventManager.UpgradePlayerHealth -= IncrementHealth;
        EventManager.UpgradePlayerDamage -= IncrementDamage;
        EventManager.UpgradePlayerDefense -= IncrementDefense;
    }

    /// <summary>
    /// Purpose of this method will be to increment the stats by whatever number has been added in the upgrade windows
    /// Might be able to clean this up into one method 
    /// </summary>
    public void IncrementHealth(float newValue, int newLevel, int availablePoints) 
    {
        HealthValueText.text = newValue.ToString();
        LevelValueText.text = newLevel.ToString();
        UpgradePointsValueText.text = availablePoints.ToString();
    }

    public void IncrementDamage(float newValue, int newLevel, int availablePoints)
    {
        DamageValueText.text = newValue.ToString();
        LevelValueText.text = newLevel.ToString();
        UpgradePointsValueText.text = availablePoints.ToString();
    }

    public void IncrementDefense(int newValue, int newLevel, int availablePoints)
    {
        DefenseValueText.text = newValue.ToString();
        LevelValueText.text = newLevel.ToString();
        UpgradePointsValueText.text = availablePoints.ToString();
    }

    /// <summary>
    /// Purpose of this method will be to increase the amount of experience needed to get a single upgrade point as your level increases
    /// </summary>
    public void IncrementCostOfUpgradePoint()
    {
        throw new NotImplementedException();
    }
}
