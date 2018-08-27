using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunPickupController : PickupController {

    public override void Pickup(Inventory inventory)
    {
        GameManager manager = FindObjectOfType<GameManager>();

        if (Application.isEditor || manager.ChefDefeated(ChefType.BUN_CHEF))
        {
            pickupSprite.SetActive(true);
            animating = true;
            inventory.AddItem(pickupType);

            DialogueTrigger[] dialogueTriggers = gameObject.GetComponents<DialogueTrigger>();
            foreach (DialogueTrigger trigger in dialogueTriggers)
            {
                if (trigger.triggerId == 1)
                    trigger.TriggerDialogue(FindObjectOfType<PlayerMovementController>());
            }
        }
        else
        {
            DialogueTrigger[] dialogueTriggers = gameObject.GetComponents<DialogueTrigger>();
            foreach (DialogueTrigger trigger in dialogueTriggers)
            {
                if (trigger.triggerId == 0)
                    trigger.TriggerDialogue(FindObjectOfType<PlayerMovementController>());
            }
        }
    }

}
