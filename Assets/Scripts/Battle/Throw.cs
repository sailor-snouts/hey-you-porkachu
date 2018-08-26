using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour {

    public GameObject prefab;
    [SerializeField, Range(-5, 5)]
    private int startingHeight = -3;
    [SerializeField, Range(0, 0.5f)]
    private float throwDelay = 0.25f;
    [SerializeField, Range(0, 5f)]
    private float targetYPosition = 3.5f;

    void Start() {

    }

    void Update() {
        if (Input.GetButtonDown("Fire1")) {
            // Call ThrowBun on a slight delay to give a sense of a wind up before the throw happens.
            Invoke("ThrowBun", throwDelay);
        }
    }

    private void ThrowBun() {
        Vector2 targetPosition = new Vector2(transform.position.x, targetYPosition);
        Vector2 pos = new Vector2(targetPosition.x, startingHeight);
        GameObject bun = Instantiate(prefab, pos, Quaternion.identity);
        bun.GetComponent<BunController>().setTargetPosition(targetPosition);
    }
}
