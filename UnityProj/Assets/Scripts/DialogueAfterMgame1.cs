using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueAfterMgame1 : MonoBehaviour
{
    BasicInkExample inkScript;
    AudioSource source;
    bool notPlaying = true;

    void Start()
    {
        inkScript = FindAnyObjectByType<BasicInkExample>();
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (notPlaying && inkScript.story.currentChoices[0].text != "Look around")
        {
            source.Play();
            notPlaying = false;
        }
    }
}
