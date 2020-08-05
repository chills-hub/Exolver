public class DodgingState : GroundedState
{
    protected bool _dodgeInput;

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

        if (!_dodgeInput)
        {
            //SoundManager.Instance.PlaySound(SoundManager.Instance.landing);
            _stateMachine.ChangeState(_playerController.standing);
        }

        if (_dodgeInput)
        {
            Dodge();
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void Dodge()
    {
        _playerController._animator.SetBool("Dodging", _inputManager.Dodge());
    }
}
