using System;
using System.Collections;
using UnityEngine;

public class FlyingEye : MonoBehaviour
{
    //public GameManager GameManager;
    private float enemySpotDistance = 20f;
    private float enemyRangedDistance = 10f;

    private EnemyAi _enemyAi;
    public GameObject Fireball;
    private float timeBtwShots;
    public float startTimeBtwShots;
    private Animator EnemyAnimator;

    // Start is called before the first frame update
    void Start()
    {
        _enemyAi = GetComponent<EnemyAi>();
        _enemyAi.enabled = false;
        EnemyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyAlert();

        if (CheckForDistance("Ranged")) 
        {
            PerformAttack2();
        }

        if (Vector2.Distance(transform.position, FindObjectOfType<PlayerController>().transform.position) <= 2f)
        {
            EnemyAnimator.SetBool("Attack1", true);
        }
        else { EnemyAnimator.SetBool("Attack1", false); }
    }

    void PerformAttack2() 
    {
            if (timeBtwShots <= 0)
            {
                Instantiate(Fireball, transform.position, Quaternion.identity).GetComponent<Fireball>().Damage = GetComponent<EnemyAi>()._enemyStats.AttackDamage1;
                timeBtwShots = startTimeBtwShots;
            }
             else
            {
                timeBtwShots -= Time.deltaTime;
            }
    }

    bool CheckForDistance(string type) 
    {
        if (type == "Ranged")
        {
            return Vector2.Distance(transform.position, FindObjectOfType<PlayerController>().transform.position) <= enemyRangedDistance;
        }
        else 
        {
            return Vector2.Distance(transform.position, FindObjectOfType<PlayerController>().transform.position) <= enemySpotDistance;
        }
    }

    void EnemyAlert() 
    {
        if (CheckForDistance("Alert")) 
        {
            _enemyAi.enabled = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            FindObjectOfType<GameManager>().ApplyHealthChanges(GetComponent<EnemyAi>()._enemyStats.AttackDamage2);
            Physics2D.IgnoreCollision(collision.collider, GetComponent<CapsuleCollider2D>());
        }
    }
}
