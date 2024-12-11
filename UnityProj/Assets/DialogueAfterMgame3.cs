using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DialogueAfterMgame3 : MonoBehaviour
{
    public BasicInkExample inkScript;
    public Sprite storyBackground;
    public Sprite godBackground;
    public Image imageLocation;
    public Text textPrefab;
    public Text storyTextPrefab;
    public TextBoxController textBoxController;
    public Canvas textCanvas;
    public Canvas textBoxCanvas;
    bool storyStarted = false;
    void Start()
    {
        textBoxCanvas.transform.GetChild(1).GetComponent<Image>().color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        if (inkScript.story.currentChoices[0].text != "I am ready" && !storyStarted)
        {
            storyStarted = true;
            imageLocation.sprite = storyBackground;
            textCanvas.GetComponent<VerticalLayoutGroup>().childAlignment = TextAnchor.MiddleCenter;
            textBoxController.enabled = false;
            textBoxCanvas.transform.GetChild(1).gameObject.SetActive(false);
        }
        
        if (inkScript.story.currentChoices[0].text == "I- I'm Dead?")
        {
            imageLocation.sprite = godBackground;
            textCanvas.GetComponent<VerticalLayoutGroup>().childAlignment = TextAnchor.LowerCenter;
            textBoxCanvas.transform.GetChild(1).gameObject.SetActive(true);
        }

        if (inkScript.story.currentText == "Lava flooded the city's outskirts, and dread quickly set in with the realization: you�re trapped." +
            " It was over in less than an hour, as the ground beneath your feet imploded, collapsing the city into an abyss of destruction.")
        {

            textBoxController.enabled = true;

        }
    }
}
