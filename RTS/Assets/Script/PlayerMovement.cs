using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;       // Horizontal speed
    [SerializeField] private float jumpForce = 12f;      // Jump strength

    [Header("Ground Check Settings")]
    [SerializeField] private Transform groundCheck;      // Empty GameObject at player's feet
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;      // Layer for ground

    private Rigidbody2D rb;
    private bool isGrounded;
    private float moveInput;

    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (groundCheck == null)
        {
            Debug.LogError("GroundCheck Transform not assigned in Inspector.");
        }
    }

    private void Update()
    {
        // Get horizontal input (-1 to 1)
        moveInput = Input.GetAxisRaw("Horizontal");

        // Check if player is on the ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Jump input
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
        
        float speed = Mathf.Abs(rb.linearVelocity.x);
        animator.SetFloat("speed", speed);

    }

    private void FixedUpdate()
    {
        // Apply horizontal movement
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    // Draw ground check radius in editor
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }

}
