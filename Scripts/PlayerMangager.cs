using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMangager : MonoBehaviour
{
    /// <summary>
    /// 设置单例模式
    /// 保证此类在游戏中永远只有一个实例
    /// 该实例对象不会被不会被销毁
    /// </summary>
    public static  PlayerMangager Instance;
    
    //现在我可以在任何代码文件中通过 PlayerManager.Instance.player 来查看/管理这个player了
    public Player player;

    void Awake()
    {
        //当单例有两个时进行销毁操作
        if(Instance != null)
            Destroy(Instance.gameObject);
        else
            Instance = this;
    }
}
