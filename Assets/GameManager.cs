using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;
    public TMP_Text FruitText;

    private int fruitCounter = 0;

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        FruitText.text = $"Fruits : {fruitCounter}";
    }

    // Update is called once per frame
    void Update() {
    }

    public void IncrementFruitCounter() {
        fruitCounter ++;
        FruitText.text = $"Fruits : {fruitCounter}";
    }
}
