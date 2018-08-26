using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour {

    [SerializeField]
    float animationDuration = 2.0f;

    [SerializeField]
    float animationSpeed = 2.0f;

    [SerializeField]
    GameObject pickupSprite = null;

    [SerializeField]
    int pickupType;


    private bool animating = false;
    private float animationTime = 0.0f;

    // Use this for initialization
    void Start () {
        pickupSprite.SetActive(false);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if(animating) {
            animationTime += Time.fixedDeltaTime;

            pickupSprite.transform.position += Vector3.up * animationSpeed * Time.deltaTime;

            if (animationTime >= animationDuration) {
                Destroy(this.gameObject);
            }
        }
	}

    public void Pickup(Inventory inventory) {
        pickupSprite.SetActive(true);
        animating = true;

        inventory.AddItem(pickupType);
    }
}
