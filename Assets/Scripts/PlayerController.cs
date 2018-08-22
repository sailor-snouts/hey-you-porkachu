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
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    public string minigameScene;

    private Animator animator;
    private Rigidbody2D r2bd;

    // TODO: Trivial bun pickup count for triggering Porkachu fight
    private int buns = 0;
    private Vector2 move;

    // Player Action Affordance & controls
    private PlayerActionUI actionUI;
    private bool interactive = false;
    private GameObject interactiveObject = null;

    void Start()
    {
        animator = GetComponent<Animator>();
        actionUI = GetComponentInChildren<PlayerActionUI>();
        r2bd = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        ProcessInteraction();
        HandleMovement();
    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float veritcal = Input.GetAxis("Vertical");

        if (Mathf.Abs(horizontal) > Mathf.Abs(veritcal))
        {
            // left or right
            int dir = horizontal > 0 ? 1 : -1;
            animator.SetInteger("WalkingDirection", horizontal > 0 ? PlayerAnimation.ANIMATION_WALK_RIGHT : PlayerAnimation.ANIMATION_WALK_LEFT);
            this.move = new Vector2(1 * dir, 0);
        }
        else if (Mathf.Abs(horizontal) < Mathf.Abs(veritcal))
        {
            // top or bottom
            int dir = veritcal > 0 ? 1 : -1;
            animator.SetInteger("WalkingDirection", veritcal > 0 ? PlayerAnimation.ANIMATION_WALK_UP : PlayerAnimation.ANIMATION_WALK_DOWN);
            this.move = new Vector2(0, 1 * dir);
        }
        else
        {
            animator.SetInteger("WalkingDirection", PlayerAnimation.ANIMATION_IDLE);
            this.move = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        r2bd.velocity = this.move * this.speed;
    }

    void ProcessInteraction() 
    {
        if( interactive && interactiveObject ) {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Return))
            {
                PerformAction();
            }
        }
    }

    void EnableInteraction(GameObject actionable)
    {
        actionUI.Show();
        interactive = true;
        interactiveObject = actionable;
    }

    void DisableInteraction()
    {
        actionUI.Hide();
        interactive = false;
        interactiveObject = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.gameObject.GetComponent<Actionable>()) 
        {
            if (actionUI)
            {
                EnableInteraction(collision.gameObject);
            } else {
                Debug.LogError("Oh shit no action ui!");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if( actionUI )
        {
            DisableInteraction();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Actionable>())
        {
            if (actionUI)
            {
                EnableInteraction(collision.gameObject);
            }
            else
            {
                Debug.LogError("Oh shit no action ui!");
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (actionUI)
        {
            DisableInteraction();
        }
    }

    private void PerformAction() 
    {
        if(!interactiveObject) {
            Debug.LogWarning("Tried to interact on a null object!");
            return;
        }

        if(interactiveObject.tag == "BunPickup")
        {
            PickupBun();
            return;
        } 
        if(interactiveObject.tag == "Porkachu")
        {
            MeetPorkachu();
            return;
        } 
        if(interactiveObject.tag == "NPC")
        {
            MeetNPC();
            return;
        } 
       
    }

    private void PickupBun()
    {
        buns++;
        Debug.Log("Oh shit a bun! Buns = " + buns);
        Destroy(interactiveObject);
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
