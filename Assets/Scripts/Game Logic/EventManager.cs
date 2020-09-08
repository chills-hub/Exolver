using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    public delegate void InteractAction();
    public static event InteractAction OnInteract;

    public delegate void InteractActionDungeon();
    public static event InteractActionDungeon OnInteractDungeon;

    public delegate void PauseGame();
    public static event PauseGame PauseTheGame;

    public delegate void UpgradeHealth(float newValue, int level, int availablePoints);
    public static event UpgradeHealth UpgradePlayerHealth;

    public delegate void UpgradeDamage(float newValue, int level, int availablePoints);
    public static event UpgradeDamage UpgradePlayerDamage;

    public delegate void UpgradeDefense(int newValue, int level, int availablePoints);
    public static event UpgradeDefense UpgradePlayerDefense;

    private PlayerController Player;
    private UpgradeStats UpgradeStats;
    private bool isUpgrading = false;
    private bool IsPaused = false;

    [HideInInspector]
    public GameObject Merchant;
    [HideInInspector]
    public GameObject InteractArrow;
    [HideInInspector]
    public GameObject InteractArrowDungeon;
    [HideInInspector]
    public GameObject MerchantUiPanel;
    [HideInInspector]
    public BoxCollider2D DungeonCollider;
    [HideInInspector]
    public GameObject MerchantSpeechPanel;
    [HideInInspector]
    public TextMeshProUGUI Pausedtext;
    [HideInInspector]
    public GameManager GameManager;


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        Player = FindObjectOfType<PlayerController>();
        UpgradeStats = new UpgradeStats();
    }

    // Update is called once per frame
    void Update()
    {
        PauseTheGameEvent();

        //need to disable these on level 2 for pausing to work
        if (GameManager.CurrentLevel == 1) 
        {
            PlayerInteractShop();
            PlayerInteractDungeon();
        }  
    }

    //Re-use this for dungeon entrance
    void PlayerInteractShop()
    {
            if (Player.GetComponent<CapsuleCollider2D>().IsTouching(Merchant.transform.GetComponent<BoxCollider2D>()))
            {
                InteractArrow.SetActive(true);
                if (Player._inputManager.GetInteractionInput())
                {
                    Player.GetComponent<PlayerController>().enabled = false;
                    isUpgrading = true;
                    if (OnInteract != null)
                        OnInteract();
                }
            }
            else
            {
                InteractArrow.SetActive(false);
                isUpgrading = false;
            }

            if (!isUpgrading && !MerchantUiPanel.activeInHierarchy)
            {
                Player.GetComponent<PlayerController>().enabled = true;
            }

            if (Player._inputManager.Escape())
            {
                CloseShopMenu();
            }
    }

    void PlayerInteractDungeon() 
    {
        if (Player.GetComponent<CapsuleCollider2D>().IsTouching(DungeonCollider.GetComponentInChildren<BoxCollider2D>()))
        {
            InteractArrowDungeon.SetActive(true);
            if (Player._inputManager.GetInteractionInput())
            {
             //   StartCoroutine(GameManager.FadeToBlack(true));
                if (OnInteractDungeon != null)
                    OnInteractDungeon();
            }
        }
        else
        {
            InteractArrowDungeon.SetActive(false);
        }
    }

    public void CloseShopMenu()
    {
        isUpgrading = false;
        MerchantUiPanel.gameObject.SetActive(false);
        MerchantSpeechPanel.gameObject.SetActive(false);
        Player.GetComponent<PlayerController>().enabled = true;
        GameManager.GetComponent<SaveGame>().Save();
    }

    public void PauseTheGameEvent()
    {
      if (Player._inputManager.Pause()) 
      {
         IsPaused = !IsPaused;
      }

        CheckPaused();
    }

    public void UpgradeHealthStat()
    {
        if (Player.PlayerStats.AvailableUpgradePoints > 0)
        {
            Player.PlayerStats.MaxHealth += UpgradeStats.NumToIncrementHealth;
            Player.PlayerStats.AvailableUpgradePoints--;
            Player.PlayerStats.Level++;
            if (UpgradePlayerHealth != null)
                UpgradePlayerHealth(Player.PlayerStats.MaxHealth, Player.PlayerStats.Level, Player.PlayerStats.AvailableUpgradePoints);
        }
    }

    public void UpgradeDamageStat()
    {
        if (Player.PlayerStats.AvailableUpgradePoints > 0)
        {
            Player.PlayerStats.Damage += UpgradeStats.NumToIncrementDamage;
            Player.PlayerStats.AvailableUpgradePoints--;
            Player.PlayerStats.Level++;
            if (UpgradePlayerDamage != null)
                UpgradePlayerDamage(Player.PlayerStats.Damage, Player.PlayerStats.Level, Player.PlayerStats.AvailableUpgradePoints);
        }
    }

    public void UpgradeDefenseStat()
    {
        if (Player.PlayerStats.AvailableUpgradePoints > 0)
        {
            Player.PlayerStats.Defense += UpgradeStats.NumToIncrementDefence;
            Player.PlayerStats.AvailableUpgradePoints--;
            Player.PlayerStats.Level++;
            if (UpgradePlayerDefense != null)
                UpgradePlayerDefense(Player.PlayerStats.Defense, Player.PlayerStats.Level, Player.PlayerStats.AvailableUpgradePoints);
        }
    }

    void CheckPaused()
    {
        if (IsPaused)
        {
            GameManager._gameState = GameState.Paused;
            Time.timeScale = 0;
            Pausedtext.gameObject.SetActive(true);
        }

        if (!IsPaused && GameManager._gameState == GameState.Paused)
        {
            GameManager._gameState = GameState.Active;
            Time.timeScale = 1;
            Pausedtext.gameObject.SetActive(false);
        }
    }
}