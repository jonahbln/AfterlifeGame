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
        inkScript.textPrefab.color = Color.black;
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
        
        if (inkScript.story.currentChoices[0].text == "I- I'm Dead?" && storyStarted)
        {
            imageLocation.sprite = godBackground;
            textCanvas.GetComponent<VerticalLayoutGroup>().childAlignment = TextAnchor.LowerCenter;
            textBoxController.enabled = true;
            inkScript.textPrefab = textPrefab;
            textBoxCanvas.transform.GetChild(1).gameObject.SetActive(true);
        }
    }
}
