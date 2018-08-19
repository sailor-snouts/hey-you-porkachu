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
        float scale = Mathf.SmoothStep(transform.localScale.x, 0.35f, shrinkRate);
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
