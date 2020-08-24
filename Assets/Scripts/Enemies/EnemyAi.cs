using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class EnemyAi : MonoBehaviour
{
    public Transform Target;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public EnemyStats _enemyStats;
    private Animator EnemyAnimator;

    int currentWaypoint = 0;
    bool ReachedEndOfPath;

    private Path _path;
    private Seeker _seeker;
    private Rigidbody2D _enemyBody;
    public Vector2 Force;

    // Start is called before the first frame update
    void Start()
    {
        _enemyBody = GetComponent<Rigidbody2D>();
        _seeker = GetComponent<Seeker>();
        EnemyAnimator = GetComponent<Animator>();
        _enemyStats = GetComponent<EnemyStats>();

        //Start a waypoint path to the player
        InvokeRepeating("UpdatePath", 0f, 0.5f);
        _enemyStats.CurrentHealth = _enemyStats.Health;
    }

    void UpdatePath() 
    {
        if (_seeker.IsDone()) 
        {
            _seeker.StartPath(_enemyBody.position, Target.position, OnPathComplete);
        }
    }

    //Check for error on path generation then update current path to be the next one
    void OnPathComplete(Path path) 
    {
        if (!path.error) 
        {
            _path = path;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_path == null) 
        {
            return;
        }
        if (currentWaypoint >= _path.vectorPath.Count)
        {
            ReachedEndOfPath = true;
            return;
        }
        else 
        {
            ReachedEndOfPath = false;
        }

        //Get direction to next waypoint along path, gives a vector that points from player to enemy
        Vector2 direction = ((Vector2)_path.vectorPath[currentWaypoint] - _enemyBody.position).normalized;
        Force = direction * speed * Time.deltaTime;
        _enemyBody.AddForce(Force);
        float distance = Vector2.Distance(_enemyBody.position, _path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance) 
        {
            //Move to next waypoint in path
            currentWaypoint++;
        }
    }

    private void Update()
    {
        GetComponent<SpriteRenderer>().flipX = Target.position.x < transform.position.x;
    }

    public void EnemyDie()
    {
        EnemyAnimator.SetBool("Dead", true);
        GetComponent<Rigidbody2D>().gravityScale = 10;
        this.enabled = false;
        _seeker.enabled = false;
        StartCoroutine(KillEnemy());
    }

    public void TakeDamage(float damage)
    {
        EnemyAnimator.SetBool("TakeHit", true);
        StartCoroutine(DamageFlash());
        float defenseValue = 100 - _enemyStats.Defence;
        string decimalValue = "0." + defenseValue;
        decimal actualDefenseValue = Convert.ToDecimal(decimalValue);
        float afterDamageHealth = _enemyStats.CurrentHealth - (damage * (float)actualDefenseValue);

        if (afterDamageHealth <= 0)
        {
            EnemyDie();
        }
        else 
        { 
            _enemyStats.CurrentHealth = afterDamageHealth;
        }
    }

    public IEnumerator DamageFlash()
    {
        for (int n = 0; n < 2; n++)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            yield return new WaitForSeconds(0.1f);
            GetComponent<SpriteRenderer>().color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
        EnemyAnimator.SetBool("TakeHit", false);
    }

    public IEnumerator KillEnemy()
    {
        _enemyBody.Sleep();
        _enemyBody.velocity = Vector2.zero;
       yield return new WaitForSeconds(2f);
       Destroy(gameObject);
    }

}
