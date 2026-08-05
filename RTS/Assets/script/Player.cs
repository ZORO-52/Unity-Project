using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField] private float moveSpeed = 3.5f;
    [SerializeField] private float jumpForce = 8;
    private float xInput;
    private bool faceright = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.UpArrow))
            Jump();
        HandleMovement();
        Handleanimation();
        Handleflip();

    }
    private void Handleanimation()
    {
        bool ismove = rb.linearVelocity.x != 0;
        anim.SetBool("ismove", ismove);
    }
    private void HandleMovement()
    {
        rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y);
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }
    private void Handleflip()
    {
        if (rb.linearVelocity.x > 0 && faceright == false)
            flip();
        else if (rb.linearVelocity.x < 0 && faceright == true)
            flip();
    }

    private void flip()
    {
        transform.Rotate(0, 180, 0);
        faceright = !faceright;
    }

}