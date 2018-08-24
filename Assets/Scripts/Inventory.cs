using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public bool onion = false;
    public bool dough = false;
    public bool pork = false;
    public bool bun = false;

    private List<Key> keys;

    void Start() {
        keys = new List<Key>();
    }

    public void AddKey(int key) {
        Key newKey = new Key();
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
