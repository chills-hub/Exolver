using UnityEngine;
public class StandingState : GroundedState
{
    private bool jump;
    private bool dodge;

    public StandingState(PlayerController playerController, StateMachine stateMachine) : base(playerController, stateMachine)
    {

    }

    //newing up the states caused issues with scene transition, need a static method to get around this, can do constructor work here 
    public static StandingState CreateStandingState(PlayerController playerController, StateMachine stateMachine)
    {
        GameObject standing = new GameObject();
        StandingState standingState = standing.AddComponent<StandingState>();
        standingState._playerController = playerController;
        standingState._stateMachine = stateMachine;
        return standingState;
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
        dodge = _playerController._inputManager.Dodge();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (dodge)
        {
            _stateMachine.ChangeState(_playerController.dodging);
        }
        if (_playerController.canJump && jump && _playerController.isGrounded )
        {
            _stateMachine.ChangeState(_playerController.jumping);
            StartCoroutine(_playerController.JumpCooldown());
        }
    }
}
