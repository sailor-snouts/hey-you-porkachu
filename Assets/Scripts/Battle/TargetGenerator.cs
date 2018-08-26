using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetGenerator : MonoBehaviour {

    public GameObject prefab;

    void Start () {
        InvokeRepeating("CreateTarget", 0f, 2);
    }

    void Update() {
    }

    private void CreateTarget() {
        Vector3 position = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), 0);
        Instantiate(prefab, position, Quaternion.identity);
    }
}
