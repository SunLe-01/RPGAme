using UnityEngine;

public class Enemy_Skeleton : Enemy
{
    protected override void Awake()
    {
        base.Awake();

        idleState = new SkeletonIdleState(this, stateMachine, "idle", this);
        moveState = new SkeletonMoveState(this, stateMachine, "move", this);
        battleState = new SkeletonBattleState(this, stateMachine, "move", this);
        attackState = new SkeletonAttackState(this, stateMachine, "Attack", this);
        stunnedState = new SkeletonStunnedState(this, stateMachine, "stunned", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.U)) stateMachine.ChangeState(stunnedState);
    }

    public override bool CanBeStunned()
    {
        if (base.CanBeStunned())
        {
            stateMachine.ChangeState(stunnedState);
            return true;
        }

        return false;
    }

    #region States

    public SkeletonIdleState idleState { get; private set; }

    public SkeletonMoveState moveState { get; private set; }

    public SkeletonBattleState battleState { get; private set; }

    public SkeletonAttackState attackState { get; private set; }

    public SkeletonStunnedState stunnedState { get; private set; }

    #endregion
}