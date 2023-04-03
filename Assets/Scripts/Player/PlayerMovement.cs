using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private LayerMask groundLayer;

    private float directionX;
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float jumpForce = 6f;

    private bool isFacingRight = true;

    [SerializeField] private Transform groundCheckCollider;
    private float groundCheckRadius = 0.2f;
    private bool isGrounded = false;
    private bool jump;
    [SerializeField] private int jumpCount = 0;
    [SerializeField] private int jumpMax = 2;

    private void Update()
    {
        directionX = Input.GetAxisRaw("Horizontal");

        Jump();
    }

    private void FixedUpdate()
    {
        GroundChecked();
        Movement(directionX, jump);
    }
    private void Movement(float dirX, bool jumpUp)
    {
        #region Move & Run
        float horizontalValue = dirX * moveSpeed * 100 * Time.deltaTime;
        Vector2 targetVelocity = new Vector2(horizontalValue, rb.velocity.y);
        rb.velocity = targetVelocity;

        // flip state
        // getChild(0) -> render from not from parent object
        if (isFacingRight && dirX < 0)
        {   
            spriteRenderer.flipX = true;
            isFacingRight = false;
        }
        else if (!isFacingRight && dirX > 0)
        {
            spriteRenderer.flipX = false;
            isFacingRight = true;
        }
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        animator.SetBool("IsFalling", false);
        #endregion

        // Check  grounded and jump
        # region Jump
        if (isGrounded && jumpUp)
        {
            isGrounded = false;
            jumpUp = false;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        #endregion
    }
    
    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && jumpCount < jumpMax)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jump = true;
            jumpCount++;
            isGrounded = false;
            animator.SetBool("IsJumping", true);
        }
        else if (Input.GetButtonUp("Jump"))
        {
            jump = false;
            animator.SetBool("IsJumping", false);
        }

        if (isGrounded == true)
        {
            jumpCount = 0;
        }
    }

    private void GroundChecked()
    {
        isGrounded = false;

        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadius, groundLayer);

        if (collider2Ds.Length > 0)
        {
            isGrounded = true;
        }
    }
}
