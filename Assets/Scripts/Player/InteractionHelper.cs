using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHelper : MonoBehaviour
{
    private ReferenceHolder _refHolder;
    private PlayerController player;

    private void OnDisable()
    {
        EventManager.OnInteract -= Interact;
    }

    // Start is called before the first frame update
    void Start()
    {
        EventManager.OnInteract += Interact;
        _refHolder = FindObjectOfType<GameManager>().GetComponent<ReferenceHolder>();
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Interact()
    {
        _refHolder.MerchantUiPanel.SetActive(true);
        _refHolder.MerchantSpeechPanel.SetActive(true);
        _refHolder.HealthValueText.text = player.PlayerStats.MaxHealth.ToString();
        _refHolder.DamageValueText.text = player.PlayerStats.Damage.ToString();
        _refHolder.DefenseValueText.text = player.PlayerStats.Defense.ToString();
        _refHolder.LevelValueText.text = player.PlayerStats.Level.ToString();
        _refHolder.UpgradePointsValueText.text = player.PlayerStats.AvailableUpgradePoints.ToString();
    }
}
