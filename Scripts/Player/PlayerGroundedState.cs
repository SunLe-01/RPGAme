using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            stateMachine.ChangeState(player.counterAttack);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            stateMachine.ChangeState(player.primaryAttack);
        }
    
        if(!player.isGroundDetected())
            stateMachine.ChangeState(player.airState);

        if (Input.GetKeyDown(KeyCode.Space) && player.isGroundDetected())
        { 
            stateMachine.ChangeState(player.jumpState);
        }
       
    }
}
