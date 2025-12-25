using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField] protected float Cooldown;
    protected float CooldownTimer;

    protected Player player;

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
        if (CooldownTimer <= 0)
        {
            UseSkill();
            CooldownTimer = Cooldown;
            return true;
        }

        Debug.Log("Skill is on cooldown");

        return false;
    }

    public virtual void UseSkill()
    {
        //技能具体代码
    }
}