using UnityEngine;

public class SpikesBehavior : MonoBehaviour {

    public PlayerHealthBehavior playerHealthBehavior;
    
    private void OnTriggerEnter2D(Collider2D collider) {
        if (!collider.CompareTag("Player")) return;

        playerHealthBehavior.Hit(1);
    }
}
