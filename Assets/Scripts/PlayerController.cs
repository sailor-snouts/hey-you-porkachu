using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public struct PlayerAnimation
{
    public const int ANIMATION_IDLE = -1;
    public const int ANIMATION_WALK_LEFT = 0;
    public const int ANIMATION_WALK_RIGHT = 1;
    public const int ANIMATION_WALK_UP = 2;
    public const int ANIMATION_WALK_DOWN = 3;
}

public class PlayerController : MonoBehaviour
{

    Animator animator;
    public float speed;
    public string minigameScene;

    // TODO: Trivial bun pickup count for triggering Porkachu fight
    private int buns = 0;
    private Vector2 move;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        UpdateAnimationState();
        move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
    }

    void UpdateAnimationState()
    {
        if (Input.GetKey(KeyCode.A))
        {
            animator.SetInteger("WalkingDirection", PlayerAnimation.ANIMATION_WALK_LEFT);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            animator.SetInteger("WalkingDirection", PlayerAnimation.ANIMATION_WALK_RIGHT);
        }
        else
        {
            animator.SetInteger("WalkingDirection", PlayerAnimation.ANIMATION_IDLE);
        }

    }

    void FixedUpdate()
    {
        float posX = transform.position.x + (this.move.x * speed * Time.fixedDeltaTime);
        float posY = transform.position.y + (this.move.y * speed * Time.fixedDeltaTime);
        transform.position = new Vector2(posX, posY);   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "BunPickup")
        {
            PickupBun(collision.gameObject);
            return;
        } 
    }

    private void PickupBun(GameObject bun)
    {
        buns++;
        Debug.Log("Oh shit a bun! Buns = " + buns);
        Destroy(bun);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Porkachu")
        {
            MeetPorkachu();
            return;
        } 
        if(collision.gameObject.tag == "NPC")
        {
            MeetNPC();
            return;
        } 

    }

    private void MeetPorkachu()
    {
        if(buns == 3) 
        {
            Debug.Log("PORKACHU LOVES YOU!");
            SceneManager.LoadSceneAsync(minigameScene);
        } else 
        {
            Debug.Log("PORKACHU HUNGRY!");
        }
    }

    private void MeetNPC() 
    {
        Debug.Log("OH HAI!");
    }
}
