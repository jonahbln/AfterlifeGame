using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.UI;
using System;

// https://www.reddit.com/r/gamedev/comments/ps66nk/heres_a_tutorial_i_put_together_on_making_a/
// https://discussions.unity.com/t/unity-inkintegration-causes-problem-with-build/257000
public class CharacterDialogueManager : MonoBehaviour
{

    public TextAsset[] inkStoryFiles;

    private Text dialogueText;
    private Story currentStory;
    private int currentStoryIndex = 0;
    private bool dialogueIsPlaying;

    // Start is called before the first frame update
    void Start()
    {
        dialogueText = GameObject.Find("CharacterProfile/Description").GetComponent<Text>();
        dialogueIsPlaying = false;
        EnterDialogueMode();
    }

    // load the story at the specified index
    void LoadStory(int index)
    {
        if (index >= 0 && index < inkStoryFiles.Length)
        {
            currentStory = new Story(inkStoryFiles[index].text);
            currentStoryIndex = index;
        }
    }

    /**
    Enters the dialogue mode and loads the story at the current story index
    */
    public void EnterDialogueMode() {
        currentStory = new Story(inkStoryFiles[currentStoryIndex].text);
        dialogueIsPlaying = true;
        if (currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();
        }
        else
        {
            ExitDialogueMode();
        }
    }

    /**
    Exits the dialogue mode
    */
    public void ExitDialogueMode() {
        dialogueIsPlaying = false;
        dialogueText.text = "";
    }

    /**
    Advances to the next set of dialogue
    */
    public void NextDialogue()
    {
        int nextIndex = (currentStoryIndex + 1) % inkStoryFiles.Length;
    }

    /**
    Rewind to the previous set of dialogue
    */
    public void PreviousDialogue()
    {
        int nextIndex = Math.Max(0, (currentStoryIndex - 1)) % inkStoryFiles.Length;
    }
}
