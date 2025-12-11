using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance;

    //冲刺技能
    public DashSkill dash { get; private set; }
    //克隆技能
    public CloneSkill clone { get; private set; }

    private void Awake()
    {
        if(Instance != null)
            Destroy(Instance.gameObject);
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        dash = GetComponent<DashSkill>();
        clone = GetComponent<CloneSkill>();
    }
}
