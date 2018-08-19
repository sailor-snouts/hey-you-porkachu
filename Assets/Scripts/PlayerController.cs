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

    // TODO: Trivial bun pickup count for triggering Porkachu fight
    private int buns = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        UpdateAnimationState();
        UpdateMovement();
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

    void UpdateMovement()
    {
        var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        transform.position += move * speed * Time.deltaTime;   
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
    }

    private void MeetPorkachu()
    {
        if(buns == 3) 
        {
            Debug.Log("PORKACHU LOVES YOU!");
            SceneManager.LoadSceneAsync(4);
        } else 
        {
            Debug.Log("PORKACHU HUNGRY!");
        }
    }
}
