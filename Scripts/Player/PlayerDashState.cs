using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //在玩家进行一次冲刺后生成一个克隆体，并且给予当前时刻玩家的transform信息
        player.skill.clone.CreateClone(player.transform);
        
        stateTimer = player.dashDuration;
    }

    public override void Exit()
    {
        base.Exit();

        player.SetVelocity(0, rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();
        if (!player.isGroundDetected() && player.isWallDetected())
            stateMachine.ChangeState(player.wallSlide);


        player.SetVelocity(player.dashDir* player.dashSpeed, 0);
         
        if (stateTimer < 0)
        { 
            stateMachine.ChangeState(player.idleState);
        }
    }
}
