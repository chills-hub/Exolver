using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchButtonPause : MonoBehaviour
{
    [HideInInspector]
    public bool pause;
    public EventManager EventManager;

    public void pauseTrueEvent()
    {
        pause = true;
        EventManager.PauseTheGameEvent();
    }

    public void pauseFalseEvent()
    {
        pause = false;
    }
}
