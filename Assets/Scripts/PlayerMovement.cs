using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    private Rigidbody2D body;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;
    private float verticalInput;

    [Header("Layers")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private LayerMask objectLayer;
    [SerializeField] private LayerMask brokenLayer;
    [SerializeField] private LayerMask ladderLayer;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Obter o componente SpriteRenderer
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // Ajustar o flip do sprite de acordo com a direção
        if (horizontalInput > 0.01f)
        {
            spriteRenderer.flipX = true;
            animator.SetBool("isWalking", true);
        }
        else if (horizontalInput < -0.01f)
        {
            spriteRenderer.flipX = false;
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
            animator.Play("CAROL_IDLE");
        }

        if (wallJumpCooldown > 0.2f)
        {
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if (onLadder()) {
                if (verticalInput > 0.01f) {
                    body.velocity = new Vector2(body.velocity.x, verticalInput * speed);
                }
                else if (verticalInput < -0.01f) {
                    body.velocity = new Vector2(body.velocity.x, verticalInput * speed);
                }
            }
            else if (!isBroken() && (OnWall() || OnObject()) && !IsGrounded()  )
            {
                if (verticalInput > 0.01f) {
                    body.velocity = new Vector2(body.velocity.x, verticalInput * speed * 0.5f);
                }
                else if (verticalInput < -0.01f) {
                    body.velocity = new Vector2(body.velocity.x, verticalInput * speed * 0.5f);
                }
            }
            else body.gravityScale = 7;

            if (Input.GetKey(KeyCode.Space)) Jump();
        }
        else wallJumpCooldown += Time.deltaTime;
    }

    private void Jump()
    {
        if (IsGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpForce);
            animator.Play("CAROL_JUMP");
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        RaycastHit2D hit2 = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, objectLayer);
        return hit.collider != null || hit2.collider != null;
    }

    private bool OnWall()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(-1, 0), 0.1f, wallLayer);
        RaycastHit2D hit2 = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(1, 0), 0.1f, wallLayer);
        return hit.collider != null || hit2.collider != null;
    }

    private bool OnObject(){
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(-1, 0), 0.1f, objectLayer);
        RaycastHit2D hit2 = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(1, 0), 0.1f, objectLayer);
        return hit.collider != null || hit2.collider != null;
    }

    private bool isBroken()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(-1, 0), 0.1f, brokenLayer);
        RaycastHit2D hit2 = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(1, 0), 0.1f, brokenLayer);
        return hit.collider != null || hit2.collider != null;
    }

    private bool onLadder(){
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(-1, 0), 0.1f, ladderLayer);
        RaycastHit2D hit2 = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(1, 0), 0.1f, ladderLayer);
        return hit.collider != null || hit2.collider != null;
    }


    public void Reset() {
        transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        jumpForce = 20;
        speed = 10;
    }


}
