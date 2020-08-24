﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public float damage;
    public PlayerController Player;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            gameManager.ApplyHealthChanges(damage);
           // Player.PlayerStats.AvailableUpgradePoints++;
        }        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
       
    }
}
