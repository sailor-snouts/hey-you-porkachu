using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public bool onion = false;
    public bool dough = false;
    public bool pork = false;
    public bool bun = false;
    public int porkachuType = 0;

    private List<Key> keys;

    void Start() {
        keys = new List<Key>();
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
