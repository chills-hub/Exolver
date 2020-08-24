using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //State Machine Stuff
    public StateMachine movementSM;
    public StandingState standing;
    public DodgingState dodging;
    public JumpingState jumping;
    public DashingState dashing;

    //acessory injected instances
    public InputManager _inputManager;
    public Rigidbody2D PlayerBody;   
    public Animator _animator;
    private FreeParallax _parallax;
    private PlayerMovement _playerMovement;
    private Vector2 _movement;
    private CapsuleCollider2D _playerCollider;
    private InteractionHelper _interactionHelper;
    private AudioSource _audioSource;

    //Inputs
    public float horizontalInput;
    public float verticalInput;
    private bool _attackInput;

    //Player Variables
    public bool isMoving;
    public bool isGrounded;
    public bool isAttacking;
    public bool isDodging;
    public float slopeCheckAngle;
    public float currentHealth;
    public Transform AttackPoint;
    public float attackRange = 0.5f;

    //Layer Masks
    public LayerMask groundMask;
    public LayerMask playerMask;
    public LayerMask enemyMask;

    public float jumpForce = 1000f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float moveSpeed = 10f;

    public PlayerStats PlayerStats { get; set;}

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = PlayerStats.MaxHealth;
        movementSM = transform.gameObject.AddComponent<StateMachine>();
        _inputManager = transform.gameObject.AddComponent<InputManager>();
        PlayerBody = transform.gameObject.GetComponent<Rigidbody2D>();
        _animator = transform.gameObject.GetComponent<Animator>();
        _playerMovement = transform.gameObject.AddComponent<PlayerMovement>();
        _playerCollider = transform.gameObject.GetComponent<CapsuleCollider2D>();
        _interactionHelper = transform.gameObject.AddComponent<InteractionHelper>();
        _parallax = FindObjectOfType<Camera>().GetComponentInChildren<FreeParallax>();
        _audioSource = transform.gameObject.GetComponent<AudioSource>();
        standing = new StandingState(this, movementSM);
        dodging = new DodgingState(this, movementSM);
        jumping = new JumpingState(this, movementSM);
        dashing = new DashingState(this, movementSM);
        movementSM.Initialise(standing);
        _audioSource.Play();
        _audioSource.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        movementSM.CurrentState.HandleInput();
        movementSM.CurrentState.LogicUpdate();
        movementSM.CurrentState.PhysicsUpdate();

        #region inputs
        horizontalInput = _inputManager.PlayerMovementInput().Item1;
        verticalInput = _inputManager.PlayerMovementInput().Item2;
        _attackInput = _inputManager.Fire1();
        #endregion

        isGrounded = CheckIfGrounded();
        slopeCheckAngle = CheckSlopeAngle();

        //Debug.Log(isDodging);

        if (_attackInput && !isAttacking)
        {
            isAttacking = true;
            isMoving = false;
            StartCoroutine(AttackWait());
            HandleAttacking();
        }

        if (isDodging) 
        {
            StartCoroutine(DodgeWait());
        }

        SetAnimationTriggers();
    }

    public void HandleMovement()
    {
        _movement = _playerMovement.Move(horizontalInput, moveSpeed, PlayerBody.velocity.y);
        CheckIfMoving();
        if (_movement != null)
        {
            _animator.SetBool("Moving", isMoving);
            PlayerBody.velocity = _movement;

            if (isAttacking)
            {
                PlayerBody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                _parallax.Speed = 0.0f;
            }
        }
    }

    void HandleAttacking()
    {
        if (currentHealth >= 0)
        {
            int index = UnityEngine.Random.Range(1, 4);
            _animator.Play("Player_Attack0" + index);
        }

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, attackRange, enemyMask);

        foreach (Collider2D collider in hitEnemies)
        {
            if (collider.CompareTag("Projectile")) 
            {
                Destroy(collider.gameObject);
            }

            if (!collider.CompareTag("Projectile")) 
            {
                collider.GetComponent<EnemyAi>().TakeDamage(PlayerStats.Damage);
                var force = transform.position - collider.transform.position;
                force.Normalize();
                collider.GetComponent<Rigidbody2D>().AddForce(-force * 600f);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(AttackPoint.transform.position, attackRange);
    }

    bool CheckIfGrounded()
    {
        RaycastHit2D groundRay = Physics2D.Raycast(_playerCollider.bounds.center, Vector2.down, _playerCollider.bounds.extents.y + 0.1f);
        //Debug.DrawRay(_playerCollider.bounds.center, Vector2.down * (_playerCollider.bounds.extents.y + 0.1f));
        bool isGroundedLocal = groundRay.collider.IsTouchingLayers(groundMask);
        //Debug.Log(isGroundedLocal);
        return isGroundedLocal;
    }

    public void CheckForWalls()
    {
        int playerLayer = 9;
        int layerMask = ~(1 << playerLayer); //Exclude layer 9 as this is the player

        RaycastHit2D wallRayLeft = Physics2D.CircleCast(_playerCollider.bounds.center, 0.5f, Vector2.left, _playerCollider.bounds.extents.y + 0.1f, layerMask);
        RaycastHit2D wallRayRight = Physics2D.CircleCast(_playerCollider.bounds.center, 0.5f, Vector2.right, _playerCollider.bounds.extents.y + 0.1f, layerMask);
        Debug.DrawRay(_playerCollider.bounds.center, Vector2.left, Color.red, _playerCollider.bounds.extents.y + 0.1f);
        Debug.DrawRay(_playerCollider.bounds.center, Vector2.right, Color.red, _playerCollider.bounds.extents.y + 0.1f);

        if (wallRayLeft || wallRayRight)
        {
            _parallax.Speed = 0;
        }
    }

    float CheckSlopeAngle()
    {
        float slopeAngle;
        Vector2 slopeRayNormal = Physics2D.Raycast(transform.position, Vector2.down, _playerCollider.bounds.extents.y + 0.1f).normal;
        Debug.DrawRay(transform.position, Vector2.down * (_playerCollider.bounds.extents.y + 0.1f));
        slopeAngle = Vector2.Angle(slopeRayNormal, Vector2.up);
        //Debug.Log(slopeAngle);
        return slopeAngle;
    }

    public void ApplySlopeGrav()
    {
        if (slopeCheckAngle > 0)
        {
            PlayerBody.gravityScale = 8f;
        }

        if (slopeCheckAngle > 0 && !isMoving)
        {
            PlayerBody.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }

        if (slopeCheckAngle > 0 && isMoving)
        {
            PlayerBody.constraints = RigidbodyConstraints2D.FreezeRotation;
            PlayerBody.AddForce(Vector2.up * 5, ForceMode2D.Force);
        }

        else if (slopeCheckAngle == 0)
        {
            PlayerBody.gravityScale = 3f;
            PlayerBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    public void ApplyParallaxSpeed()
    {
        if (horizontalInput > 0 && isMoving)
        {
            _parallax.Speed = -moveSpeed * 0.5f;
        }
        else if (horizontalInput < 0 && isMoving)
        {
            _parallax.Speed = moveSpeed * 0.5f;
        }
        else
        {
            _parallax.Speed = 0.0f;
        }
    }

    #region Coroutines
    IEnumerator AttackWait()
    {
        isMoving = false;
        if (isGrounded && isAttacking)
        {
            CheckIfMoving();
            yield return new WaitForSeconds(0.4f);
        }
        PlayerBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        isAttacking = false;
    }

    public IEnumerator DamageFlash()
    {
        for (int n = 0; n < 2; n++)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            yield return new WaitForSeconds(0.1f);
            GetComponent<SpriteRenderer>().color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator DodgeWait()
    {
        yield return new WaitForSeconds(0.8f);
        isDodging = false;
    }
    #endregion

    #region Helper Methods
    void CheckIfMoving()
    {
        if (horizontalInput != 0)
        {
            isMoving = true;
            _audioSource.UnPause();
        }
        else
        {
            isMoving = false;
            _audioSource.Pause();
        }
    }

    void SetAnimationTriggers() 
    {
        _animator.SetBool("Jumping", _inputManager.PlayerJumpInput());
        _animator.SetBool("Grounded", isGrounded);
        _animator.SetBool("Dodging", _inputManager.Dodge());
    }
    #endregion
}
