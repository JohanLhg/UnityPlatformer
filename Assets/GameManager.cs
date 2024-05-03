using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;
    public Rigidbody2D player;
    public PlayerHealthBehavior playerHealthBehavior;
    public PlayerMovementBehavior playerMovementBehavior;
    public TMP_Text FruitText;
    public int MaxHealth;

    private int Score = 0;
    private int health;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    
    void Start() {
        FruitText.text = $"Score : {Score}";
        health = MaxHealth;
    }

    public void AddScore(int score) {
        Score += score;
        FruitText.text = $"Score : {Score}";
    }

    public void TakeDamage() {
        
    }
}
