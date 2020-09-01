using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    private EnemyAi _enemyAi;
    private Animator _enemyAnimator;
    private float enemySpotDistance = 15f;
    public GameManager gameManager;

    #region attacking vars
    public Transform rayCast;
    public LayerMask rayCastMask;
    public float rayCastLength;
    public float attackDistance;
    public float coolDownTimer;
    #endregion

    #region private vars
    private RaycastHit2D hit;
    private bool attackMode;
    private bool inRange;
    private bool coolDown;
    private float intTimer;
    #endregion

    private void Awake()
    {
        intTimer = coolDownTimer; //store initial value of timer
    }

    void Start()
    {
        _enemyAi = GetComponent<EnemyAi>();
        _enemyAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        EnemyAlert();
        //FlipCharacter();

        if (inRange)
        {
            hit = Physics2D.Raycast(rayCast.position, Vector2.left, rayCastLength, rayCastMask);
            RaycastDebugger();
        }

        //player is in range
        if (hit.collider != null)
        {
            PerformAttack();
        }
        else if (hit.collider == null)
        {
            inRange = false;
        }

        if (!inRange) 
        {
            _enemyAnimator.SetBool("IsAttacking", false);
            _enemyAnimator.SetBool("Moving", true);
        }
    }

    void PerformAttack() 
    {
        _enemyAnimator.SetBool("Moving", false);

        if (CheckForDistance(attackDistance) && !coolDown && !_enemyAnimator.GetBool("Dead"))
        {
            Attack();
        }

        if (coolDown)  
        {
            CoolDown();
            _enemyAnimator.SetBool("IsAttacking", false);
        }
    }

    void Attack() 
    {
        coolDownTimer = intTimer;
        _enemyAnimator.SetBool("IsAttacking", true);
        _enemyAnimator.SetBool("Moving", false);
    }

    void ApplyDamageToPlayer() 
    {
        gameManager.ApplyHealthChanges(_enemyAi._enemyStats.AttackDamage1);
    }

   public void TriggerCooling() 
    {
        coolDown = true;
        ApplyDamageToPlayer();
    }

    void CoolDown() 
    {
        coolDownTimer -= Time.deltaTime;

        if (coolDownTimer <= 0 && coolDown) 
        {
            coolDown = false;
            coolDownTimer = intTimer;
        }
    }

    bool CheckForDistance(float input)
    {
       return Vector2.Distance(transform.position, _enemyAi.Target.transform.position) <= input;
    }

    void EnemyAlert()
    {
        if (CheckForDistance(enemySpotDistance))
        {
            _enemyAi.enabled = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<CapsuleCollider2D>());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player")) 
        {
            inRange = true;
        }
    }

    void RaycastDebugger() 
    {
        var distance = Vector2.Distance(transform.position, _enemyAi.Target.transform.position);
        if (distance > attackDistance)
        {
            Debug.DrawRay(rayCast.position, Vector2.left * rayCastLength, Color.red);
        }
        else { Debug.DrawRay(rayCast.position, Vector2.left * rayCastLength, Color.green); }
    }

    public void MoveToFloor() 
    {
        transform.position = transform.position - new Vector3(0, 0.75f, 0);
    }

    #region flip character (just for reference)
    /// <summary>
    /// Flips the melee point of the character for attacks
    /// </summary>
    //void FlipCharacter()
    //{
    //    Vector2 localScale = transform.localScale;

    //    if (_enemyAi.Target.position.x < transform.position.x)
    //    {
    //        MeleePoint.transform.position = transform.position - new Vector3(0.8f, 0,0);
    //    }

    //    if (_enemyAi.Target.position.x > transform.position.x)
    //    {
    //        MeleePoint.transform.position = transform.position + new Vector3(0.8f, 0, 0);
    //    }

    //    transform.localScale = localScale;
    //}
    #endregion
}
