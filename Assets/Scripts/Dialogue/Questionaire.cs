using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Questionaire : MonoBehaviour {

    private DialogueQuestionTrigger[] questions;
    private int currentQuestion = 0;
    private int answer1count = 0;
    private int answer2count = 0;
    private int answer3count = 0;
    private int answer4count = 0;
    private PlayerMovementController movement;
    private Inventory inventory;
    private bool inQuestionaire = false;

	void Start ()
    {
        this.questions = this.gameObject.GetComponents<DialogueQuestionTrigger>();
        this.movement = FindObjectOfType<PlayerMovementController>();
        this.inventory = FindObjectOfType<Inventory>();
	}
	
	void Update ()
    {
        if (this.inQuestionaire && Input.GetButtonDown("Fire1"))
        {
            int answer = this.questions[this.currentQuestion].EndDialogue();
            switch(answer)
            {
                case 1:
                    this.answer1count++;
                    break;
                case 2:
                    this.answer2count++;
                    break;
                case 3:
                    this.answer3count++;
                    break;
                case 4:
                    this.answer4count++;
                    break;
            }
            this.currentQuestion++;
            if(this.currentQuestion < this.questions.Length)
            {
                this.questions[this.currentQuestion].TriggerDialogue();
            }
            else
            {
                this.movement.SetLocked(false);
                this.inventory.porkachuType = this.GetPorkachuType();
                Destroy(this.gameObject);
            }
        }
    }

    public void Ask(PlayerMovementController movement, Inventory inventory)
    {
        this.inQuestionaire = true;
        this.movement = movement;
        this.movement.SetLocked(true);
        this.questions[0].TriggerDialogue();
    }

    private int GetPorkachuType()
    {
        // i know this is terrible but short on time so ¯\_(ツ)_/¯ fight me
        int max = Mathf.Max(answer1count, answer2count);
        max = Mathf.Max(max, answer3count);
        max = Mathf.Max(max, answer4count);

        if(max == this.answer1count)
        {
            return 1;
        }
        if (max == this.answer2count)
        {
            return 2;
        }
        if (max == this.answer3count)
        {
            return 3;
        }
        if (max == this.answer4count)
        {
            return 4;
        }

        return 1;
    }
}
