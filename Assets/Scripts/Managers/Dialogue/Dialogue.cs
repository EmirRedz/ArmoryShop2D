using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue 
{
    public string NPCName;
    [TextArea(3,15)]
    public string[] sentences;
    public AudioClip[] clipsToPlay;
}
