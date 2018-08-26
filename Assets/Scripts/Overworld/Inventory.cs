using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public int porkachuType = 0;

    public List<Item> items;
    public List<Key> keys;

    void Start() {
        keys = new List<Key>();
        items = new List<Item>();
    }

    public void AddItem(int item)
    {
        Debug.Log("Adding item " + item);
        Item newItem = ScriptableObject.CreateInstance<Item>();
        newItem.type = item;
        items.Add(newItem);
    }

    public bool HasItem(int itemType)
    {
        foreach (Item item in items)
        {
            if (item.type == itemType)
                return true;
        }

        return false;
    }

    public void AddKey(int key) {
        Key newKey = ScriptableObject.CreateInstance<Key>();
        newKey.type = key;
        keys.Add(newKey);    
    }

    public bool HasKey(int keyType) {
        foreach(Key key in keys) {
            if (key.type == keyType)
                return true;
        }

        return false;
    }
}
