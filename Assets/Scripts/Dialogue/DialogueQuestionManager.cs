using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueQuestionManager : MonoBehaviour
{
    public SpriteRenderer selectIcon; 
    public SpriteRenderer moreIcon; 
    public TextMeshProUGUI question;
    public TextMeshProUGUI answer1;
    public TextMeshProUGUI answer2;
    public TextMeshProUGUI answer3;
    public TextMeshProUGUI answer4;
    private Renderer rend;
    private bool canChangeSelection = true;
    private int currentSelection = 1;
    [SerializeField]
    private AudioClip select;
    private AudioSource audio;

    private Queue<string> sentences;

    void Awake()
    {
        sentences = new Queue<string>();
        rend = GetComponent<Renderer>();
        this.setRenderers(false);
        this.audio = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!this.isHavingDialogue())
        {
            return;
        }
        
        float vertical = Input.GetAxis("Vertical");
        if (this.canChangeSelection && Mathf.Abs(vertical) > 0f)
        {
            this.ChangeSelection(vertical > 0f ? -1 : 1);
        } else if(!this.canChangeSelection && Mathf.Abs(vertical) == 0f) {
            this.canChangeSelection = true;
        }
    }

    public bool isHavingDialogue()
    {
        return rend.enabled;
    }

    public bool hasMoreDialogue()
    {
        return sentences.Count > 0;
    }

    private void setRenderers(bool enabled)
    {
        this.rend.enabled = enabled;
        this.selectIcon.GetComponent<Renderer>().enabled = enabled;
    }

    private void ChangeSelection(int direction)
    {
        this.currentSelection = Mathf.Clamp(this.currentSelection + direction, 1, 4);
        Vector3 currentPosition = this.selectIcon.transform.position;
        float yPosition = 0f;
        // im not sure why the text box positions are off so im adding an offset as a dirty fix
        // ofset is technically 1 but im adjusting it to be centered with the text
        float yPositionOffset = 0.9f;
        switch(this.currentSelection)
        {
            case 1:
                yPosition = answer1.rectTransform.position.y + yPositionOffset;
                break;
            case 2:
                yPosition = answer2.rectTransform.position.y + yPositionOffset;
                break;
            case 3:
                yPosition = answer3.rectTransform.position.y + yPositionOffset;
                break;
            case 4:
                yPosition = answer4.rectTransform.position.y + yPositionOffset;
                break;
            default:
                yPosition = currentPosition.y;
                break;
        }
        this.selectIcon.transform.position = new Vector3(currentPosition.x, yPosition, currentPosition.z);
        this.canChangeSelection = false;

    }

    public void StartDialogue(DialogueQuestion dialogue)
    {
        this.setRenderers(true);
        this.sentences.Clear();
        this.question.text = dialogue.question;
        this.answer1.text = dialogue.answers[0];
        this.answer2.text = dialogue.answers[1];
        this.answer3.text = dialogue.answers[2];
        this.answer4.text = dialogue.answers[3];
    }

    public int EndDialogue()
    {
        if (this.select != null)
        {
            this.audio.PlayOneShot(this.select);
        }

        this.question.text = "";
        this.answer1.text = "";
        this.answer2.text = "";
        this.answer3.text = "";
        this.answer4.text = "";
        this.setRenderers(false);
        return this.currentSelection;
    }
}