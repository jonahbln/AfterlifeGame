using Ink.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class PrologueDialogue : MonoBehaviour
{
    public static event Action<Story> OnCreateStory;
    public Canvas textBox;
    public Image image;
    public GameObject doorRight;
    public GameObject doorMiddle;
    public GameObject doorLeft;
    private bool paused;
    private bool doorClickedAlready = false;
    private Choice currentOption;

    void Awake()
    {
        // Remove the default message
        RemoveChildren();
        StartStory();
    }

    private void Update()
    {
        if (paused)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !doorClickedAlready)
            {
                OnClickChoiceButton(currentOption);
            }
            else if (Input.GetKeyDown(KeyCode.Space) )
            {
                FindObjectOfType<SceneTransition>().LoadNextScene();
            }
        }
    }

    // Creates a new Story object with the compiled story which we can then play!
    public void StartStory()
    {
        story = new Story(inkJSONAsset.text);
        if (OnCreateStory != null) OnCreateStory(story);
        RefreshView();
    }

    // This is the main function called every time the story changes. It does a few things:
    // Destroys all the old content and choices.
    // Continues over all the lines of text, then displays all the choices. If there are no choices, the story is finished!
    void RefreshView()
    {
        // Remove all the UI on screen
        RemoveChildren();

        // Read all the content until we can't continue any more
        while (story.canContinue)
        {
            // Continue gets the next line of the story
            string text = story.Continue();
            // This removes any white space from the text.
            text = text.Trim();
            // Display the text on screen!
            CreateContentView(text);
        }

        // Display all the choices, if there are any!
        if (story.currentChoices.Count > 0)
        {
            for (int i = 0; i < story.currentChoices.Count; i++)
            {
                Choice choice = story.currentChoices[i];
                if (choice.text.Trim() == "next")
                {
                    currentOption = choice;
                    textBox.GetComponent<TextBoxController>().Resize();
                }
                else
                {
                    Button button = CreateChoiceView(choice.text.Trim());
                    // Tell the button what to do when we press it
                    button.onClick.AddListener(delegate {
                        OnClickChoiceButton(choice);
                    });
                }
            }
        }
        // If we've read all the content and there's no choices, the story is finished!
        else
        {
            doorbutton.interactable = true;
        }


    }

    // When we click the choice button, tell the story to choose that choice!
    void OnClickChoiceButton(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        RefreshView();
        paused = false;
        image.gameObject.SetActive(true);
        doorRight.gameObject.SetActive(true);
        doorMiddle.gameObject.SetActive(true);
        doorLeft.gameObject.SetActive(true);
    }

    // Creates a textbox showing the the line of text
    void CreateContentView(string text)
    {
        textBox.GetComponent<TextBoxController>().Resize(text, story.currentChoices.Count);

        Text storyText = Instantiate(textPrefab) as Text;

        storyText.transform.SetParent(canvas.transform, false);
        StartCoroutine(EnterFullText(storyText, text));


    }

    IEnumerator EnterFullText(Text t, string print)
    {
        char[] fullTxtArray = print.ToCharArray();
        string nextTxt = " ";
        foreach (char c in print)
        {
            if (nextTxt[nextTxt.Length - 1] == '/' && c == 'n')
            {
                nextTxt = nextTxt.Substring(0, nextTxt.Length - 2);
                nextTxt += '\n';
            }
            else
            {
                nextTxt += c;
            }


            t.text = nextTxt;

            yield return new WaitForSeconds(0.025f);
        }
        paused = true;
    }

    // Creates a button showing the choice text
    Button CreateChoiceView(string text)
    {
        // Creates the button from a prefab
        Button choice = Instantiate(buttonPrefab) as Button;
        choice.transform.SetParent(canvas.transform, false);

        // Gets the text from the button prefab
        Text choiceText = choice.GetComponentInChildren<Text>();
        choiceText.text = text;
        choiceText.fontSize = choiceTextSize;

        // Make the button expand to fit the text
        HorizontalLayoutGroup layoutGroup = choice.GetComponent<HorizontalLayoutGroup>();
        layoutGroup.childForceExpandHeight = false;

        return choice;
    }

    // Destroys all the children of this gameobject (all the UI)
    void RemoveChildren()
    {
        int childCount = canvas.transform.childCount;
        for (int i = childCount - 1; i >= 0; --i)
        {
            Destroy(canvas.transform.GetChild(i).gameObject);
        }
    }

    public void doorClicked()
    {
        RemoveChildren();
        doorRight.gameObject.SetActive(false);
        doorMiddle.gameObject.SetActive(false);
        doorLeft.gameObject.SetActive(false);
        image.sprite = keyPadImage;
        doorbutton.interactable = false;
        // Continue gets the next line of the story
        string text = "You walk up to the door, it is locked. But there is a keypad.";
        // This removes any white space from the text.
        text = text.Trim();
        // Display the text on screen!
        CreateContentView(text);
        paused = true;
        doorClickedAlready = true;
    }

    [SerializeField]
    private TextAsset inkJSONAsset = null;
    public Story story;

    [SerializeField]
    private Canvas canvas = null;

    // UI Prefabs
    [SerializeField]
    private Text textPrefab = null;
    [SerializeField]
    private Button buttonPrefab = null;
    [SerializeField]
    private Font choiceFont;
    [SerializeField]
    private Button doorbutton;
    [SerializeField]
    private Sprite keyPadImage;
    [SerializeField]
    private int choiceTextSize = 24;
}
