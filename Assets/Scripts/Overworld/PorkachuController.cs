using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PorkachuController : MonoBehaviour {
    public void Pickup(Inventory inventory)
    {
        if (inventory.HasItem(ItemType.BUN))
        {
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
