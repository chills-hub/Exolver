using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager: MonoBehaviour 
{
    void Update() { }

    public (float,float) PlayerMovementInput()   
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");
        return (xInput, yInput);
    }

    public bool GetInteractionInput() 
    {
        return Input.GetKeyDown(KeyCode.W);
    }

    public bool PlayerJumpInput() 
    {
        bool jumped = false;

        if (Input.GetButtonDown("Jump")) 
        {
            jumped = true;
        }
        return jumped;
    }

    public bool Dodge()
    {
        return Input.GetButtonDown("Sprint");
    }

    public bool Fire1() 
    {
        return Input.GetMouseButtonDown(0);
    }
    public bool Fire2()
    {
        return Input.GetMouseButtonDown(1);
    }

    public bool Pause()
    {
        return Input.GetKeyDown(KeyCode.P);
    }

    public bool Escape() 
    {
        return Input.GetKeyDown(KeyCode.Escape);
    }
}
