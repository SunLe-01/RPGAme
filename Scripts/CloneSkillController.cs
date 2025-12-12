using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneSkillController : MonoBehaviour
{
    private SpriteRenderer sr;
    private Animator anim;
    
    //透明度降低的速度
    [SerializeField] private float colorLosingSpeed;
    private float cloneTimer;

    [SerializeField] private Transform attackCheck;
    [SerializeField] private float attackCheckRadius = .8f;
    private Transform closestEnemy;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        //先让克隆体存在一段时间：cloneTimer
        cloneTimer -= Time.deltaTime;
        //当时间结束后执行让克隆体的透明度降低
        if (cloneTimer < 0)
        {
            sr.color = new Color(1,1,1,sr.color.a - (Time.deltaTime * colorLosingSpeed));
            if (sr.color.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetupClone(Transform _newTransform,float _cloneDuration,bool _canAttack)
    {

        if (_canAttack)
        {
            anim.SetInteger("AttackNumber",Random.Range(1,4));
        }

        //获取克隆体的位置
        transform.position = _newTransform.position;
        
        cloneTimer = _cloneDuration;
        
        FaceClosestTarget();
    }

    private void AnimationTrigger()
    {
        cloneTimer = -.1f;
    }
//攻击关键帧执行此方法
    private void AttackTriiger()
    {
        //在玩家的攻击检测半径周围做一个圆形范围的检测
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position, attackCheckRadius);
        //遍历检测到的所有的碰撞体
        foreach (var hit in colliders)
        {
            //如果这个碰撞体上带有Enemy的组件
            if (hit.GetComponent<Enemy>() != null)
                //让这个敌人执行damage（）
                hit.GetComponent<Enemy>().damage();
        }
    }
//这个方法可以实现克隆体面向距离它最近的一个敌人
    private void FaceClosestTarget()
    {
        //以克隆体的位置为中心生成一个25单位为半径的检测圆圈
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 25);

        float closestDistance = Mathf.Infinity;
        //对于每个在检测区域中的物体进行遍历
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {//计算克隆体到一个敌人的距离
                float distanceToEnemy = Vector2.Distance(transform.position, hit.transform.position);
                //如果计算出的距离要小于之前得到的最小距离则将该敌人设定为最近敌人
                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = hit.transform; 
                }
            }
        }
        if (closestEnemy != null)
        {
            //实现转向
            if(transform.position.x > closestEnemy.position.x)
                transform.Rotate(0,180,0);
        }
    }
}
