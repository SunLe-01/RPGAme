using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSKillConctroller : MonoBehaviour
{
    [SerializeField] private float returnSpeed = 16;
    private Animator anim;
    private Rigidbody2D rb;
    private CircleCollider2D cd;
    
    private Player player;
    private bool isReturning;
    private bool canRotate = true;
    
    
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<CircleCollider2D>();
        
    }

    public void SetUpSword(Vector2 _dir,Player _player)
    {
        player = _player;
        rb.velocity = _dir;
        //rb.gravityScale = _gravityScale;
        
        anim.SetBool("Rotation", true);
    }

    public void ReturnSword()
    {
        rb.isKinematic = false;
        transform.parent = null;
        isReturning = true;
    }

    private void Update()
    {
        if(canRotate)
            transform.right = rb.velocity;

        if (isReturning)
        {
            transform.position = Vector2.MoveTowards(transform.position,player.transform.position,returnSpeed* Time.deltaTime);
            
            if(Vector2.Distance(transform.position,player.transform.position) <0.5f)
                player.CatchTheSword();
        
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        anim.SetBool("Rotation", false);
        
        canRotate = false;
        cd.enabled = false;

        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        
        transform.parent = collision.transform;
    }
}
 