using System;
using UnityEngine;

public class PlayerMovementBehavior : MonoBehaviour {

    public Rigidbody2D Body;
    public SpriteRenderer sprite;
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
    public Animator Animator;

    private static float rayCastSideDistance = 0.25f;
    private static float rayCastJumpDistance = 0.01f;


    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        bool isWallGrinding = false;

        if (Input.GetKey(KeyCode.LeftShift) && !IsGrounded() && (HasWallOnLeft() || HasWallOnRight())) {
            Body.velocityY = -0.1f;
            isWallGrinding = true;
            sprite.flipX = HasWallOnLeft();
        }

        if (IsGoingRight()) {
            sprite.flipX = false;
            if (!HasWallOnRight()) {
                if (Input.GetKey(KeyCode.LeftShift))
                    Body.velocityX = Speed * 2;
                else Body.velocityX = Speed;
            }
        }
        else if (IsGoingLeft()) {
            sprite.flipX = true;
            if (!HasWallOnLeft()) {
                if (Input.GetKey(KeyCode.LeftShift))
                    Body.velocityX = -Speed * 2;
                else Body.velocityX = -Speed;
            }
        }

        if (IsJumping() && (IsGrounded() || isWallGrinding)) {
            if (IsGrounded()) Body.AddForce(Vector2.up * JumpForce);
            else if (isWallGrinding) {
                Body.AddForce(Vector2.up * JumpForce);
                if (HasWallOnLeft()) Body.AddForce(Vector2.right * (JumpForce / 2));
                else Body.AddForce(Vector2.left * (JumpForce / 2));
            }
        }

        Animator.SetFloat("velocityX", Math.Abs(Body.velocityX));
        Animator.SetFloat("velocityY", Body.velocityY);
        Animator.SetBool("isWallGrinding", isWallGrinding);
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
