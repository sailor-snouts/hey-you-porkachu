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

    // Player Action Affordance & controls
    private PlayerActionUI actionUI;
    private bool interactive = false;
    private GameObject interactiveObject = null;

    private Rigidbody2D rgb2d;

    void Start()
    {
        animator = GetComponent<Animator>();
        actionUI = GetComponentInChildren<PlayerActionUI>();
        rgb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        ProcessInteraction();
        UpdateAnimationState();
        move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
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
        Debug.Log("Old x: " + transform.position.x);
        var oldPosition = Clamp(transform.position, 64);
        Debug.Log("Old X Clamped: " + oldPosition.x);
        float posX = transform.position.x + (this.move.x * speed * Time.fixedDeltaTime);
        float posY = transform.position.y + (this.move.y * speed * Time.fixedDeltaTime);

        var moveVector = new Vector2(posX, posY);
        rgb2d.MovePosition(Clamp(moveVector, 64));
    }

    Vector2 Clamp(Vector2 moveVector, int pixelsPerUnit)
    {
        Vector2 vectorInPixels = new Vector2(
            Mathf.RoundToInt(moveVector.x * pixelsPerUnit),
            Mathf.RoundToInt(moveVector.y * pixelsPerUnit));
        Debug.Log("Vector X in pixels: " + vectorInPixels.x);
        // Smoots to per-pixel movement

        var meh = vectorInPixels / pixelsPerUnit;
        Debug.Log(" 'Clamped' X: " + meh.x);
        return vectorInPixels / pixelsPerUnit;
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
