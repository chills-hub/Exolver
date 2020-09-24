using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchButtonInteract : MonoBehaviour
{
    [HideInInspector]
    public bool interact;

    public void InteractTrueEvent()
    {
        interact = true;
    }

    public void InteractFalseEvent()
    {
        interact = false;
    }
}
