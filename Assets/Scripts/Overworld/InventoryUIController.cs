using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIController : MonoBehaviour {

    public SpriteRenderer dough;
    public SpriteRenderer onion;
    public SpriteRenderer pork;
    public SpriteRenderer bun;
    private Inventory inventory;

    private void Start()
    {
        if (!inventory) {
            GameManager manager = FindObjectOfType<GameManager>();
            inventory = FindObjectOfType<Inventory>();
        }
    }

    void Update () {
        if(!inventory) {
            Debug.LogError("Inventory UI could NOT find Inventory");
            return;
        }

        dough.enabled = inventory.HasItem(ItemType.DOUGH);		
        onion.enabled = inventory.HasItem(ItemType.ONION);        
        pork.enabled = inventory.HasItem(ItemType.PORK);        
        bun.enabled = inventory.HasItem(ItemType.BUN);        
	}
}
