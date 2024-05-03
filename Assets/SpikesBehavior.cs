using UnityEngine;

public class SpikesBehavior : MonoBehaviour {

    private PlayerHealthBehavior playerHealthBehavior;

    void Start() {
        playerHealthBehavior = GameManager.Instance.playerHealthBehavior;
    }
    
    private void OnTriggerEnter2D(Collider2D collider) {
        if (!collider.CompareTag("Player")) return;

        playerHealthBehavior.Hit(1);
    }
}
