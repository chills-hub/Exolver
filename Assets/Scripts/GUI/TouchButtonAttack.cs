using UnityEngine;

public class TouchButtonAttack : MonoBehaviour
{
    [HideInInspector]
    public bool attack;

    public void AttackTrueEvent()
    {
        attack = true;
    }

    public void AttackFalseEvent()
    {
        attack = false;
    }
}
