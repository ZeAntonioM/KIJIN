using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    private Rigidbody2D body;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private float horizontalInput;
    private bool onLadder;
    private bool isWallJumping;

    [Header("Layers")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private LayerMask objectLayer;
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        onLadder = false;
        isWallJumping = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            onLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder"))
        {
            onLadder = false;
        }
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (OnWall() && !OnGround())
        {

            if (Input.GetKey(KeyCode.Space))
            {

                //Decide which way to jump
                RaycastHit2D wallLeft = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.left, 0.1f, wallLayer);
                RaycastHit2D wallRight = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.right, 0.1f, wallLayer);

                if (wallLeft.collider != null)
                {
                    body.velocity = new Vector2(speed, jumpForce);
                    spriteRenderer.flipX = true;
                    isWallJumping = true;
                }
                else if (wallRight.collider != null)
                {
                    spriteRenderer.flipX = false;
                    body.velocity = new Vector2(-speed, jumpForce);
                    isWallJumping = true;
                }

                animator.Play("CAROL_JUMP");
                animator.SetBool("isWalking", false);

            }

        }

        else
        {

            if (OnGround())
            {
                isWallJumping = false;
            }

            if (horizontalInput == 0f)
            {
                animator.Play("CAROL_IDLE");
                animator.SetBool("isWalking", false);
            }
            else if (OnGround())
            {
                animator.SetBool("isWalking", true);

                spriteRenderer.flipX = horizontalInput > 0.01f;
            }
            
            // Verifica se o jogador está colidindo com uma parede ou objeto
            RaycastHit2D hitLeft = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.left, 0.1f, wallLayer | objectLayer);
            RaycastHit2D hitRight = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.right, 0.1f, wallLayer | objectLayer);
            RaycastHit2D onGround = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);

            bool isCollidingWithWallOrObject = (hitLeft.collider != null) || (hitRight.collider != null);

            if (!isWallJumping && !isCollidingWithWallOrObject)
            {
                body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
            }
            else if (OnGround())
            {
                body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
            }


            if (Input.GetKey(KeyCode.Space))
            {

                if (onLadder)
                {
                    body.velocity = new Vector2(body.velocity.x, jumpForce * 0.2f);
                }
                else if (OnGround())
                {
                    body.velocity = new Vector2(body.velocity.x, jumpForce);
                }

                animator.SetBool("isWalking", false);

            }

        }
    }



    private bool OnGround()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        RaycastHit2D hit2 = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, objectLayer);
        return hit.collider != null || hit2.collider != null;
    }

    private bool OnWall()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.left, 0.1f, wallLayer);
        RaycastHit2D hit2 = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.right, 0.1f, wallLayer);
        return hit.collider != null || hit2.collider != null;
    }

    public void Reset()
    {
        transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        jumpForce = 20;
        speed = 10;
    }


}