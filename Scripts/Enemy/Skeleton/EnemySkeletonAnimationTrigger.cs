using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeletonAnimationTrigger : MonoBehaviour
{
    private Enemy_Skeleton enemy => GetComponentInParent<Enemy_Skeleton>();

    private void AnimationTrigger()
    { 
        enemy.AnimationFinishTrigger();
    }

//攻击关键帧执行此方法
    private void AttackTriiger()
    {
        //在玩家的攻击检测半径周围做一个圆形范围的检测
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);
        //遍历检测到的所有的碰撞体
        foreach (var hit in colliders)
        {
            //如果这个碰撞体上带有Enemy的组件
            if (hit.GetComponent<Player>() != null)
                //让这个敌人执行damage（）
                hit.GetComponent<Player>().damage();
        }
    }

    private void OpenCounterWindow() => enemy.OpenCounterAttackWindow();
    
    private void CloseCounterWindow() => enemy.CloseCounterAttackWindow();
}
