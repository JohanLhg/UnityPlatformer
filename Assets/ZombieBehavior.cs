using System;
using UnityEngine;

public class ZombieBehavior : MonoBehaviour {
    public Transform RaycastBottom;
    public Transform RaycastBottomRight;
    public Transform RaycastCenterRight;
    public Transform RaycastTopRight;
    public Transform RaycastBottomLeft;
    public Transform RaycastCenterLeft;
    public Transform RaycastTopLeft;
    public LayerMask GroundMask;
    public GameObject DisappearPrefab;

    public int MaxHealth;
    public int Damage;
    public float RunSpeed;
    public float WalkSpeed;
    public float JumpForce;
    public float Knockback;
    public float KnockbackUp;

    [HideInInspector]
    public GameObject target;

    private Rigidbody2D body;
    private SpriteRenderer sprite;
    private Animator animator;
    private Vector3 spawn;
    private float speed;
    private int health;

    private static float rayCastSideDistance = 0.35f;
    private static float rayCastJumpDistance = 0.01f;

    private Vector3 currentDestination;

    void Start() {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        spawn = transform.position;
        health = MaxHealth;
    }

    void FixedUpdate() {
        speed = 0;

        if (target != null) {
            currentDestination = target.transform.position;
            speed = RunSpeed;
        }
        else {
            currentDestination = spawn;
            if (Vector2.Distance(transform.position, spawn) > 0.1f)
                speed = WalkSpeed;
        }

        Vector3 dir = transform.position - currentDestination;
        sprite.flipX = dir.x > 0;

        if (speed > 0) {
            body.velocityX = dir.x > 0 ? -speed : speed;

            if (HasWallOnSide() && IsGrounded()) {
                body.AddForceY(JumpForce);
            }
        }

        animator.SetFloat("velocityX", speed);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag("Player")) {
            Rigidbody2D playerBody = collision.collider.attachedRigidbody;

            if (playerBody.velocityY < 0) {
                playerBody.velocityY = 0;
                playerBody.AddForceY(KnockbackUp);
                OnDamageTaken();
            }
            else {
                playerBody.AddForce((collision.GetContact(0).normal * -Knockback) + Vector2.up * KnockbackUp);
                //GameManager.Instance.TakeDamage(Damage);
            }
        }
    }

    private void OnDamageTaken() {
        health --;
        if (health <= 0) {
        Destroy(gameObject);
        Instantiate(DisappearPrefab, transform.position, Quaternion.identity);
        } else animator.SetTrigger("Hit");
    }

    private bool HasWallOnRight() {
        RaycastHit2D hitRightBottom = Physics2D.Raycast(RaycastBottomRight.position, Vector2.right, rayCastSideDistance, GroundMask);
        RaycastHit2D hitRightCenter = Physics2D.Raycast(RaycastCenterRight.position, Vector2.right, rayCastSideDistance, GroundMask);
        RaycastHit2D hitRightTop = Physics2D.Raycast(RaycastTopRight.position, Vector2.right, rayCastSideDistance, GroundMask);

        return hitRightBottom.collider != null || hitRightCenter.collider != null || hitRightTop.collider != null;
    }

    private bool HasWallOnSide() {
        return HasWallOnLeft() || HasWallOnRight();
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
