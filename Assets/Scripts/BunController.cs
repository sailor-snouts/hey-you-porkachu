using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunController : MonoBehaviour {

    // How quickly the bun is shrunk, or how 'fast' it appears to move away from the player.
    // The smaller this number, the slower the bun will shrink.
    [SerializeField, Range(0.08f, 0.16f)]
    private float shrinkRate = 0.1f;

    // The smaller this number, the faster the bun will move through its arc.
    [SerializeField, Range(0.08f, 0.2f)]
    private float smoothTime = 0.17f;
    
    //Needed by SmoothDamp
    private float yVelocity = 0.0f;
    //How close a bun must be to its apex to consider it to have peaked
    private float apexMargin = 0.06f;
    private bool isFalling = false;
    private Vector2 targetVector;
    private Vector2 terminalVector;
	
	void Start () {
		
	}	
	
	void Update () {
        Shrink();
        Move();
    }

    // Convey a sense of the bun traveling away from the player by scaling the size of the bun down over time.
    private void Shrink() {
        float scale = Mathf.SmoothStep(transform.localScale.x, 0.35f, shrinkRate);
        transform.localScale = new Vector3(scale, scale, scale);
    }

    // Calculate how much to move the bun this frame and then move it.
    private void Move() {
        DetermineDirection();
        float yMovement = Mathf.SmoothDamp(transform.position.y, targetVector.y, ref yVelocity, smoothTime);
        transform.position = new Vector3(transform.position.x, yMovement, 0);
    }

    // Figure out if bun should be headed up or down and respond accordingly.
    private void DetermineDirection() {
        // If bun isn't already falling, check to see if it's within a certain margin of its target apex
        if (!isFalling && Mathf.Abs(transform.position.y - targetVector.y) < apexMargin) {
            isFalling = true;
            // Bun has reached its peak and is ready to fall, so reset its target to the bottom of the screen
            targetVector = terminalVector;
            // Lengthen the smooth time a bit on the descent, otherwise the springy nature of SmoothDamp causes the bun to fall too fast.
            smoothTime = smoothTime * 2;
        }
    }

    public void setTargetPosition(Vector2 vector) {
        // Bump the target just a bit over the pointer position to make it feel more like the bun is being thrown
        targetVector = vector + new Vector2(0, 0.1f);
        terminalVector = new Vector2(targetVector.x, -6);
    }
}
