using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float runSpeed = 5f;
    public float jumpSpeed = 8f;
    public Animator animator;
    public float vida = 100f;

    public SpriteRenderer spriteRenderer;
    Rigidbody2D rb2D;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (Input.GetKey("d"))
        {
            rb2D.velocity = new Vector2(runSpeed, rb2D.velocity.y);
            spriteRenderer.flipX = false;
            if (CheckGround.isGrounded)
            {
                animator.SetBool("Run", true);
            }
        }
        else if (Input.GetKey("a"))
        {
            rb2D.velocity = new Vector2(-runSpeed, rb2D.velocity.y);
            spriteRenderer.flipX = true;
            if (CheckGround.isGrounded)
            {
                animator.SetBool("Run", true);
            }
        }
        else
        {
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);
            animator.SetBool("Run", false);
        }

        if (Input.GetKey("space") && CheckGround.isGrounded)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed);
            animator.SetBool("Jump", true);
        }

        if (!CheckGround.isGrounded)
        {
            animator.SetBool("Jump", true);
        }

        if (CheckGround.isGrounded)
        {
            animator.SetBool("Jump", false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            Destroy(collision.gameObject); 
        }

        if (collision.gameObject.CompareTag("Spikes"))
        {
            animator.SetBool("Hurt", true); 
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spikes"))
        {
            animator.SetBool("Hurt", false); 
        }
    }

}
