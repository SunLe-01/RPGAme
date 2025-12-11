using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBattleState : EnemyState
{
    private Transform player;

    private  Enemy_Skeleton enemy;

    private int moveDir;

    public SkeletonBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Skeleton enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

       //player = GameObject.Find("Player").transform;
       
       //以上注释代码为单例创造前的代码
       player = PlayerMangager.Instance.player.transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (enemy.IsPlayerDetected())
        {
            stateTimer = enemy.battleTime;
            //每次检测到玩家后刷新一次stateTimer为战斗时长
            if (enemy.IsPlayerDetected().distance < enemy.attackDistance)
            {
                if(CanAttack())
                    stateMachine.ChangeState(enemy.attackState);//如果可以攻击，转为attackState
            }
        }
        else
        {
            if (stateTimer< 0||Vector2.Distance(player.position,enemy.transform.position)>10)
            {
                stateMachine.ChangeState(enemy.idleState);
            }
        }

//判断玩家位置和敌人位置之间的关系，从而改变敌人的面朝方向
        if (player.position.x > enemy.transform.position.x)
        {
            moveDir = 1;
        }
        else if (player.position.x < enemy.transform.position.x)
        { 
            moveDir = -1;
        }

        enemy.SetVelocity(enemy.moveSpeed * moveDir, rb.velocity.y);
    }
    
//检查敌人是否可以进行攻击，Time.time是现在的时间，lastTimeAttacked是上一次攻击的时间,attckCooldown是冷却时间
    private bool CanAttack()
    {
        if (Time.time >= enemy.lastTimeAttacked + enemy.attackCooldown)
        { 
            enemy.lastTimeAttacked = Time.time;

            return true;
        }

        Debug.Log("Attack is on CoolDown");

        return false;
    }
}
