using System.Collections;
using UnityEngine;

public class CloneSkill : Skill
{
    [Header("Clone info")]
    [SerializeField] private GameObject clonePrefab;
    [SerializeField] private float cloneDuration;

    [Space]
    [SerializeField] private bool canAttack;
    [SerializeField] private bool createCloneOnDashStart;
    [SerializeField] private bool createCloneOnDashOver;
    [SerializeField] private bool canCreateCloneOnCounterAttack;
    [SerializeField] private bool canDuplicateClone;
    
    [Header("clone can duplicate")]
    [SerializeField] private float chanceToDuplicate;

    [Header("Crystal instead of clone")] 
    public bool crystalInsteadOfClone;

    public void CreateClone(Transform clonePosition, Vector3 _offset)
    {
        if (crystalInsteadOfClone)
        {
            SkillManager.Instance.crystal.CreateCrystal();
            SkillManager.Instance.crystal.CurrentCrystalChooseRandomTarget();
            return;
        }

        // 克隆一个prefab实体
        var newClone = Instantiate(clonePrefab);
        // 获取生成的克隆体的位置
        newClone.GetComponent<CloneSkillController>().SetupClone(clonePosition, cloneDuration, canAttack, _offset, FindClosestEnemy(newClone.transform),canDuplicateClone,chanceToDuplicate);
    }

    public void CreateCloneOnDashStart()
    {
        // 在玩家冲刺开始的地方生成一个克隆体
        if (createCloneOnDashStart)
            CreateClone(player.transform, Vector3.zero);
    }

    public void CreateCloneOnDashOver()
    {
        // 在玩家冲刺结束的地方生成一个克隆体
        if (createCloneOnDashOver)
            CreateClone(player.transform, Vector3.zero);
    }

    public void CreateCloneOnCounterAttack(Transform _enemyTransform)
    {
        if (canCreateCloneOnCounterAttack)
        {
            // 这里修改：添加延时并确保克隆体朝向敌人
            StartCoroutine(CreateCloneWithDelay(_enemyTransform, new Vector3(.9f * player.facingDir, 0)));
        }
    }

    private IEnumerator CreateCloneWithDelay(Transform _transform, Vector3 _offset)
    {
        yield return new WaitForSeconds(0.4f);
        CreateClone(_transform, _offset);
    }
}