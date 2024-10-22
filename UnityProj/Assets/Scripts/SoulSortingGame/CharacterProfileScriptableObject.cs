using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterProfile", menuName = "ScriptableObjects/CharacterProfile", order = 1)]
public class CharacterProfileScriptableObject : ScriptableObject
{
    public string characterName;
    public string characterDescription;
    // a list of string for character dialogue
    public string[] characterDialogue; 
}
