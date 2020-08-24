using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashingState : State
{
    protected float dashForce;

    public DashingState(PlayerController playerController, StateMachine stateMachine) : base(playerController, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Dash();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!_playerController.GetComponent<InputManager>().Fire2())
        {
            //SoundManager.Instance.PlaySound(SoundManager.Instance.landing);
            _stateMachine.ChangeState(_playerController.standing);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void Dash()
    {        
        //Hold button, set timescale
        //Aim while in slow motion
        //release button to dash through enemies

        if (_playerController.GetComponent<InputManager>().Fire2()) 
        {
            Time.timeScale = 0.5f;
        }
    }
}
