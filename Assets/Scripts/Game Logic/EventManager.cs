using UnityEngine;

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
    private ReferenceHolder _refHolder;
    private GameManager _gameManager;
    private bool isUpgrading = false;
    private bool IsPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<PlayerController>();
        _refHolder = FindObjectOfType<GameManager>().GetComponent<ReferenceHolder>();
        _gameManager = FindObjectOfType<GameManager>();
        UpgradeStats = new UpgradeStats();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInteractShop();
        PlayerInteractDungeon();
        PauseTheGameEvent();
    }

    //Re-use this for dungeon entrance
    void PlayerInteractShop()
    {
        if (Player.GetComponent<CapsuleCollider2D>().IsTouching(_refHolder.Merchant.transform.GetComponent<BoxCollider2D>()))
        {
            _refHolder.InteractArrow.SetActive(true);
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
            _refHolder.InteractArrow.SetActive(false);
            isUpgrading = false;
        }

        if (!isUpgrading && !_refHolder.MerchantUiPanel.activeInHierarchy)
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
        if (Player.GetComponent<CapsuleCollider2D>().IsTouching(_refHolder.DungeonCollider.GetComponentInChildren<BoxCollider2D>()))
        {
            _refHolder.InteractArrowDungeon.SetActive(true);
            if (Player._inputManager.GetInteractionInput())
            {
                if (OnInteractDungeon != null)
                    OnInteractDungeon();
            }
        }
        else 
        {
            _refHolder.InteractArrowDungeon.SetActive(false);
        }
    }

    public void CloseShopMenu()
    {
        isUpgrading = false;
        _refHolder.MerchantUiPanel.gameObject.SetActive(false);
        _refHolder.MerchantSpeechPanel.gameObject.SetActive(false);
        Player.GetComponent<PlayerController>().enabled = true;
    }

    public void PauseTheGameEvent()
    {
      if (Player._inputManager.Pause()) 
      {
         IsPaused = !IsPaused;
      }

        CheckPaused();
    }

    //Might be able to put all these in one event call
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
            _gameManager._gameState = GameState.Paused;
            Time.timeScale = 0;
            _refHolder.Pausedtext.gameObject.SetActive(true);
            _refHolder.SaveButton.gameObject.SetActive(true);
            CloseShopMenu();
        }

        if (!IsPaused && _gameManager._gameState == GameState.Paused)
        {
            _gameManager._gameState = GameState.Active;
            Time.timeScale = 1;
            _refHolder.Pausedtext.gameObject.SetActive(false);
            _refHolder.SaveButton.gameObject.SetActive(false);
        }
    }
}