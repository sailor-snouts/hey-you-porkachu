using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    [SerializeField, Range(0.07f, 0.2f)]
    protected float speed = 0.1f;
    [SerializeField, Range(1, 7)]
    protected float boundary = 5;
    protected float y;
    protected bool moving = false;
    float movementAmount;
    float destination;
    float destinationMargin = 0.3f;
    protected SpriteRenderer spriteR;
    public Sprite guardedSprite;
    public Sprite unguardedSprite;
    protected BoxCollider2D col;
    public GameObject healthPrefab;
    protected HealthBar healthBar;

    // Use this for initialization
    void Start () {
        this.y = this.transform.position.y;
        spriteR = GetComponent<SpriteRenderer>();
        GetComponent<SpriteRenderer>().sprite = unguardedSprite;
        Invoke("ToggleGuard", 2f);
        col = GetComponent<BoxCollider2D>();
        GameObject healthInstance = Instantiate(healthPrefab, healthPrefab.transform.position, Quaternion.identity);
        healthBar = healthInstance.GetComponent<HealthBar>();        
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
            spriteR.sprite = unguardedSprite;
            col.enabled = true;
        } else {
            spriteR.sprite = guardedSprite;
            col.enabled = false;
        }

        Invoke("ToggleGuard", Random.Range(2, 3));
    }

    protected void CalculateMove() {
        int direction = Random.Range(-1, 2);
        destination = Mathf.Clamp(transform.position.x + Random.Range(0,6) * direction, -boundary, boundary);
    }

    protected void Move() {
        if(Mathf.Abs(transform.position.x - destination) < destinationMargin) {
            //Arrived at the current destination, so stop moving.
            moving = false;
            return;
        }
        moving = true;
        transform.position = new Vector2(Mathf.SmoothStep(transform.position.x, destination, speed), this.y);         
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        BattleItemController item = collision.gameObject.GetComponent<BattleItemController>();
        healthBar.HurtLove(item.GetDamage());        
        if(!healthBar.IsAlive()) {
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager) {
                gameManager.EndBattle(true);
            }
        }
        Destroy(collision.gameObject);
    }
}
