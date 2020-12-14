using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ReferenceHolder : MonoBehaviour
{
    public GameObject MerchantUiPanel;
    public GameObject MerchantSpeechPanel;
    public GameObject InteractArrow;
    public GameObject InteractArrowDungeon;
    public GameObject FadeToBlackImage;
    public GameObject PlayerAimArrow;
    public GameObject MerchantTrigger;
    public TextMeshProUGUI HealthValueText;
    public TextMeshProUGUI DamageValueText;
    public TextMeshProUGUI DefenseValueText;
    public TextMeshProUGUI LevelValueText;
    public TextMeshProUGUI UpgradePointsValueText;
    public TextMeshProUGUI Pausedtext;
    public TextMeshProUGUI GameOverText;
    public Button UpgradeHealthButtonArrow;
    public Button UpgradeDamageButtonArrow;
    public Button UpgradeDefenseButtonArrow;
    public Button SaveButton;
    public BoxCollider2D DungeonCollider;
    public Button AttackButton;
    public Button JumpButton;
    public Button InteractButton;
    public Button SlideButton;
    public Joystick Joystick;

    //Upgrade Button
    public Button CloseUpgradeMenu;
    //Healthbar
    public HealthBar Healthbar;
    //GameManager
    public GameManager GameManager;
    //EventManager
    public EventManager EventManager;
    //Player
    public PlayerController Player;

    static ReferenceHolder referenceHolderInstance;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (referenceHolderInstance == null)
        {
            //First run, set the instance
            referenceHolderInstance = this;

        }
        else if (referenceHolderInstance != this)
        {
            //Instance is not the same as the one we have, destroy old one, and reset to newest one
            Destroy(referenceHolderInstance.gameObject);
            referenceHolderInstance = this;
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        GameManager.Healthbar = Healthbar;
        GameManager.FadeToBlackImage = FadeToBlackImage;
        GameManager.GameOverText = GameOverText;
        EventManager.GameManager = GameManager;
        EventManager.Pausedtext = Pausedtext;
        Player = FindObjectOfType<PlayerController>();

        if (level == 1)//aka main hub, only need these in that area
        {
            Player.gameObject.GetComponent<InteractionHelper>().gameManager = GameManager;
            EventManager.InteractArrow = InteractArrow;
            EventManager.Merchant = MerchantTrigger;
            EventManager.MerchantUiPanel = MerchantUiPanel;
            EventManager.InteractArrowDungeon = InteractArrowDungeon;
            EventManager.DungeonCollider = DungeonCollider;
            EventManager.MerchantSpeechPanel = MerchantSpeechPanel;
            Player.GetComponent<InteractionHelper>().MerchantUiPanel = MerchantUiPanel;
            Player.GetComponent<InteractionHelper>().MerchantSpeechPanel = MerchantSpeechPanel;
            Player.GetComponent<InteractionHelper>().HealthValueText = HealthValueText;
            Player.GetComponent<InteractionHelper>().DamageValueText = DamageValueText;
            Player.GetComponent<InteractionHelper>().DefenseValueText = DefenseValueText;
            Player.GetComponent<InteractionHelper>().LevelValueText = LevelValueText;
            Player.GetComponent<InteractionHelper>().UpgradePointsValueText = UpgradePointsValueText;
            GameManager.GetComponent<UpgradeHelper>().HealthValueText = HealthValueText;
            GameManager.GetComponent<UpgradeHelper>().DefenseValueText = DefenseValueText;
            GameManager.GetComponent<UpgradeHelper>().DamageValueText = DamageValueText;
            GameManager.GetComponent<UpgradeHelper>().UpgradePointsValueText = UpgradePointsValueText;
            GameManager.GetComponent<UpgradeHelper>().LevelValueText = LevelValueText;
        }
    }
}
