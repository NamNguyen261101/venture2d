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

    [SerializeField] private int jumpCount = 1;
    [SerializeField] private int maxJump = 1;

    private void Update()
    {
        directionX = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            //rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jump = true;
            animator.SetBool("IsJumping", true);
        }   else if (Input.GetButtonUp("Jump"))
        {
            jump = false;
            animator.SetBool("IsJumping", false);
            // animator.SetBool("IsFalling", false);
        }

        

        // AnimationState();
    }

    private void FixedUpdate()
    {
        GroundChecked();
        Movement(directionX, jump);
    }
    private void Movement(float dirX, bool jumpUp)
    {
        #region Move & Run
        // rb.velocity = new Vector2(directionX * moveSpeed, rb.velocity.y);
        float horizontalValue = dirX * moveSpeed * 100 * Time.deltaTime;
        Vector2 targetVelocity = new Vector2(horizontalValue, rb.velocity.y);
        rb.velocity = targetVelocity;

        // flip state
        // getChild(0) -> render from not from parent object
        if (isFacingRight && dirX < 0)
        {
            //this.gameObject.transform.localScale = new Vector3(-1, 1, 1);
            spriteRenderer.flipX = true; 
            isFacingRight = false;
        }
        else if (!isFacingRight && dirX > 0)
        {
            // this.gameObject.transform.localScale = new Vector3(1, 1, 1);
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
