using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunController : MonoBehaviour {

    [SerializeField, Range(0.01f, 0.1f)]
    private float shrinkRate = 0.065f;
	
	void Start () {
		
	}	
	
	void Update () {
        shrink();
	}

    private void shrink() {
        float xScale = Mathf.SmoothStep(transform.localScale.x, 0.35f, shrinkRate);
        float yScale = Mathf.SmoothStep(transform.localScale.y, 0.35f, shrinkRate);
        float zScale = Mathf.SmoothStep(transform.localScale.z, 0.35f, shrinkRate);

        transform.localScale = new Vector3(xScale, yScale, zScale);
    }
}
