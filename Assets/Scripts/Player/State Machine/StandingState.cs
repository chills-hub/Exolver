using UnityEngine;
public class StandingState : GroundedState
{
    private bool jump;
    private bool dodge;

    public StandingState(PlayerController playerController, StateMachine stateMachine) : base(playerController, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _speed = _playerController.moveSpeed;
        jump = false;
    }

    public override void HandleInput()
    {
        base.HandleInput();
        jump = _playerController._inputManager.PlayerJumpInput();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (dodge)
        {
            _stateMachine.ChangeState(_playerController.dodging);
        }
        if (jump && _playerController.isGrounded)
        {
            _stateMachine.ChangeState(_playerController.jumping);
        }
    }
}
