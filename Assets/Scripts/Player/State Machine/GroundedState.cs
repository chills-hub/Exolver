using UnityEngine;
public class GroundedState : State
{
    protected float _speed;
    private float horizontalInput;

    public GroundedState(PlayerController playerController, StateMachine stateMachine) : base(playerController, stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        horizontalInput = 0.0f;
    }

    public override void Exit()
    {
        base.Exit();
        //playerController.ResetMoveParams();
    }

    public override void HandleInput()
    {
        base.HandleInput();
        horizontalInput = this._playerController._inputManager.PlayerMovementInput().Item1;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        _playerController.ApplyParallaxSpeed();

        if (FindObjectOfType<GameManager>()._gameState != GameState.GameOver && FindObjectOfType<GameManager>()._gameState != GameState.Paused)
        {
            FlipCharacter();
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        _playerController.horizontalInput = horizontalInput;
        _playerController.HandleMovement();
        _playerController.CheckForWalls();
        _playerController.ApplySlopeGrav();
    }

    void FlipCharacter()
    {
        Vector2 localScale = _playerController.transform.localScale;
        //SpriteRenderer render = new SpriteRenderer();
        //render.flipX

        if (horizontalInput < 0)
        {
            localScale.x = -5;
        }

        if (horizontalInput > 0)
        {
            localScale.x = 5;
        }

        _playerController.transform.localScale = localScale;
    }
}
