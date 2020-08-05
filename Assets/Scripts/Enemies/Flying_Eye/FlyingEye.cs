using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class FlyingEye : MonoBehaviour
{
    public GameManager GameManager;
    EnemyStats enemyStats = new EnemyStats();
    private float enemySpotDistance = 20f;
    private float enemyRangedDistance = 10f;
    private EnemyAi _enemyAi;
    public GameObject Fireball;

    private float timeBtwShots;
    public float startTimeBtwShots;

    // Start is called before the first frame update
    void Start()
    {
        _enemyAi = GetComponent<EnemyAi>();
        _enemyAi.enabled = false;
        enemyStats.Health = 30;
        enemyStats.Defence = 0;
        enemyStats.AttackDamage1 = 5;
        enemyStats.AttackDamage2 = 8;
    }

    // Update is called once per frame
    void Update()
    {
        EnemyAlert();
        PerformAttack1();
    }

    void PerformAttack1() 
    {
        if (Vector2.Distance(transform.position, FindObjectOfType<PlayerController>().transform.position) <= enemyRangedDistance)
        {
            if (timeBtwShots <= 0)
            {
                Instantiate(Fireball, transform.position, Quaternion.identity).GetComponent<Fireball>().Damage = enemyStats.AttackDamage1;
                timeBtwShots = startTimeBtwShots;
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
            //Instantiate fireball here
        }
    }

    void PerformAttack2()
    {
        throw new NotImplementedException();
    }

    void TakeDamage()
    {
        throw new NotImplementedException();
    }

    void EnemyDie()
    {
        throw new NotImplementedException();
    }

    void EnemyAlert() 
    {
        if (Vector2.Distance(transform.position, FindObjectOfType<PlayerController>().transform.position) <= enemySpotDistance) 
        {
            _enemyAi.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            FindObjectOfType<GameManager>().ApplyHealthChanges(3f);
        }
    }
}
