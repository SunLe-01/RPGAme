using UnityEngine;

public class PlayerCounterAttackState : PlayerState
{
    private bool canCreateClone;
    
    public PlayerCounterAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        canCreateClone = true;
        stateTimer = player.counterAttackDuration;
        player.anim.SetBool("SuccessfulCounterAttack", false);
    }

    public override void Update()
    {
        base.Update();

        player.SetZeroVelocity();

        var colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

        foreach (var hit in colliders)
        {
            // 如果检测到敌人，并且敌人可以被击晕
            if (hit.GetComponent<Enemy>() != null)
                if (hit.GetComponent<Enemy>().CanBeStunned())
                {
                    stateTimer = player.counterAttackDuration;
                    player.anim.SetBool("SuccessfulCounterAttack", true);

                    if (canCreateClone)
                    {
                        canCreateClone = false;
                        player.skill.clone.CreateCloneOnCounterAttack(hit.transform);  // 修正：传入敌人的Transform
                    }

                    
                }
        }

        if (stateTimer < 0 || triggerCalled) stateMachine.ChangeState(player.idleState);
    }

    public override void Exit()
    {
        base.Exit();
    }
}