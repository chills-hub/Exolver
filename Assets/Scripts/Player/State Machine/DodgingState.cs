using System.Collections;
using UnityEngine;

public class DodgingState : State
{
    public DodgingState(PlayerController playerController, StateMachine stateMachine) : base(playerController, stateMachine)
    {
    }
    public static DodgingState CreateDodgingState(PlayerController playerController, StateMachine stateMachine)
    {
        GameObject standing = new GameObject();
        DodgingState dodgingState = standing.AddComponent<DodgingState>();
        dodgingState._playerController = playerController;
        dodgingState._stateMachine = stateMachine;
        return dodgingState;
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
