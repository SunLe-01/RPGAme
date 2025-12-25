using UnityEngine;

public class CloneSkill : Skill
{
    [Header("Clone info")]
    //这里需要在unity中需要给予Prefab预制体
    [SerializeField]
    private GameObject clonePrefab;

    [SerializeField] private float cloneDuration;

    [Space] [SerializeField] private bool canAttack;

    public void CreateClone(Transform clonePosition,Vector3 _offset)
    {
        //克隆一个prefaab实体
        var newClone = Instantiate(clonePrefab);
        //获取生成的克隆体的位置
        newClone.GetComponent<CloneSkillController>().SetupClone(clonePosition, cloneDuration, canAttack,_offset);
    }
}