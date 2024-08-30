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
        spriteRenderer = GetComponent<SpriteRenderer>(); 
        onLadder = false;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Ladder")) {
            onLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ladder")) {
            onLadder = false;
        }
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if ((OnWall() || OnObject()) && !isBroken() && !OnGround()) {

            if (Input.GetKey(KeyCode.Space)) {
                
                //Decide which way to jump
                RaycastHit2D wallLeft = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.left, 0.1f, wallLayer);
                RaycastHit2D wallRight = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.right, 0.1f, wallLayer);

                RaycastHit2D boxLeft = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.left, 0.1f, objectLayer);
                RaycastHit2D boxRight = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.right, 0.1f, objectLayer);

                if (wallLeft.collider != null || boxLeft.collider != null) {
                    body.velocity = new Vector2(speed, jumpForce);
                    spriteRenderer.flipX = true;
                }
                else if (wallRight.collider != null || boxRight.collider != null) {
                    spriteRenderer.flipX = false;
                    body.velocity = new Vector2(-speed, jumpForce);
                }

                animator.Play("CAROL_JUMP");

            }

        }

        else {

            if (horizontalInput == 0f) {
                animator.Play("CAROL_IDLE");
            }
            else if (OnGround()){
                animator.SetBool("isWalking", true);
            
                spriteRenderer.flipX = horizontalInput > 0.01f;
                body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            }

            if (Input.GetKey(KeyCode.Space)) {
                
                Debug.Log("Jump");
                if (onLadder) {
                    body.velocity = new Vector2(horizontalInput * speed, jumpForce * 0.2f);
                }
                else if (OnGround()) {
                    body.velocity = new Vector2(horizontalInput * speed, jumpForce);
                }


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

    private bool OnObject(){
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.left, 0.1f, objectLayer);
        RaycastHit2D hit2 = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.right, 0.1f, objectLayer);
        return hit.collider != null || hit2.collider != null;
    }

    private bool isBroken()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.left, 0.1f, brokenLayer);
        RaycastHit2D hit2 = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.right, 0.1f, brokenLayer);
        return hit.collider != null || hit2.collider != null;
    }


    public void Reset() {
        transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        jumpForce = 20;
        speed = 10;
    }


}
