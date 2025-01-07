using System;
using UnityEngine;

public class Player:MonoBehaviour
{
    private int jump = 2;
    [SerializeField]
    public float jumpForce = 10f; // Force applied for the jump
    [SerializeField]
    private Rigidbody2D rb;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(jump);
            if(jump > 0){
                Jump();
            }
        }
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        jump -= 1;

    }

    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "Ground"){
            jump = 2;
        }else if (collision.gameObject.tag == "Spike"){
            Debug.Log("hit spike");
            GameManager.instance.playerDie();
        }
    }
}
