using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField] protected float Cooldown;      //大冷却时长
    protected float CooldownTimer;                  //当前冷却时间（秒）
    
    protected Player player;                        //玩家引用

    protected virtual void Start()
    {
        player = PlayerMangager.Instance.player;
    }

    protected virtual void Update()
    {
        CooldownTimer -= Time.deltaTime;
    }

    public virtual bool CanUseSkill()
    {
        //这是所有技能的入口，在skill的update中cooldownTimer一直在减少，当冷却时间小于零时即可触发UseSkill
        if (CooldownTimer <= 0)
        {
            UseSkill();                 //冷却好了，执行具体的功能
            CooldownTimer = Cooldown;   //再次进入冷却
            return true;                
        }

        Debug.Log("Skill is on cooldown");

        return false;
    }

    public virtual void UseSkill()
    {
        //技能具体代码
    }

    protected virtual Transform FindClosestEnemy(Transform _checkTransform)
    {
        Transform closestEnemy = null;
        
        //以克隆体的位置为中心生成一个25单位为半径的检测圆圈
        var colliders = Physics2D.OverlapCircleAll(_checkTransform.position, 25);

        var closestDistance = Mathf.Infinity;
        //对于每个在检测区域中的物体进行遍历
        foreach (var hit in colliders)
            if (hit.GetComponent<Enemy>() != null)
            {
                //计算克隆体到一个敌人的距离
                var distanceToEnemy = Vector2.Distance(_checkTransform.position, hit.transform.position);
                //如果计算出的距离要小于之前得到的最小距离则将该敌人设定为最近敌人
                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = hit.transform;
                }
            }

        return closestEnemy;
    }
}