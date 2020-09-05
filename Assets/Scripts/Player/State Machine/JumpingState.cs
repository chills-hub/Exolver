using UnityEngine;
public class JumpingState : State
{
    public JumpingState(PlayerController playerController, StateMachine stateMachine) : base(playerController, stateMachine)
    {
    }

    public static JumpingState CreateJumpingState(PlayerController playerController, StateMachine stateMachine)
    {
        GameObject jumping = new GameObject();
        JumpingState jumpingState = jumping.AddComponent<JumpingState>();
        jumpingState._playerController = playerController;
        jumpingState._stateMachine = stateMachine;
        return jumpingState;
    }

    public override void Enter()
    {
        base.Enter();
        Jump();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        _playerController.CheckForWalls();

        if (_playerController.isGrounded)
        {
            _stateMachine.ChangeState(_playerController.standing);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void Jump()
    {

        _playerController.PlayerBody.velocity = Vector2.up * _playerController.jumpForce;

        //if (_playerController.PlayerBody.velocity.y < 0)
        //{
        //    _playerController.PlayerBody.velocity += Vector2.up * Physics2D.gravity.y * (_playerController.fallMultiplier - 1);
        //}
        //else if (_playerController.PlayerBody.velocity.y > 0 && !_playerController._inputManager.PlayerJumpInput())
        //{
        //    _playerController.PlayerBody.velocity += Vector2.up * Physics2D.gravity.y * (_playerController.lowJumpMultiplier - 1);
        //}
    }
}
