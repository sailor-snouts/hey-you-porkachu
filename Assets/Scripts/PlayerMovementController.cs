using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public struct PlayerAnimation
{
    public const int ANIMATION_IDLE = 0;
    public const int ANIMATION_WALK_UP = 1;
    public const int ANIMATION_WALK_RIGHT = 2;
    public const int ANIMATION_WALK_DOWN = 3;
    public const int ANIMATION_WALK_LEFT = 4;
}

public class PlayerMovementController : MonoBehaviour
{
    public string minigameScene;
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private Animator animator;
    private Rigidbody2D r2bd;
    private Vector2 move;
    private int facing;
    private bool isLocked;
    private float lockedCooldown = 0.1f;

    public int GetDirection()
    {
        return this.facing;
    }

    public void SetLocked (bool locked)
    {
        Debug.Log("setting locked to: " + locked);
        Debug.Log("cooldown is: " + this.lockedCooldown);

        if(locked)
        {
            if(this.lockedCooldown <= 0)
            {
                this.isLocked = true;
                this.lockedCooldown = 0.5f;
            }
        } else {
            this.isLocked = false;
        }
        
        Debug.Log("locked set to: " + locked);
    }

    public bool canLock()
    {
        return this.lockedCooldown <= 0;
    }

    public bool IsLocked()
    {
        return this.isLocked;
    }

    void Start()
    {
        animator = this.GetComponent<Animator>();
        r2bd = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!this.isLocked)
        {
            this.lockedCooldown -= Time.deltaTime;
            this.HandleMovement();
        }
    }

    void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float veritcal = Input.GetAxis("Vertical");

        if (Mathf.Abs(horizontal) > Mathf.Abs(veritcal))
        {
            // left or right
            int dir = horizontal > 0 ? 1 : -1;
            this.facing = horizontal > 0 ? PlayerAnimation.ANIMATION_WALK_RIGHT : PlayerAnimation.ANIMATION_WALK_LEFT;
            this.animator.SetInteger("WalkingDirection", this.facing);
            this.move = new Vector2(1 * dir, 0);
            if ((dir == 1 && this.transform.localScale.x > 0) || (dir == -1 && this.transform.localScale.x < 0))
            {
                Vector3 theScale = this.transform.localScale;
                theScale.x *= -1;
                this.transform.localScale = theScale;
            }
        }
        else if (Mathf.Abs(horizontal) < Mathf.Abs(veritcal))
        {
            // top or bottom
            int dir = veritcal > 0 ? 1 : -1;
            this.facing = veritcal > 0 ? PlayerAnimation.ANIMATION_WALK_UP : PlayerAnimation.ANIMATION_WALK_DOWN;
            this.move = new Vector2(0, 1 * dir);
            if(this.animator.GetInteger("WalkingDirection") != this.facing)
            {
                Debug.Log("changing animation");
                this.animator.SetInteger("WalkingDirection", this.facing);
            }
        }
        else
        {
            this.animator.SetInteger("WalkingDirection", PlayerAnimation.ANIMATION_IDLE);
            this.move = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        this.r2bd.velocity = this.move * this.speed;
    }
}
