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
    public TextMeshProUGUI HealthValueText;
    public TextMeshProUGUI DamageValueText;
    public TextMeshProUGUI DefenseValueText;
    public TextMeshProUGUI LevelValueText;
    public TextMeshProUGUI UpgradePointsValueText;
    public TextMeshProUGUI Pausedtext;
    public Button UpgradeHealthButtonArrow;
    public Button UpgradeDamageButtonArrow;
    public Button UpgradeDefenseButtonArrow;
    public Button SaveButton;
    public BoxCollider2D DungeonCollider;
    //Upgrade Button
    public Button CloseUpgradeMenu;
}
