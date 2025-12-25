using UnityEngine;

public class EnemyState
{
    private readonly string animBoolName;

    protected Enemy enemyBase; //这个enemyBase其实就是一个敌人的本体，它具有敌人的所有的组件例如anim（Animator组件）transform组件等

    protected Rigidbody2D rb;
    protected EnemyStateMachine stateMachine;

    protected float stateTimer;

    protected bool triggerCalled;


    public EnemyState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName)
    {
        enemyBase = _enemyBase;
        stateMachine = _stateMachine;
        animBoolName = _animBoolName;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }

    public virtual void Enter()
    {
        triggerCalled = false;

        rb = enemyBase.rb;

        enemyBase.anim.SetBool(animBoolName, true);
    }

    public virtual void Exit()
    {
        enemyBase.anim.SetBool(animBoolName, false);
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}