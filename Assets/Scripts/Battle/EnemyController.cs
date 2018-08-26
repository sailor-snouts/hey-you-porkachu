using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    [SerializeField, Range(0.07f, 0.2f)]
    private float speed = 0.1f;
    [SerializeField, Range(1, 7)]
    private float boundary = 5;
    private float y;
    private bool moving = false;
    float movementAmount;
    float destination;
    float destinationMargin = 0.3f;
    private SpriteRenderer spriteR;
    public Sprite guardedSprite;
    public Sprite unguardedSprite;
    private BoxCollider2D col;
    

    // Use this for initialization
    void Start () {
        this.y = this.transform.position.y;
        spriteR = GetComponent<SpriteRenderer>();
        GetComponent<SpriteRenderer>().sprite = unguardedSprite;
        Invoke("ToggleGuard", 2f);
        col = GetComponent<BoxCollider2D>();
    }
	
	// Update is called once per frame
	void Update () {
        if(moving) {
            Move();
            return;
        }
        CalculateMove();
        Move();
	}

    private void ToggleGuard() {
        Sprite currentSprite = GetComponent<SpriteRenderer>().sprite;
        if(currentSprite == guardedSprite) {
            GetComponent<SpriteRenderer>().sprite = unguardedSprite;
            col.enabled = true;
        } else {
            GetComponent<SpriteRenderer>().sprite = guardedSprite;
            col.enabled = false;
        }

        Invoke("ToggleGuard", Random.Range(2, 3));
    }

    private void CalculateMove() {
        int direction = Random.Range(-1, 2);
        destination = Mathf.Clamp(transform.position.x + Random.Range(0,6) * direction, -boundary, boundary);
    }

    private void Move() {
        if(Mathf.Abs(transform.position.x - destination) < destinationMargin) {
            //Arrived at the current destination, so stop moving.
            moving = false;
            return;
        }
        moving = true;
        transform.position = new Vector2(Mathf.SmoothStep(transform.position.x, destination, speed), this.y);         
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        // @TODO Make this a calculation of HP etc and move this code to after
        Destroy(gameObject);
        Destroy(collision.gameObject);

        // @TODO Move this to HP calculation
        // @TODO Head back to the Restaurant.  You won!
        GameManager gameManager = FindObjectOfType<GameManager>();
        if(gameManager) {
            gameManager.EndBattle(true);
        }
    }
}
