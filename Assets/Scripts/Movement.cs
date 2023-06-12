using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Move")]
    public Rigidbody2D rb;
    public Animator anim;
    public GameObject currentPlatform;
    public bool GameIsPaused;

    public float horizontal;
    public float vertical;
    public float speed;
    public bool facingRight;

    public bool leftArr;
    public bool rightArr;

    [Header("Jump")]
    public LayerMask ground;
    public bool upArrJump;
    public bool grounded = false;
    public float groundedSkin = 0.05f;
    public float jumpForce;
    public bool holdUpArr;
    public bool jumpNow;

    public Vector2 playerSize;
    public Vector2 boxSize;
    public Vector2 boxOffset;

    public float colliderOffset;

    [Header("Better Jump")]
    public float smallJump;
    public float fallJump;
    public bool slowJump;
    public float slowFallJump;
    public float fall;
    public float fallSpeed;
    public float gravityScale = 1f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        colliderOffset = 0.02f;
        playerSize = GetComponent<BoxCollider2D>().size - Vector2.one * colliderOffset; 
        boxOffset = GetComponent<BoxCollider2D>().offset;
        boxSize = new Vector2(playerSize.x, groundedSkin);
        facingRight = true;
        rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        SmootherMovement();

        if (Input.GetKeyDown(KeyCode.UpArrow) && grounded && !GameIsPaused)
        {
            upArrJump = true;
            AudioManager.PlaySound("jump");
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
            if (currentPlatform != null && !GameIsPaused)
            {
                StartCoroutine(DisableCollision());
                AudioManager.PlaySound("FallThrough");
            }

        holdUpArr = Input.GetKey(KeyCode.UpArrow);
        leftArr = Input.GetKey(KeyCode.LeftArrow);
        rightArr = Input.GetKey(KeyCode.RightArrow);
    }

    void FixedUpdate()
    {
        if(!GameIsPaused)
        {
            Move();
            Jump();

            Vector2 boxCenter = (Vector2)transform.position + GetComponent<BoxCollider2D>().offset + Vector2.down * (playerSize.y + boxSize.y) * 0.5f;
            grounded = Physics2D.OverlapBox(boxCenter, boxSize, 0f, ground) && Mathf.Abs(rb.velocity.y) <= 0.2f;

            if (facingRight && horizontal < 0)
                Flip();
            else if (!facingRight && horizontal > 0)
                Flip();
        }

        Animation();
    }

    void Move()
    {
        if (rightArr && leftArr)
            rb.velocity = new Vector2(0f, rb.velocity.y);
        else if (!grounded)
            rb.velocity = new Vector2(horizontal * speed * 0.95f, rb.velocity.y);
        else
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    void SmootherMovement()
    {
        if (leftArr && horizontal > 0f)
            horizontal = 0;
        else if (rightArr && horizontal < 0f)
            horizontal = 0f;
    }

    void Flip()
    {
        facingRight = !facingRight;
        if (!GetComponent<SpriteRenderer>().flipX)
            GetComponent<SpriteRenderer>().flipX = true;
        else if (GetComponent<SpriteRenderer>().flipX)
            GetComponent<SpriteRenderer>().flipX = false;
    }

    void Jump()
    {
        if (grounded && !upArrJump)
            jumpNow = false;
        if (upArrJump)
        {
            rb.velocity = Vector2.up * jumpForce;
            jumpNow = true;
            upArrJump = false;
        }

        if (rb.velocity.y < 0f && !jumpNow)
            rb.gravityScale = fall;
        else if (!slowJump)
        {
            if (!holdUpArr && jumpNow) //small jump
            {
                rb.gravityScale = smallJump; 
            }
        }
        else if (holdUpArr && rb.velocity.y > 0f && jumpNow)
        {
            rb.gravityScale = gravityScale;
        }
        else if (rb.velocity.y < 0f && jumpNow && holdUpArr) //hold during all jump
        {
            rb.gravityScale = slowFallJump;
        }
        else if (!holdUpArr && jumpNow)
        {
            rb.gravityScale = fallJump;
        }
        else if (slowJump)
        {
            if (!holdUpArr && jumpNow)
            {
                rb.gravityScale = fallJump;
            }
        }

        if (vertical > 0.45f)
            vertical = 1;
        if (vertical == 1f)
            slowJump = true;

        if (grounded)
        {
            rb.gravityScale = gravityScale;
            slowJump = false;
        }

        if (rb.velocity.y < -fallSpeed)
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -fallSpeed, Mathf.Infinity));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 9)
            currentPlatform = collision.gameObject;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9)
            currentPlatform = null;
    }

    private IEnumerator DisableCollision()
    {
        BoxCollider2D currentPlat = currentPlatform.GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(currentPlat, GetComponent<BoxCollider2D>());
        yield return new WaitForSeconds(0.5f);
        Physics2D.IgnoreCollision(currentPlat, GetComponent<BoxCollider2D>(), false);
    }

    void Animation()
    {
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("Vertical", rb.velocity.y);
        anim.SetBool("Jump", !grounded);
    }
}
