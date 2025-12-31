using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();

    private void AnimationTrigger()
    {
        player.Animationntrigger();
    }

//攻击关键帧执行此方法
    private void AttackTriiger()
    {
        //在玩家的攻击检测半径周围做一个圆形范围的检测
        var colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);
        //遍历检测到的所有的碰撞体
        foreach (var hit in colliders)
            //如果这个碰撞体上带有Enemy的组件
            if (hit.GetComponent<Enemy>() != null)
            {
                EnemyStats _target = hit.GetComponent<EnemyStats>();
                player.stats.DoDamage(_target);
            }
    }

//玩家向前丢出宝剑，在关键帧触发
    private void ThrowSword()
    {
        SkillManager.Instance.sword.CreateSword();
    }
}