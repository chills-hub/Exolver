using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAi : MonoBehaviour
{
    public Transform Target;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;

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

        //Start a waypoint path to the player
        InvokeRepeating("UpdatePath", 0f, 0.5f);
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

        if (Force.x >= 0.01f)
        {
            GetComponent<SpriteRenderer>().flipX = false;  
        }
        else if (Force.x <= -0.01f) 
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }
}
