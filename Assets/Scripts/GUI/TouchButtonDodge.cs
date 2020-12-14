using UnityEngine;

public class TouchButtonDodge : MonoBehaviour
{
    [HideInInspector]
    public bool dodge;

    public void dodgeTrueEvent()
    {
        dodge = true;
    }

    public void dodgeFalseEvent()
    {
        dodge = false;
    }
}
