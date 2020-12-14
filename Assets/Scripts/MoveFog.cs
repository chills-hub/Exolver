using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFog : MonoBehaviour
{
    public float fogSpeed = 0.01f;
    private float endSpeed = 1f;
    private float multiplier = 0.000001f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MoveDelay());
    }

    // Update is called once per frame
    void Update()
    {
        IncreaseFogSpeed();
        transform.Translate(Vector2.right * fogSpeed);
    }

    private IEnumerator MoveDelay() 
    {
        yield return new WaitForSeconds(3f);
    }

    private void IncreaseFogSpeed() 
    {
        fogSpeed = Mathf.Lerp(fogSpeed, endSpeed, multiplier);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            FindObjectOfType<PlayerController>().InFog = true;
        }

        if (collision.transform.tag == "DeleteBlock")
        {
            Destroy(collision.transform.parent.gameObject);
        }
    }
}
