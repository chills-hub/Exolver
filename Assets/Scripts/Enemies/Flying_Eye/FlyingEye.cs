using System;
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
        PerformAttack1();
    }

    void PerformAttack1() 
    {
        if (Vector2.Distance(transform.position, FindObjectOfType<PlayerController>().transform.position) <= enemyRangedDistance)
        {
            if (timeBtwShots <= 0)
            {
                EnemyAnimator.SetBool("Attack1", true);
                Instantiate(Fireball, transform.position, Quaternion.identity).GetComponent<Fireball>().Damage = GetComponent<EnemyAi>()._enemyStats.AttackDamage1;
                timeBtwShots = startTimeBtwShots;
                EnemyAnimator.SetBool("Attack1", false);

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

    void EnemyAlert() 
    {
        if (Vector2.Distance(transform.position, FindObjectOfType<PlayerController>().transform.position) <= enemySpotDistance) 
        {
            _enemyAi.enabled = true;
        }
    }

    //NEEDS FIXED TO USE ON COLLIDER ENTER
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            FindObjectOfType<GameManager>().ApplyHealthChanges(3f);
            Physics2D.IgnoreCollision(collision.collider, GetComponent<CapsuleCollider2D>());
        }
    }
}
