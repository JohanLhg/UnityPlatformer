using System;
using UnityEngine;

public class PlayerMovementBehavior : MonoBehaviour {
    public Transform RaycastBottom;
    public Transform RaycastBottomRight;
    public Transform RaycastCenterRight;
    public Transform RaycastTopRight;
    public Transform RaycastBottomLeft;
    public Transform RaycastCenterLeft;
    public Transform RaycastTopLeft;
    public LayerMask GroundMask;
    public float Speed;
    public float JumpForce;

    private Rigidbody2D body;
    private SpriteRenderer sprite;
    private Animator animator;

    private static float rayCastSideDistance = 0.25f;
    private static float rayCastJumpDistance = 0.01f;

    private bool hasDoubleJump = true;

    void Start() {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update() {
        bool isWallGrinding = false;

        if (Input.GetKey(KeyCode.LeftShift) && !IsGrounded() && (HasWallOnLeft() || HasWallOnRight())) {
            body.velocityY = -0.1f;
            isWallGrinding = true;
            sprite.flipX = HasWallOnLeft();
        }

        if (body.velocityY < 0) {
            animator.SetBool("isDoubleJumping", false);
        }
        if (IsGrounded() || isWallGrinding) {
            animator.SetBool("isDoubleJumping", false);
            hasDoubleJump = true;
        }

        if (IsGoingRight()) {
            sprite.flipX = false;
            if (!HasWallOnRight()) {
                if (Input.GetKey(KeyCode.LeftShift))
                    body.velocityX = Speed * 2;
                else body.velocityX = Speed;
            }
        }
        else if (IsGoingLeft()) {
            sprite.flipX = true;
            if (!HasWallOnLeft()) {
                if (Input.GetKey(KeyCode.LeftShift))
                    body.velocityX = -Speed * 2;
                else body.velocityX = -Speed;
            }
        }

        if (IsJumping() && (IsGrounded() || isWallGrinding || hasDoubleJump)) {
            // Simple Jump
            if (IsGrounded()) {
                body.AddForceY(JumpForce);
            }
            // Wall Jump
            else if (isWallGrinding) {
                body.AddForceY(JumpForce);
                if (HasWallOnLeft()) body.AddForceX(JumpForce / 2);
                else body.AddForce(Vector2.left * (JumpForce / 2));
            } 
            // Double Jump
            else {
                animator.SetBool("isDoubleJumping", true);
                hasDoubleJump = false;
                body.velocityY = 0;
                body.AddForceY(JumpForce * .8f);
            }
        }

        animator.SetFloat("velocityX", Math.Abs(body.velocityX));
        animator.SetFloat("velocityY", body.velocityY);
        animator.SetBool("isWallGrinding", isWallGrinding);
    }

    private bool IsGoingRight() {
        return Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
    }

    private bool IsGoingLeft() {
        return Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
    }

    private bool IsJumping() {
        return Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space);
    }

    private bool HasWallOnRight() {
        RaycastHit2D hitRightBottom = Physics2D.Raycast(RaycastBottomRight.position, Vector2.right, rayCastSideDistance, GroundMask);
        RaycastHit2D hitRightCenter = Physics2D.Raycast(RaycastCenterRight.position, Vector2.right, rayCastSideDistance, GroundMask);
        RaycastHit2D hitRightTop = Physics2D.Raycast(RaycastTopRight.position, Vector2.right, rayCastSideDistance, GroundMask);

        return hitRightBottom.collider != null || hitRightCenter.collider != null || hitRightTop.collider != null;
    }

    private bool HasWallOnLeft() {
        RaycastHit2D hitLeftBottom = Physics2D.Raycast(RaycastBottomLeft.position, Vector2.left, rayCastSideDistance, GroundMask);
        RaycastHit2D hitLeftCenter = Physics2D.Raycast(RaycastCenterLeft.position, Vector2.left, rayCastSideDistance, GroundMask);
        RaycastHit2D hitLeftTop = Physics2D.Raycast(RaycastTopLeft.position, Vector2.left, rayCastSideDistance, GroundMask);

        return hitLeftBottom.collider != null || hitLeftCenter.collider != null || hitLeftTop.collider != null;
    }

    private bool IsGrounded() {
        RaycastHit2D hitBottom = Physics2D.Raycast(RaycastBottom.position, Vector2.down, rayCastJumpDistance, GroundMask);
        RaycastHit2D hitBottomRight = Physics2D.Raycast(RaycastBottomRight.position, Vector2.down, rayCastJumpDistance, GroundMask);
        RaycastHit2D hitBottomLeft = Physics2D.Raycast(RaycastBottomLeft.position, Vector2.down, rayCastJumpDistance, GroundMask);
        return hitBottom.collider != null || hitBottomRight.collider != null || hitBottomLeft.collider != null;
    }
}
