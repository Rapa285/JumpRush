using System;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int jump = 2; 
    [SerializeField]
    public float jumpForce = 10f; 
    [SerializeField]
    private float moveSpeed = 5f; 
    [SerializeField]
    private float dashForce; 
    [SerializeField]
    private float wallStickTime = 2f; 
    [SerializeField]
    private float wallSlideSpeed = 2f;
    [SerializeField]
    private Rigidbody2D rb;

    private bool isGrounded = false;
    private bool isDashing = false; 
    private bool isTouchingWall = false; 
    private bool isStickingToWall = false; 
    private float stickTimer = 0f; 

    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }

    void Update()
    {
        // movement
        float moveInput = Input.GetAxis("Horizontal");
        if (!isStickingToWall && !isDashing)
        {
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        }

        // jumping and dashing
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isStickingToWall)
            {
                WallJump(moveInput);
            }
            else if (jump > 0)
            {
                if (jump == 1 && Mathf.Abs(moveInput) > 0)
                {
                    Dash(Mathf.Sign(moveInput));
                }
                else
                {
                    Jump();
                }
            }
            Debug.Log("jump left : " + jump);
        }

        // wall sliding
        if (isStickingToWall)
        {
            stickTimer -= Time.deltaTime;
            rb.linearVelocity = new Vector2(0, -wallSlideSpeed); // Slide down slowly

            if (stickTimer <= 0)
            {
                isStickingToWall = false; // Stop sticking after time expires
            }
        }
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        jump -= 1;
    }

    void Dash(float direction)
    {
        Debug.Log("Dashing "+ direction);
        isDashing = true;
        // rb.linearVelocity = Vector2.zero;
        Vector2 dash = new Vector2(direction * 15f, 1f);
        Debug.Log("dash : "+dash);
        Debug.Log("v = "+rb.linearVelocity);
        rb.AddForce(dash,ForceMode2D.Impulse);
        Debug.Log("v = "+rb.linearVelocity);
        // rb.AddForce(new Vector2(direction * 200f, 1f), ForceMode2D.Impulse);
        jump -= 1;
        Invoke("EndDash", 0.2f);
    }

    void WallJump(float direction)
    {
        isStickingToWall = false; 
        jump = 1; 
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(new Vector2(direction * moveSpeed, jumpForce), ForceMode2D.Impulse);
    }

    void EndDash()
    {
        isDashing = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            jump = 2;
            isGrounded = true;

        }
        else if (collision.gameObject.tag == "Wall" && !isGrounded)
        {
            isTouchingWall = true;
            isStickingToWall = true;
            stickTimer = wallStickTime; 
        }
        else if (collision.gameObject.tag == "Spike")
        {
            Debug.Log("Hit spike");
            GameManager.instance.playerDie();
        }
    }

    void OnCollisionStay2D(Collision2D collision){
        if (collision.gameObject.tag == "Orb" && Input.GetMouseButtonDown(0)){
            refreshJump(); 
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            isTouchingWall = false;
            isStickingToWall = false; 
        }

        if(collision.gameObject.tag == "Ground"){
            isGrounded = false;
        }
    }

    public void refreshJump(){
        jump = 2;
    }
}
