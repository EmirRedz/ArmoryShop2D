using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
     public static DialogueManager Instance;
    public Animator dialogueAnimator;
    public TextMeshProUGUI dialogueText;
    public TMP_Text npcName;
    private Queue<string> sentences;
    private Queue<AudioClip> clips;
    [SerializeField]
    AudioSource audioSource;
    bool isDialogueWindowUp;
    private DialogueTrigger currentNPC;
    [Header("Next Sentence")]
    public float timer = 5;
    [HideInInspector]public float currentTime;
    private static readonly int IsOpen = Animator.StringToHash("isOpen");

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        clips = new Queue<AudioClip>();
        currentTime = timer;
    }

    private void Update()
    {
        if (isDialogueWindowUp)
        {
            if (currentTime <= 0)
            {
                DisplayNextSentence();
                currentTime = timer;
            }
            else
            {
                currentTime -= Time.deltaTime;
            }
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {

        dialogueAnimator.SetBool(IsOpen, true);
        isDialogueWindowUp = true;
        
        if (npcName != null)
        {
            npcName.SetText(dialogue.NPCName);
        }
        sentences.Clear();
        clips.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        foreach (AudioClip clip in dialogue.clipsToPlay)
        {
            clips.Enqueue(clip);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        //AudioClip clip = clips.Dequeue();
        StopAllCoroutines();
        StartCoroutine(AnimateSentence(sentence));
        // if (!audioSource.isPlaying)
        // {
        //     audioSource.PlayOneShot(clip);
        // }
    }

    public void SetCurrentDialogueNPC( DialogueTrigger trigger)
    {
        currentNPC = trigger;
    }

    private void EndDialogue()
    {
        if (currentNPC != null)
        {
            currentNPC.OnDialogueEnded?.Invoke();
        }
        dialogueAnimator.SetBool(IsOpen, false);
        isDialogueWindowUp = false;
    }

    IEnumerator AnimateSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }

    }
}
