using SaveSystem;
using System;
using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public PlayerController Player;
    public int CurrentLevel;
    public GameState _gameState = new GameState();
    private AudioSource _audioSource;

    [HideInInspector]
    public HealthBar Healthbar;
    [HideInInspector]
    public GameObject FadeToBlackImage;
    [HideInInspector]
    public TextMeshProUGUI GameOverText;
    [HideInInspector]
    public StartGame startGame;
    [HideInInspector]
    public SaveGame Loader;

    static GameManager gameManagerInstance; 

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (gameManagerInstance == null)
        {
            //First run, set the instance
            gameManagerInstance = this;

        }
        else if (gameManagerInstance != this)
        {
            //Instance is not the same as the one we have, destroy old one, and reset to newest one
            Destroy(gameManagerInstance.gameObject);
            gameManagerInstance = this;
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        CurrentLevel = level;

        if (level == 1 && GameOverText != null)
        {
            GameOverText.gameObject.SetActive(false);
        }

        if (level == 2) 
        {
            //stop the hub music from gamemanager
            _audioSource.Stop();
            SetPlayerHealth();
            SetBlackoutAlphaTo1();
            StartCoroutine(FadeToBlack(false));
            
            //Initialise statemachine again
            InitStateMachine();
        }
    }

    private void InitStateMachine() 
    {
        Player.standing = StandingState.CreateStandingState(Player, Player.movementSM);
        Player.dodging = DodgingState.CreateDodgingState(Player, Player.movementSM);
        Player.jumping = JumpingState.CreateJumpingState(Player, Player.movementSM);
        Player.movementSM.Initialise(Player.standing);
    }

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        Loader = gameObject.GetComponent<SaveGame>();
        startGame = gameObject.AddComponent<StartGame>();
        SetBlackoutAlphaTo1();
        StartCoroutine(FadeToBlack(false));
        SetPlayerHealthInitial();

        if (StartGame.IsPlayerLoading) 
        {
            Loader.LoadSaveFile();
        }
        
        _gameState = GameState.Active;
    }

    private void Update()
    {
        if (Player.transform.position.y < -5)
        {
            if (_gameState == GameState.GameOver) 
            {
                return;
            }
            _gameState = GameState.GameOver;
            ApplyHealthChanges(50000);
        }
    }

    public void ApplyHealthChanges(float damage) 
    {
        float defenseValue = 100 - Player.PlayerStats.Defense;

        if (Player.isDodging) 
        {
            //player is in iFrames
            //defense value is used for multiplication against damage so zero
            //should result in no damage
            defenseValue = 0;
        }

        float afterDamageHealth = Player.currentHealth - (damage * (float)ConvertDefenseValue(defenseValue));
        StartCoroutine(Player.DamageFlash());
        Healthbar.SetHealth(afterDamageHealth);
        Player.currentHealth = afterDamageHealth;
        CheckHealthValueForGameOver(afterDamageHealth);
    }

    decimal ConvertDefenseValue(float value) 
    {
        string decimalValue = "0." + value;
        return Convert.ToDecimal(decimalValue);
    }

    void CheckHealthValueForGameOver(float health) 
    {
        if (health <= 0) 
        {
            Player._animator.SetBool("ZeroHealth", true);
            Player.moveSpeed = 0;
            _gameState = GameState.GameOver;
            StartCoroutine(WaitForEndGame());
            StartCoroutine(FadeToBlack());
        }
    }

    void SetPlayerHealthInitial() 
    {
        Player.PlayerStats = new PlayerStats();
        Healthbar.SetHealth(Player.PlayerStats.MaxHealth);
    }

    void SetPlayerHealth() 
    {
        Healthbar.SetHealth(Player.PlayerStats.MaxHealth);
    }

    void EndGameDialogue() 
    {
        Debug.Log("PLAYER DEATH");
        GetComponent<SaveGame>().Save();
        //Destroy(Player.transform);
        //UNSURE IF THIS WORKS CORRECTLY
        if (_gameState == GameState.GameOver) 
        {   
            startGame.StartTheGameWithLoad();
            _gameState = GameState.Active;
        }
    }

    void SetBlackoutAlphaTo1() 
    {
        Color objectColor = FadeToBlackImage.GetComponent<Image>().color;
        objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, 1);
        FadeToBlackImage.GetComponent<Image>().color = objectColor;
    }

    private IEnumerator WaitForEndGame() 
    {
        GameOverText.gameObject.SetActive(true);
        Player.jumpForce = 0;
        yield return new WaitForSeconds(1f);
        EndGameDialogue();
    }

    public IEnumerator FadeToBlack(bool fadeToBlack = true, float fadeSpeed = 0.5f) 
    {
        Color objectColor = FadeToBlackImage.GetComponent<Image>().color;
        float fadeAmount;

        if (fadeToBlack)
        {
            while (FadeToBlackImage.GetComponent<Image>().color.a < 1)
            {
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                FadeToBlackImage.GetComponent<Image>().color = objectColor;
                yield return null;
            }
        }
        else 
        {
            while (FadeToBlackImage.GetComponent<Image>().color.a > 0)
            {
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                FadeToBlackImage.GetComponent<Image>().color = objectColor;
                yield return null;
            }
        }
        yield return new WaitForEndOfFrame();
    }
}
