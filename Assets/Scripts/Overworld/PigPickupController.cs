using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigPickupController : PickupController
{
    public override void Pickup(Inventory inventory)
    {
        GameManager manager = FindObjectOfType<GameManager>();

        if( manager.ChefDefeated(3)) {
            pickupSprite.SetActive(true);
            animating = true;
            inventory.AddItem(pickupType);

            DialogueTrigger[] dialogueTriggers = gameObject.GetComponents<DialogueTrigger>();
            foreach (DialogueTrigger trigger in dialogueTriggers)
            {
                if (trigger.triggerId == 0)
                    trigger.TriggerDialogue(FindObjectOfType<PlayerMovementController>());
            }
        }
        else {
            DialogueTrigger[] dialogueTriggers = gameObject.GetComponents<DialogueTrigger>();
            foreach (DialogueTrigger trigger in dialogueTriggers ) {
                if(trigger.triggerId == 1 )
                    trigger.TriggerDialogue(FindObjectOfType<PlayerMovementController>());
            }
        }
    }
}
