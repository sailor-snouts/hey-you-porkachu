using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePlayerController : MonoBehaviour {
    Rigidbody2D rigidBody;
    private float y;
    public float speed = 0.1f;

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
            if(controllerInput < 0) {
                movementAmount = transform.position.x - speed;
            } else {
                movementAmount = transform.position.x + speed;
            }

            transform.position = new Vector2(movementAmount, this.y);
        }
    }
}
