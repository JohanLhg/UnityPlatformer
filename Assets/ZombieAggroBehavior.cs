using UnityEngine;

public class ZombieAggroBehavior : MonoBehaviour {
    public ZombieBehavior Enemy;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player"))
            Enemy.target = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player"))
            Enemy.target = null;
    }
}
