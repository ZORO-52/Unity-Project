using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [HeaderAttribute("Movement details")]
    [SerializeField] private float moveSpeed = 3.5f;
    [SerializeField] private float jumpForce = 8;
    private float xInput;
    private bool faceright = true;
    [HeaderAttribute("Collision details")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private bool isGrounded;
    [SerializeField] private LayerMask WhatIsGround;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        HandleCollision();
        xInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.UpArrow))
            Jump();
        HandleMovement();
        Handleanimation();
        Handleflip();

    }
    private void Handleanimation()
    {
        
        anim.SetFloat("xvelocity", rb.linearVelocity.x);
        anim.SetFloat("yvelocity", rb.linearVelocity.y);
        anim.SetBool("isgrounded", isGrounded);
    }
    private void HandleMovement()
    {
        rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y);
    }

    private void Jump()
    {
        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }
    private void Handleflip()
    {
        if (rb.linearVelocity.x > 0 && faceright == false)
            flip();
        else if (rb.linearVelocity.x < 0 && faceright == true)
            flip();
    }
    private void HandleCollision()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, WhatIsGround);
    }
    private void flip()
    {
        transform.Rotate(0, 180, 0);
        faceright = !faceright;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance));
    }
}