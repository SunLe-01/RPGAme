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
    }

    protected virtual void Update()
    {
    }

    #region Component

    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public EntityFX fx { get; private set; }
    public SpriteRenderer sr { get; private set; }
    
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

    #region Cllision

    public virtual void damage()
    {
        fx.StartCoroutine("flashFx");
        //在收到伤害的同时被击退
        StartCoroutine("knockback");
        Debug.Log(gameObject.name + " damaged");
    }

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
    }

    public virtual void FlipController(float _x)
    {
        if (_x > 0 && !facingRight)
            Flip();
        else if (_x < 0 && facingRight) Flip();
    }

    #endregion

    public void MakeTransparent(bool _transparent)
    {
        if (_transparent)
            sr.color = Color.clear;
        else
        {
            sr.color = Color.white;
        }
    }
}