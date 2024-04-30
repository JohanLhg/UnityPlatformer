using UnityEngine;

public class ZombieBehavior : MonoBehaviour {

    public float Speed;
    public Rigidbody2D Body;
    public Transform PlayerBody;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if (Body.position.x < PlayerBody.position.x) {
            transform.rotation = Quaternion.LookRotation(Vector3.forward);
            Body.velocityX = Speed;
        }
        if (Body.position.x > PlayerBody.position.x) {
            transform.rotation = Quaternion.LookRotation(Vector3.back);
            Body.velocityX = -Speed;
        }
    }
}
