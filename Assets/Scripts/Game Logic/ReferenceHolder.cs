using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ReferenceHolder : MonoBehaviour
{
    public GameObject MerchantUiPanel;
    public GameObject MerchantSpeechPanel;
    public GameObject Merchant;
    public GameObject InteractArrow;
    public GameObject InteractArrowDungeon;
    public GameObject FadeToBlackImage;
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
    //Upgrade Button
    public Button CloseUpgradeMenu;

    static ReferenceHolder refHolderInstance;

    private void Awake()
    {
        if (refHolderInstance == null)
        {
            //First run, set the instance
            refHolderInstance = this;
            DontDestroyOnLoad(this);

        }
        else if (refHolderInstance != this)
        {
            //Instance is not the same as the one we have, destroy old one, and reset to newest one
            Destroy(refHolderInstance.gameObject);
            refHolderInstance = this;
            DontDestroyOnLoad(this);
        }
    }
}
