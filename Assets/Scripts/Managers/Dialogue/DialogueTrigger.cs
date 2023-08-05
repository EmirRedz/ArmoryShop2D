using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public UnityEvent OnDialogueEnded;
    
    public void TriggerDialogue()
    {
        DialogueManager.Instance.currentTime = DialogueManager.Instance.timer;
        DialogueManager.Instance.StartDialogue(dialogue);
        DialogueManager.Instance.SetCurrentDialogueNPC(this);
    }
}
