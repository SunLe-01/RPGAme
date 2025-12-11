using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.iOS;

public class Player : Entity
{
    [Header("Attack details")]
    public Vector2[] attackMovement;

    public float counterAttackDuration = .2f;
    public float t;
    public bool isBusy { get; private set; }
    [Header ("Move info")]
    [SerializeField] public float moveSpeed = 10f;
    public float jumpForce ;

    [Header("Dash info")]
    public float dashSpeed;
    public float dashDuration;
    public float dashDir { get; private set; }

    public SkillManager skill { get; private set; }

    #region State
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdle idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallSlideState wallSlide { get; private set; }
    public PlayerWallJumpState wallJump { get; private set; }
    public PlayerPrimaryAttackState primaryAttack { get; private set; }
    public PlayerCounterAttackState counterAttack { get; private set; }

    #endregion
    protected override void Awake()
    {
        base .Awake();
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdle(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState  = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlide = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJump = new PlayerWallJumpState(this, stateMachine,"Jump");
        primaryAttack = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
        counterAttack = new PlayerCounterAttackState(this, stateMachine, "CounterAttack");

    }   

    protected override void Start()
    {
        base.Start();

        skill = SkillManager.Instance;

        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
        CheckForDashInput();
       
    }

    public IEnumerator BusyFor(float _seconds)
    { 
        isBusy = true;

        yield return new WaitForSeconds(_seconds);

        isBusy = false;
    }

    public void Animationntrigger() => stateMachine.currentState.AnimationFinishTrigger();
    private void CheckForDashInput()
    {
        if (isWallDetected())
            return;
        
        if (Input.GetKeyDown(KeyCode.LeftShift) && SkillManager.Instance.dash.CanUseSkill())
        { 

            dashDir = Input.GetAxisRaw("Horizontal");

            if(dashDir == 0)
                dashDir = facingDir;

            stateMachine.ChangeState(dashState);
        }
    }
  
}


