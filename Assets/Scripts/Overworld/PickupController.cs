using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour {

    [SerializeField]
    float animationDuration = 2.0f;

    [SerializeField]
    float animationSpeed = 2.0f;

    [SerializeField]
    protected GameObject pickupSprite = null;

    [SerializeField]
    protected int pickupType;


    protected bool animating = false;
    protected float animationTime = 0.0f;

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

    public virtual void Pickup(Inventory inventory) {

        Debug.Log("Trying to pick something up: " + pickupType);

        DialogueTrigger[] dialogueTriggers = gameObject.GetComponents<DialogueTrigger>();
        foreach (DialogueTrigger trigger in dialogueTriggers)
        {
            Debug.Log("Dialog trigger ID: " + trigger.triggerId);
            if (trigger.triggerId == 0)
            {
                trigger.TriggerDialogue(FindObjectOfType<PlayerMovementController>());
            }
        }


        pickupSprite.SetActive(true);
        animating = true;
        inventory.AddItem(pickupType);
    }
}
