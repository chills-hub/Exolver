using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFollowPlayer : MonoBehaviour
{
    private PlayerController Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(Player.transform.position.x, transform.position.y);
    }
}
