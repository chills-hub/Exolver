using SaveSystem;
using System;
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

        //_gameState = GameState.Active;
    }

    public void ApplyHealthChanges(float damage) 
    {
        float defenseValue = 100 - Player.PlayerStats.Defense;
        string decimalValue = "0." + defenseValue;
        decimal actualDefenseValue = Convert.ToDecimal(decimalValue);
        float afterDamageHealth = Player.currentHealth - (damage * (float)actualDefenseValue);
        StartCoroutine(Player.DamageFlash());
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
            StartCoroutine(WaitForEndGame());
        }
    }

    void SetPlayerHealth() 
    {
        Player.PlayerStats = new PlayerStats();
        Healthbar.SetHealth(Player.PlayerStats.MaxHealth);
    }

    private IEnumerator WaitForEndGame() 
    {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0;
    }
}
