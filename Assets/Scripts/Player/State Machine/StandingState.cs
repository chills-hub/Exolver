using UnityEngine;
public class StandingState : GroundedState
{
    private bool jump;
    private bool dodge;
    public InputManager input;

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
        standingState.input = playerController._inputManager;
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
        jump = input.PlayerJumpInput();
        dodge = input.Dodge();
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
