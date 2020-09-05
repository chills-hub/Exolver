using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayerHere : MonoBehaviour
{
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;

        if (player != null)
        {
            player.transform.position = this.transform.position;
        }
        else 
        {
            throw new MissingComponentException("No player in scene");
        }
    }
}
