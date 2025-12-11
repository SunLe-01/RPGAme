using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    private int comboCounter;

    private float lastTimeAttacked;
    private float comboWindow = 2;
    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        xInput = 0;//我们需要这一行代码来修复一个攻击时方向错误的bug

        if (comboCounter > 2 || Time.time >= lastTimeAttacked + comboWindow)
        { 
            comboCounter = 0;
        }

        if (comboCounter == 2) { 
            player.anim.speed = 0.9f;
        }
        player.anim.SetInteger("ComboCounter", comboCounter);

        #region Choose attack direction

        float attackDir = player.facingDir;

        #endregion
        if (xInput != 0)
        { 
            attackDir = xInput;
            }

        player.SetVelocity(player.attackMovement[comboCounter].x * attackDir, player.attackMovement[comboCounter].y);

        stateTimer = .1f;

    }

    public override void Exit()
    {
        base.Exit();

        player.StartCoroutine("BusyFor", .15f);
        player.anim.speed = 1;

        comboCounter++;
        lastTimeAttacked = Time.time;
       
    }
    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
        {
            player.SetZeroVelocity();
        }

        if (triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }
}
