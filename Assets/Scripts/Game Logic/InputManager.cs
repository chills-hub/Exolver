using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InputManager: MonoBehaviour 
{
    public Joystick joystick;
    public Button AttackButton;
    public Button InteractButton;
    public Button JumpButton;
    public Button DodgeButton;
    public Button PauseButton;

    private void Start()
    {
        //DontDestroyOnLoad(this);
        //joystick = FindObjectOfType<Joystick>();
    }

    public (float,float) PlayerMovementInput()   
    {
        //float xInput = Input.GetAxisRaw("Horizontal");
        //float yInput = Input.GetAxisRaw("Vertical");
        float xInput = joystick.Horizontal;
        float yInput = joystick.Vertical;
        return (xInput, yInput);
    }

    public bool GetInteractionInput() 
    {
      //return Input.GetKeyDown(KeyCode.W);
        return InteractButton.GetComponent<TouchButtonInteract>().interact;
    }

    public bool PlayerJumpInput()
    {
        // bool jumped = false;

        //if (Input.GetButtonDown("Jump")) 
        //{
        //    jumped = true;
        //}

        return JumpButton.GetComponent<TouchButtonJump>().jump;
    }

    public bool Dodge()
    {
        //return Input.GetButtonDown("Sprint");
        return DodgeButton.GetComponent<TouchButtonDodge>().dodge;
    }

    public bool Fire1() 
    {
        //return Input.GetMouseButtonDown(0);
        return AttackButton.GetComponent<TouchButtonAttack>().attack;
    }

    public bool Pause()
    {
        //return Input.GetKeyDown(KeyCode.P);   
        return PauseButton.GetComponent<TouchButtonPause>().pause;
    }

    public bool Escape() 
    {
        return Input.GetKeyDown(KeyCode.Escape);
    }
}
