using SaveSystem;
using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public HealthBar Healthbar;
    public PlayerController Player;
    public GameState _gameState = new GameState();
    public SaveGame Loader;

    // Start is called before the first frame update
    void Start()
    {
        Loader = gameObject.GetComponent<SaveGame>();
        DontDestroyOnLoad(gameObject);
        SetPlayerHealth();

        if (StartGame.IsPlayerLoading) 
        {
            Loader.LoadSaveFile();
        }

        //StartCoroutine(PlayerSpeak());
        //_gameState = GameState.Active;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ApplyHealthChanges(float damage) 
    {
        // float afterDamageHealth = Player.currentHealth - (damage * (float)Player.PlayerStats.Defense);
        StartCoroutine(Player.DamageFlash());
        float afterDamageHealth = Player.currentHealth - damage;
        Healthbar.SetHealth(afterDamageHealth);
        Player.currentHealth = afterDamageHealth;
        CheckHealthValueForGameOver(afterDamageHealth);
    }

    void CheckHealthValueForGameOver(float health) 
    {
        if (health <= 0) 
        {
            Player._animator.SetBool("ZeroHealth", true);
            Player.moveSpeed = 0;
            _gameState = GameState.GameOver;
        }
    }

    void SetPlayerHealth() 
    {
        Player.PlayerStats = new PlayerStats();
        Healthbar.SetHealth(Player.PlayerStats.MaxHealth);
    }

    //IEnumerator PlayerSpeak()
    //{
    //    Player.GetComponentInChildren<TextMeshProUGUI>().text = "Where am I...";
    //    yield return new WaitForSeconds(3f);
    //    Player.GetComponentInChildren<TextMeshProUGUI>().gameObject.SetActive(false);
    //}
}
