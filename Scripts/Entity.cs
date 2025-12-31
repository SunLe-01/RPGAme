using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] protected Vector2 knockbackDirection;
    [SerializeField] protected float knockbackDuration;

    [Header("Collision info")] public Transform attackCheck;

    public float attackCheckRadius;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;
    protected bool facingRight = true;


    [Header("Knockback info")] protected bool isKnockback;

    public System.Action onFlipped;
    
    public int facingDir { get; private set; } = 1;

    protected virtual void Awake()
    {
    }

    protected virtual void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        fx = GetComponent<EntityFX>();
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<CharacterStats>();
        cd = GetComponent<CapsuleCollider2D>();
    }

    protected virtual void Update()
    {
    }

    public virtual void SlowEntityBy(float _slowPercentage,float _slowDuration)
    {
        
    }

    protected virtual void ReturnDefaultSpeed()
    {
        anim.speed = 1;
    }

    #region Component

    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public EntityFX fx { get; private set; }
    public SpriteRenderer sr { get; private set; }
    public CharacterStats stats { get; private set; }
    public CapsuleCollider2D cd { get; private set; }
    
    #endregion

    #region velocity

    public void SetZeroVelocity()
    {
        if (isKnockback)
            return;
        rb.velocity = new Vector2(0, 0);
    }


    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        //如果在被击退状态下不能进行其他的移动行为
        if (isKnockback)
            return;
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }

    #endregion

    public virtual void DamageImpact()
    {
        //在收到伤害的同时被击退
        StartCoroutine("knockback"); 
    }
    
    
    #region Cllision


    public virtual IEnumerator knockback()
    {
        isKnockback = true;
        //设定击退方向 * 面朝方向的反方向为新的速度
        rb.velocity = new Vector2(knockbackDirection.x * -facingDir, knockbackDirection.y);
        //并且以新速度移动knockbackDuration时长
        yield return new WaitForSeconds(knockbackDuration);

        isKnockback = false;
    }

    public virtual bool isGroundDetected()
    {
        return Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    }

    public virtual bool isWallDetected()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundCheck.position,
            new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position,
            new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }
    
    #endregion

    #region Flip  
   
    public virtual void Flip()
    {
        facingDir *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);

        if (onFlipped != null)
            onFlipped();
    }

    public virtual void FlipController(float _x)
    {
        if (_x > 0 && !facingRight)
            Flip();
        else if (_x < 0 && facingRight) Flip();
    }

    #endregion



    public virtual void Die()
    {
        
    }
}