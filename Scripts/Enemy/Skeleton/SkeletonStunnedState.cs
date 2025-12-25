using UnityEngine;

public class SkeletonStunnedState : EnemyState
{
    public Enemy_Skeleton enemy;

    public SkeletonStunnedState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName,
        Enemy_Skeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
        {
            enemy.SetZeroVelocity();
            stateMachine.ChangeState(enemy.idleState);
        }
    }

    public override void Enter()
    {
        base.Enter();

        enemy.fx.InvokeRepeating("RedcolorBlink", 0, 0.1f);

        stateTimer = enemy.stunnedDuration;

        rb.velocity = new Vector2(-enemy.stunnedDirection.x * enemy.facingDir, enemy.stunnedDirection.y);
    }


    public override void Exit()
    {
        base.Exit();

        enemy.fx.Invoke("CancelRedBlink", 0);
    }
}