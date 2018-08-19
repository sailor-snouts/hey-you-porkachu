using UnityEngine;
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

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
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

        var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        transform.position += move * speed * Time.deltaTime;
    }
}
