using UnityEngine;

public class PlayerHealthBehavior : MonoBehaviour {

    private Rigidbody2D body;
    private Animator animator;

    private int maxHealth = 2;
    private int health;

    void Start() {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        health = maxHealth;
    }

    public void Hit(int damage) {
        health -= damage;
        if (health <= 0) {
            animator.SetBool("isDead", true);
        } else {
            animator.SetTrigger("Hit");
            body.AddForceY(200);
        }
    }

    private void Respawn() {
        animator.SetBool("isDead", false);
        animator.SetTrigger("Respawn");
        body.position = new Vector2(0, 0);
        health = maxHealth;
    }
}
