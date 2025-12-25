using UnityEngine;
using UnityEngine.Serialization;

public enum SwordType
{
    Regular,
    Bounce,
    Pierce,
    Spin
}

public class SwordSkill : Skill
{
    public SwordType swordType = SwordType.Regular;
    
    [Header("Spin info")]
    [SerializeField] private float hitCooldown = 0.3f;
    [SerializeField] private float spinDuration  = 2;
    [SerializeField] private float maxTravelDistance = 7;
    [SerializeField] private float spinGravity  = 1;
    
    [FormerlySerializedAs("amountOfBounces")]
    [Header("Bounce info")]
    [SerializeField] private int bounceAmount;
    [SerializeField] private float bounceGravity;
    [SerializeField] private float bounceSpeed;
    
    [Header("SKill info")] 
    [SerializeField] private GameObject swordPrefab;
    [SerializeField] private Vector2 launchForce;
    [SerializeField] private float swordGravity;
    [SerializeField] private float freezeTimeDuration;
    [SerializeField] private float returnSpeed;
    
    [FormerlySerializedAs("amountOfPeirces")]
    [Header("Peirce info")]
    [SerializeField] private int pierceAmount;
    [SerializeField] private float pierceGravity;
    
    [Header("Aim dots")] [SerializeField] private int numberOfDots;
    [SerializeField] private float spaceBetweenDots;
    [SerializeField] private GameObject dotPerfab;
    [SerializeField] private Transform dotsParent;
    private GameObject[] dots;

    private Vector2 finalDir;

    protected override void Start()
    {
        base.Start();

        GenerateDots();

        SetUpGravity();
    }

    private void SetUpGravity()
    {
        if(swordType == SwordType.Bounce)
            swordGravity = bounceGravity;
        else if (swordType == SwordType.Pierce)
            swordGravity = pierceGravity;
        else if (swordType == SwordType.Regular)
            swordGravity = swordGravity; 
        else if(swordType == SwordType.Spin)
            swordGravity = spinGravity;
    }

    private void Update()
    {
        SetUpGravity();
        if (Input.GetKeyUp(KeyCode.Mouse1))
            finalDir = new Vector2(AimDirection().normalized.x * launchForce.x,
                AimDirection().normalized.y * launchForce.y);

        if (Input.GetKey(KeyCode.Mouse1))
            for (var i = 0; i < numberOfDots; i++)
                dots[i].transform.position = DotsPosition(i * spaceBetweenDots);
    }
    
    #region  Aim Region
    public void CreateSword()
    {
        //在玩家位置生成剑的实体
        var newSword = Instantiate(swordPrefab, player.transform.position, transform.rotation);
        //获取剑身上的控制脚本newSwordScript
        var newSwordScript = newSword.GetComponent<SwordSKillConctroller>();

        if (swordType == SwordType.Bounce)
        {
            newSwordScript.SetUpBounce(true,bounceAmount,bounceSpeed);
        }
        else if (swordType == SwordType.Pierce)
        {
            newSwordScript.SetUpPierce(pierceAmount);
        }
        else if (swordType == SwordType.Spin)
        {
            newSwordScript.SetUpSpin(true,maxTravelDistance, spinDuration,hitCooldown);
        }

        //初始化剑的数据：传入之前计算好的最终方向(finalDir)，剑的重力和玩家引用
        newSwordScript.SetUpSword(finalDir, swordGravity,player,freezeTimeDuration,returnSpeed);
        // 通知玩家脚本剑已经生成（通常用于UI更新或状态切换）
        player.AssignNewSword(newSword);
        // 关闭瞄准线
        DotsActive(false);
    }
    
    public Vector2 AimDirection()
    {
        //这个方法用来计算瞄准方位，计算从玩家到鼠标的向量，用于确定发射方向
        Vector2 playerPosition = player.transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var direction = mousePosition - playerPosition;
        return direction;
    }

    public void DotsActive(bool _isActive)
    {
        for (var i = 0; i < numberOfDots; i++) dots[i].SetActive(_isActive);
    }

    private void GenerateDots()
    {
        dots = new GameObject[numberOfDots];
        for (var i = 0; i < numberOfDots; i++)
        {
            dots[i] = Instantiate(dotPerfab, player.transform.position, Quaternion.identity, dotsParent);
            dots[i].SetActive(false);
        }
    }

    private Vector2 DotsPosition(float t)
    {
        // 物理学公式：位移 = 初始位置 + 初始速度 * 时间 + 0.5 * 加速度 * 时间的平方
        // 这里使用 Physics2D.gravity * swordGravity 作为加速度
        var position = (Vector2)player.transform.position + new Vector2(
            AimDirection().normalized.x * launchForce.x,
            AimDirection().normalized.y * launchForce.y) * t + 0.5f * (Physics2D.gravity * swordGravity) * (t * t);
        return position;
    }
    #endregion
}