using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    void Update() { }
    public Vector2 Move(float xInput, float speed, float yInput) 
    {
        return new Vector2(xInput * speed, yInput);
    }
}
