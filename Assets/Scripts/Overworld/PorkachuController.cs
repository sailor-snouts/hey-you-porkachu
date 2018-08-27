using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PorkachuController : ChefController {
    public void Pickup(Inventory inventory)
    {
        if (inventory.HasItem(ItemType.BUN))
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            gameManager.currentChef = chefType;

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
