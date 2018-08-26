using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TargetController : MonoBehaviour {

    [SerializeField, Range(0.01f, 0.1f)]
    private float shrinkRate = 0.065f;

    // Use this for initialization
    void Start () {
		
	}

    void Update() {
        if(transform.localScale.x < 0.03f) {
            Destroy(gameObject);
        }
        shrink();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Destroy(gameObject);
        Destroy(collision.gameObject);

        // @TODO WHOA THIS IS GROSS
        // @TODO Head back to the Restaurant.  You won!
        SceneManager.LoadScene(3);
    }

    private void shrink() {
        float scale = Mathf.SmoothStep(transform.localScale.x, 0, shrinkRate);
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
