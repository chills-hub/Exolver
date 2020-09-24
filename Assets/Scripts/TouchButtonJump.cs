using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchButtonJump : MonoBehaviour
{
    [HideInInspector]
    public bool jump;

    public void jumpTrueEvent()
    {
        jump = true;
    }

    public void jumpFalseEvent()
    {
        jump = false;
    }
}
