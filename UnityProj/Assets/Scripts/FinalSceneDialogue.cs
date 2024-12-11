using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

// The purpose of this script is to control the dialogue of the final scene of the game.
// This might be a janky way to do it, but it matches the sequence documented here.
// https://docs.google.com/document/d/19zBlph5LiX08tPz7AJlYZv9ZvIXaARPI2VZ05_jn4Jo/edit?tab=t.0
public class FinalSceneDialogue : MonoBehaviour
{
    public BasicInkExample inkScript;
    public List<Sprite> backgrounds; // list of backgrounds in the correct order
    public Image backgroundImage; // reference to the image component of the background
    public List<AudioClip> audioClips; // list of audio clips in the correct order
    public AudioSource audioSource; // reference to the audio source
    public TextBoxController textBoxController;
    public Canvas textCanvas;
    public Canvas textBoxCanvas;
    bool storyStarted = false;

    private int backgroundIndex = 0;
    private List<int> backgroundTriggers = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0 }; // used so that we don't update the background multiple times
    private int audioIndex = 0;
    private List<int> audioTriggers = new List<int> { 0, 0, 0, 0, 0 }; // used so that we don't update the audio multiple times

    void Start()
    {
        UpdateBackground();
        UpdateBackgroundTrigger(0);

        UpdateAudio();
        UpdateAudioTrigger(0);

        textBoxCanvas.transform.GetChild(1).GetComponent<Image>().color = Color.white;
    }

    // updates the background image by increasing the index
    private void UpdateBackground() {
        if (backgroundIndex >= backgrounds.Count) {
            return;
        }
        backgroundImage.sprite = backgrounds[backgroundIndex];
        backgroundIndex++;
    }

    // updates the audio clip by increasing the index
    private void UpdateAudio(bool loop = true) {
        if (audioIndex >= audioClips.Count) {
            return;
        }
        audioSource.clip = audioClips[audioIndex];
        audioIndex++;

        audioSource.loop = loop;
        audioSource.Play();
    }

    /**
    * Updates the background trigger at the given index to 1 to indicate that the background has been updated.
    */
    private void UpdateBackgroundTrigger(int index) {
        backgroundTriggers[index] = 1;
    }

    /**
    * Updates the audio trigger at the given index to 1 to indicate that the audio has been updated.
    */
    private void UpdateAudioTrigger(int index) {
        audioTriggers[index] = 1;
    }

    // Update is called once per frame
    void Update()
    {
        // switch to colorful_wideshot_scary_goddess_of_renewal
        if (inkScript.story.currentChoices.Count > 0 && inkScript.story.currentChoices[0].text.StartsWith("Walk up to her") && backgroundTriggers[1] == 0) {
                            Debug.Log("Switching background 1");

            UpdateBackground();
            UpdateBackgroundTrigger(1);
        }

        // switch to bw_wideshot_goddess_of_renewal
        if (inkScript.story.currentText.StartsWith("[YOU] Oh shit.") && backgroundTriggers[2] == 0) {
                                        Debug.Log("Switching background 2");

            UpdateBackground();
            UpdateBackgroundTrigger(2);

            UpdateAudio();
            UpdateAudioTrigger(1);
        }

        // switch to scary_goddess_of_renewal
        if (inkScript.story.currentText.StartsWith("[RATHE] Who are you?") && backgroundTriggers[3] == 0) {
                                        Debug.Log("Switching background 3");

            UpdateBackground();
            UpdateBackgroundTrigger(3);

        }

        // switch to scary_w_anvolto
        if (inkScript.story.currentText.StartsWith("[ANVOLTO] They earned it. I sent them to Eon.") && backgroundTriggers[4] == 0) {
                                        Debug.Log("Switching background 4");

            UpdateBackground();
            UpdateBackgroundTrigger(4);

            UpdateAudio(false);
            UpdateAudioTrigger(2);
        }

        // switch to all_gods_scary
        if (inkScript.story.currentText.StartsWith("[EON] I concur, I was the one") && backgroundTriggers[5] == 0) {
                                        Debug.Log("Switching background 5");

            UpdateBackground();
            UpdateBackgroundTrigger(5);

            UpdateAudio(false);
            UpdateAudioTrigger(3);
        }

        // switch to all_gods_serious_rathe
        if (inkScript.story.currentText.StartsWith("[RATHE to ANVOLTO and EON] …What the hell") && backgroundTriggers[6] == 0) {
                                        Debug.Log("Switching background 6");

            UpdateBackground();
            UpdateBackgroundTrigger(6);

            UpdateAudio();
            UpdateAudioTrigger(4);
        }

        // switch to all_gods_happy
        if ((inkScript.story.currentText.StartsWith("[RATHE] Oh, no worries") || 
            inkScript.story.currentText.StartsWith("[RATHE] Sorry, they") || 
            inkScript.story.currentText.StartsWith("[RATHE] You could say that")) && backgroundTriggers[7] == 0) {

                Debug.Log("Switching background 7");
            UpdateBackground();
            UpdateBackgroundTrigger(7);
        }    
    }
}
