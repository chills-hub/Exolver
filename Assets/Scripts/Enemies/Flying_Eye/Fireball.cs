using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private float speed = 6f;
    public float Damage;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 directionToTarget = FindObjectOfType<PlayerController>().transform.position - transform.position;
        Vector2 target = directionToTarget + new Vector2(0, 0.5f);
        transform.GetComponent<Rigidbody2D>().AddForce(target * speed * Time.deltaTime, ForceMode2D.Impulse);
        FlipCharacter();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);

        if (collision.gameObject.CompareTag("Player")) 
        {
            FindObjectOfType<GameManager>().ApplyHealthChanges(Damage);
        }
    }

    void FlipCharacter()
    {
        //FIX THIS
    }
}
