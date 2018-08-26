using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePlayerController : MonoBehaviour {
    Rigidbody2D rigidBody;
    private float y;

    [SerializeField]
    private float speed = 0.1f;
    [SerializeField]
    private float minClamp;
    [SerializeField]
    private float maxClamp;

    // Use this for initialization
    void Start () {
        this.y = this.transform.position.y;	
	}
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    private void Move() {
        float controllerInput = Input.GetAxis("Horizontal");
        float movementAmount;
        if(controllerInput != 0) {
            int dir = controllerInput > 0 ? 1 : -1;
            movementAmount = Mathf.Clamp(transform.position.x + speed * dir, this.minClamp, this.maxClamp);

            transform.position = new Vector2(movementAmount, this.y);
        }
    }
}
