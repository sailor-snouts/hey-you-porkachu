﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionUI : MonoBehaviour {
    [SerializeField]
    private float iconCheckRadius = 1f;
    [SerializeField]
    private float actionCheckMagnitude = 0.8f;
    private PlayerMovementController movement;
    private Animator animator;
    private SpriteRenderer sprite;

    void Start()
    {
        this.movement = this.gameObject.GetComponentInParent<PlayerMovementController>();
        this.animator = this.gameObject.GetComponentInParent<Animator>();
        this.sprite = this.gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        this.DisplayTipIcon();
        if(Input.GetButtonDown("Fire1"))
        {
            this.TriggerAction();
        }
    }

    void TriggerAction()
    {
        Vector2 checkDirection;
        switch(this.movement.GetDirection())
        {
            case PlayerAnimation.ANIMATION_WALK_UP:
                checkDirection = new Vector2(0, 1);
                break;
            case PlayerAnimation.ANIMATION_WALK_RIGHT:
                checkDirection = new Vector2(1, 0);
                break;
            case PlayerAnimation.ANIMATION_WALK_DOWN:
                checkDirection = new Vector2(0, -1);
                break;
            case PlayerAnimation.ANIMATION_WALK_LEFT:
                checkDirection = new Vector2(-1, 0);
                break;
            default:
                return;
        }

        Transform parent = this.transform.parent;
        Debug.DrawLine(parent.position, parent.position + (Vector3) checkDirection.normalized * this.actionCheckMagnitude, Color.red);
        RaycastHit2D[] hitColliders = Physics2D.RaycastAll(parent.position, checkDirection.normalized, this.actionCheckMagnitude);
        foreach (RaycastHit2D col in hitColliders)
        {
            GameObject obj = col.transform.gameObject;
            switch (obj.tag)
            {
                case "BunPickup":
                    break;
                case "Porkachu":
                    break;
                case "NPC":
                    Debug.Log("starting dialogue");
                    DialogueTrigger dialogue = obj.GetComponent<DialogueTrigger>();
                    dialogue.TriggerDialogue();
                    break;
                default:
                    break;
            }
        }
    }

    void DisplayTipIcon()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(this.transform.position, this.iconCheckRadius);
        bool isShowingIcon = false;
        foreach (Collider2D col in hitColliders)
        {
            if(col.gameObject.GetComponent<Actionable>())
            {
                isShowingIcon = true;
                break;
            }
        }
        this.sprite.enabled = isShowingIcon;
    }
}
