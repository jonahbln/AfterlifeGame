using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBeforeMgame4: MonoBehaviour
{
    BasicInkExample inkScript;
    AudioSource source;
    Image background;
    bool notPlaying = true;
    public Sprite godImage;

    void Start()
    {
        inkScript = FindAnyObjectByType<BasicInkExample>();
        source = GetComponent<AudioSource>();
        background = transform.GetChild(0).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (notPlaying && inkScript.story.currentChoices[0].text != "look around")
        {
            source.Play();
            notPlaying = false;
            background.sprite = godImage;
        }
    }
}
