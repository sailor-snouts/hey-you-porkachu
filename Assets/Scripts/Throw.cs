using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour {

    public GameObject prefab;
    [SerializeField, Range(-5, 5)]
    private int startingHeight = -5;
    [SerializeField, Range(200, 800)]
    private int force = 650;

    // Use this for initialization
    void Start () {
		
	}

    void Update() {
        if (Input.GetButtonDown("Fire1")) {
            throwBun();
        }
    }

    private void throwBun() {
        Vector3 pos = new Vector3(1, startingHeight, 1);
        GameObject bun = Instantiate(prefab, pos, Quaternion.identity);
        bun.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, force));
    }
}
