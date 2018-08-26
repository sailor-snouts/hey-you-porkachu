using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIController : MonoBehaviour {

    public SpriteRenderer dough;
    public SpriteRenderer onion;
    public SpriteRenderer pork;
    public SpriteRenderer bun;
    public Inventory inventory;

    private void Start()
    {
        if (!inventory) {
            GameManager manager = FindObjectOfType<GameManager>();
            inventory = manager.GetComponent<Inventory>();
        }
    }

    void Update () {
        if(!inventory) {
            Debug.LogError("Inventory UI could NOT find Inventory");
            return;
        }

        dough.enabled = inventory.dough;		
        onion.enabled = inventory.onion;        
        pork.enabled = inventory.pork;        
        bun.enabled = inventory.bun;        
	}
}
