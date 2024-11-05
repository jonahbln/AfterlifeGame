using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterProfile", menuName = "ScriptableObjects/CharacterProfile", order = 1)]
public class CharacterProfileScriptableObject : ScriptableObject
{
    [SerializeField]
    public string characterName;

    [SerializeField]
    public string characterDescription;

    // a list of string for character dialogue
    [SerializeField]
    public string[] characterDialogue; 

    // the ink file for the character's dialogue
    [SerializeField]
    public TextAsset inkJSONAsset;

    // true if the character is a good character, false if the character is a bad character
    [SerializeField]
    public bool verdict; 
}
