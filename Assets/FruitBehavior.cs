using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class FruitBehavior : MonoBehaviour {

    public FruitData Data;
    public GameObject CollectedPrefab;

    private void OnTriggerEnter2D(Collider2D collider) {
        if (!collider.CompareTag("Player")) return;
        
        GameManager.Instance.AddScore(Data.Score);
        Destroy(gameObject);
        Instantiate(CollectedPrefab, transform.position, Quaternion.identity);
    }
}
