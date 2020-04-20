using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    public float checkerRadius = 0.1f;
    public LayerMask groundMask;
    public LayerMask wallGrabMask;
    public Transform groundChecker;
    public Transform wallCheckerR;
    public Transform wallCheckerL;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    private Rigidbody2D rb;
    private Animator anim;
    private GameManager gm;

    private float position;
    private bool pressJump;
    private bool isJumping;
    private bool onGround;
    private bool onWall;
    private int extraJumps;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        gm = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        onGround = Physics2D.OverlapCircle(groundChecker.position, checkerRadius, groundMask);
        position = Input.GetAxisRaw("Horizontal");
        pressJump = Input.GetButtonDown("Jump");
        isJumping = Input.GetButton("Jump");
        ControlAnimations();
        if (CanJump())
        {
            Jump();
        }
    }

    private void WallGrab()
    {
        onWall = Physics2D.OverlapCircle(wallCheckerR.position, checkerRadius, wallGrabMask) || 
            Physics2D.OverlapCircle(wallCheckerL.position, checkerRadius, wallGrabMask);
        anim.SetBool("onWall", onWall && !isJumping);
        if (onWall && !isJumping)
        {
            extraJumps = 0;
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            
            if (position > 0)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.x, 180f, transform.rotation.z);
            }
            else if (position < 0)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.x, 0f, transform.rotation.z);
            }
        }
    }

    private bool CanJump()
    {
        if(onGround)
        {
            extraJumps = 0;
        }
        return pressJump && extraJumps < 2;
    }

    private void FixedUpdate()
    {
        Movement();
        WallGrab();
        ControlJump();
    }

    private void Movement()
    {
        if(!onWall || OnTheWallAndIsMoving())
        {
            rb.velocity = new Vector2(position * speed, rb.velocity.y);
        }
        Flip();
    }

    private void Flip()
    {
        if(position < 0)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 180f, transform.rotation.z);
        } else if (position > 0)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x, 0f, transform.rotation.z);
        }
    }

    private void ControlAnimations()
    {
        bool isRunning = position != 0;
        anim.SetBool("isRunning", isRunning);
    }

    private void ControlJump ()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !isJumping)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        if(!onGround && !onWall)
        {
            anim.SetBool("jumpingUp", rb.velocity.y > 0);
            anim.SetBool("jumpingDown", rb.velocity.y < 0);
        } else if (onGround || onWall)
        {
            anim.SetBool("jumpingDown", false);
        }
    }

    private void Jump()
    {
        extraJumps++;
        rb.velocity = Vector2.up * jumpForce;
    }

    private bool OnTheWallAndIsMoving()
    {
        return onWall && position != 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile"))
        {
            gm.RestartLevel();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(wallCheckerL.position, checkerRadius);
        Gizmos.DrawWireSphere(wallCheckerR.position, checkerRadius);
        Gizmos.DrawWireSphere(groundChecker.position, checkerRadius);
    }
}
