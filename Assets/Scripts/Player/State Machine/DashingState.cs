using UnityEngine;

public class DashingState : State
{

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

        if (!_playerController.GetComponent<InputManager>().Dash())
        {
            _stateMachine.ChangeState(_playerController.standing);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();      
    }

    private void Dash()
    {
    }

    //private void AimAtReticle()
    //{
        //_playerController.PlayerAimArrow.SetActive(true);

        //Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - _playerController.PlayerAimArrow.transform.position;
        //float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        //Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.back);
        //_playerController.PlayerAimArrow.transform.rotation = Quaternion.Slerp(_playerController.PlayerAimArrow.transform.rotation, rotation, angularSpeed * Time.deltaTime);
    //}

    private void ApplyDashForce(Vector2 input) 
    {
  
    }
}
