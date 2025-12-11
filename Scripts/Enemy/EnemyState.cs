using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState 
{
    protected EnemyStateMachine stateMachine;

    protected Enemy enemyBase;//这个enemyBase其实就是一个敌人的本体，它具有敌人的所有的组件例如anim（Animator组件）transform组件等

    protected float stateTimer;

    protected bool triggerCalled;

    protected Rigidbody2D rb;

    private string animBoolName;


    public EnemyState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName)
    {
        this.enemyBase = _enemyBase;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
        
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
