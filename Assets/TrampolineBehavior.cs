using UnityEngine;

public class TrampolineBehavior : MonoBehaviour {

    public float jumpForce;
    private Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
    }
    
    private void OnTriggerEnter2D(Collider2D collider) {
        if (!collider.CompareTag("Player")) return;

        animator.SetTrigger("Jump");
        float force = jumpForce;
        if (collider.attachedRigidbody.velocityY < 0)
            force += collider.attachedRigidbody.velocityY / 2;

        collider.attachedRigidbody.velocityY = 0;
        collider.attachedRigidbody.AddForceY(force);
    }
}
