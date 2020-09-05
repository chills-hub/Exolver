using UnityEngine;
using TMPro;

public class InteractionHelper : MonoBehaviour
{
    private PlayerController player;
    private GameManager gameManager;

    [HideInInspector]
    public GameObject MerchantUiPanel;
    [HideInInspector]
    public GameObject MerchantSpeechPanel;
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

    private void OnDisable()
    {
        EventManager.OnInteract -= Interact;
        EventManager.OnInteractDungeon -= InteractDungeon;
    }

    // Start is called before the first frame update
    void Start()
    {
        EventManager.OnInteract += Interact;
        EventManager.OnInteractDungeon += InteractDungeon;
        gameManager = FindObjectOfType<GameManager>();
        player = FindObjectOfType<PlayerController>();
    }

    void Interact()
    {
        MerchantUiPanel.SetActive(true);
        MerchantSpeechPanel.SetActive(true);
        HealthValueText.text = player.PlayerStats.MaxHealth.ToString();
        DamageValueText.text = player.PlayerStats.Damage.ToString();
        DefenseValueText.text = player.PlayerStats.Defense.ToString();
        LevelValueText.text = player.PlayerStats.Level.ToString();
        UpgradePointsValueText.text = player.PlayerStats.AvailableUpgradePoints.ToString();
    }

    void InteractDungeon() 
    {
        //DISPLAY TEXT HERE
        //INTO THE NIGHTMARE
        gameManager.startGame.EnterGameplay();
    }
}
