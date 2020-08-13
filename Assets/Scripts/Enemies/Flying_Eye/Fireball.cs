using System.Collections;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private float speed = 6f;
    public float Damage;

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
        if (!collision.gameObject.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<CircleCollider2D>());
        }

        if (collision.gameObject.CompareTag("Player")) 
        {
            FindObjectOfType<GameManager>().ApplyHealthChanges(Damage);
            Destroy(gameObject);
        }

        StartCoroutine(FireballLifetime());
    }

    void FlipCharacter()
    {
        if (GetComponent<Rigidbody2D>().velocity.x >= 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else 
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public IEnumerator FireballLifetime()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
