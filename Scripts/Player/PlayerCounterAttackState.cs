using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCounterAttackState : PlayerState
{
    public PlayerCounterAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = player.counterAttackDuration;
        player.anim.SetBool("SuccessfulCounterAttack",false);
        
    }

    public override void Update()
    {
        base.Update();
        
        player.SetZeroVelocity();
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);
        //遍历检测到的所有的碰撞体
        foreach (var hit in colliders)
        {
            //如果这个碰撞体上带有Enemy的组件
            if (hit.GetComponent<Enemy>() != null)
            {
                if (hit.GetComponent<Enemy>().CanBeStunned())
                {
                    stateTimer = player.counterAttackDuration;//只要是一个大于1的数字即可
                    player.anim.SetBool("SuccessfulCounterAttack",true);
                }
            }
        }

        if (stateTimer < 0 || triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
