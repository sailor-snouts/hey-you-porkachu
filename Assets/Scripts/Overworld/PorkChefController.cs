using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PorkChefController: ChefController {

    DialogueTrigger calm, battle;

    public override void Initialize() {
        DialogueTrigger[] triggers = gameObject.GetComponents<DialogueTrigger>();
        foreach (DialogueTrigger trigger in triggers)
        {
            if (trigger.triggerId == 0)
            {
                calm = trigger;
            }
            else if (trigger.triggerId == 1)
            {
                battle = trigger;
            }
        }
    }

    public override void StartEncounter() 
	{

		GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.currentChef = chefType;

        Inventory inventory = FindObjectOfType<Inventory>();

        if(inventory.HasItem(ItemType.DOUGH) && inventory.HasItem(ItemType.ONION))
        {
            battle.TriggerDialogue(FindObjectOfType<PlayerMovementController>());
        } else {
            calm.TriggerDialogue(FindObjectOfType<PlayerMovementController>());
        }
    }
}
