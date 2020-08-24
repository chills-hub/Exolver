using System.Collections;
using UnityEngine;

public class DodgingState : State
{
    public DodgingState(PlayerController playerController, StateMachine stateMachine) : base(playerController, stateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();
        Dodge();
    }

    public override void Exit()
    {
        base.Exit();
        //character.ColliderSize = character.NormalColliderHeight;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!_playerController.GetComponent<InputManager>().Dodge())
        {
            //SoundManager.Instance.PlaySound(SoundManager.Instance.landing);
            _stateMachine.ChangeState(_playerController.standing);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void Dodge()
    {
        _playerController.isDodging = true;
    }
}
