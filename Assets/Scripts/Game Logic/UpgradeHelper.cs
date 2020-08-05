using System;
using UnityEngine;

public class UpgradeHelper : MonoBehaviour
{
    private ReferenceHolder _refHolder;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.UpgradePlayerHealth += IncrementHealth;
        EventManager.UpgradePlayerDamage += IncrementDamage;
        EventManager.UpgradePlayerDefense += IncrementDefense;
        _refHolder = FindObjectOfType<GameManager>().GetComponent<ReferenceHolder>();
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
        _refHolder.HealthValueText.text = newValue.ToString();
        _refHolder.LevelValueText.text = newLevel.ToString();
        _refHolder.UpgradePointsValueText.text = availablePoints.ToString();
    }

    public void IncrementDamage(float newValue, int newLevel, int availablePoints)
    {
        _refHolder.DamageValueText.text = newValue.ToString();
        _refHolder.LevelValueText.text = newLevel.ToString();
        _refHolder.UpgradePointsValueText.text = availablePoints.ToString();
    }

    public void IncrementDefense(int newValue, int newLevel, int availablePoints)
    {
        _refHolder.DefenseValueText.text = newValue.ToString();
        _refHolder.LevelValueText.text = newLevel.ToString();
        _refHolder.UpgradePointsValueText.text = availablePoints.ToString();
    }

    /// <summary>
    /// Purpose of this method will be to increase the amount of experience needed to get a single upgrade point as your level increases
    /// </summary>
    public void IncrementCostOfUpgradePoint()
    {
        throw new NotImplementedException();
    }
}
