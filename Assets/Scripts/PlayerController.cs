using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using UnityEngine.XR;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    
    [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Jumping")]
    public float jumpPower = 10f;
    public int maxJumps = 2;
    int jumpsRemaining;

    [Header("Ground Check")]
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask groundLayer;

    [Header("Gravity")]
    public float baseGravity = 2f;
    public float maxFallSpeed = 10f;
    public float fallSpeedMultiplier = 2f;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

    }

    public void Jump(InputAction.CallbackContext context)
    {
            if (context.performed && jumpsRemaining > 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
                jumpsRemaining--;
                animator.SetTrigger("jump");
            }

            else if (context.canceled && rb.linearVelocity.y > 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
                //animator.SetTrigger("jump");
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);

        if (moveInput.x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveInput.x), 1, 1);
        }

        isGrounded();
        Gravity();

        animator.SetFloat("yVelocity", rb.linearVelocity.y);
        animator.SetFloat("magnitude", Mathf.Abs(rb.linearVelocity.x));

    }

    private void Update()
    {


    }

    private void Gravity()
    {
        if(rb.linearVelocity.y > 0)
        {
            rb.gravityScale = baseGravity * fallSpeedMultiplier;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Max(rb.linearVelocity.y, -maxFallSpeed));
        }
        else
        {
            rb.gravityScale = baseGravity;
        }
    }

    private void isGrounded()
    {
        if (Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer) && rb.linearVelocity.y <=0.01f)
        {
            jumpsRemaining = maxJumps;
            animator.SetBool("isGrounded", true);
        }
        else
        {
            animator.SetBool("isGrounded", false);
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
    }
}
