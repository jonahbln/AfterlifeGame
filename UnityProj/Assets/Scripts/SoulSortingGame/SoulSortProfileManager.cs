using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;

/**
This class manages the character profiles used for the soul sorting game
*/
public class SoulSortProfileManager : MonoBehaviour
{
    /** The assortment of soul game objects*/
    [SerializeField]
    public List<GameObject> soulPrefabs;

    public List<CharacterProfileScriptableObject> allCharacterProfiles;
    public GameObject toastPopup;

    public GameObject verdictScreen; // verdict screen component

    private int currentProfileIndex; 
    private CharacterProfileScriptableObject currentCharacterProfile; // the current profile we are sorting
    private Queue<CharacterProfileScriptableObject> characterProfiles;  // the queue of profiles to sort
    private List<CharacterProfileScriptableObject> yesCharacterProfiles; // the profiles the player has said yes to 
    private List<CharacterProfileScriptableObject> noCharacterProfiles; // the profiles the player has said no to 
    private Button yesButton;
    private Button noButton;
    private CallableInkDialogue inkDialogueManager;
    private Color correctBackgroundColor = new Color(100f / 255f, 200f / 255f, 100f/ 255f, 1f);
    private Color incorrectBackgroundColor = new Color(94f / 255f, 5f / 255f, 5f/ 255f, 1f);

    #region Unity methods 
    // Start is called before the first frame update
    void Start()
    {
        inkDialogueManager = FindObjectOfType<CallableInkDialogue>();
        yesButton = GameObject.Find("YesButton").GetComponent<Button>();
        noButton = GameObject.Find("NoButton").GetComponent<Button>();
        toastPopup.SetActive(false);
        Restart();
    }


    #endregion 


    /**
    Are there more profiles to sort?
    */
    bool hasMoreProfilesToSort() {
        return characterProfiles.Count > 0;
    }

    /**
    Call to dequeue the first character profile in queue and set them as the current character and update the display
    */
    void QueueCharacterProfile() {
        if (hasMoreProfilesToSort()) {
            CharacterProfileScriptableObject newCurrentProfile = characterProfiles.Dequeue();
            currentCharacterProfile = newCurrentProfile;
            UpdateCurrentCharacterProfileDisplay();
        }
    }

    /**
    Updates the information displayed about the character profile
    */
    void UpdateCurrentCharacterProfileDisplay() {
        inkDialogueManager.MountStory(currentCharacterProfile.inkJSONAsset);
        inkDialogueManager.StartStory();
        Text nameText = GameObject.Find("NameText").GetComponent<Text>();
        // Text nameText = GameObject.Find("CharacterProfile/Name").GetComponent<Text>();
        // Text descriptionText = GameObject.Find("CharacterProfile/Description").GetComponent<Text>();

        // string dialogues = "";
        // if (currentCharacterProfile.characterDialogue.Count() > 0) {
        //     dialogues = currentCharacterProfile.characterDialogue.Aggregate((i, j) => i + "\n" + j);
        // }
        SpawnNewSoulPrefab();

        nameText.text = currentCharacterProfile.characterName.Trim();
        if (currentCharacterProfile.characterDescription != null && currentCharacterProfile.characterDescription != "") {
            nameText.text += " [" + currentCharacterProfile.characterDescription.Trim() + "]";
        }
        // descriptionText.text = dialogues;
    }
    
    /**
    * Spawns a new soul prefab to the scene. 
    * This method should be called when the player has made a verdict on the current soul
    * and a new soul needs to be spawned.
    */
    private void SpawnNewSoulPrefab() {
        GameObject soulPrefabContainer = GameObject.Find("SoulPrefabContainer");
        foreach (Transform child in soulPrefabContainer.transform)
        {
            Destroy(child.gameObject); 
        }

        GameObject selectedPrefab = soulPrefabs[currentProfileIndex % soulPrefabs.Count];

        GameObject spawnedSoul = Instantiate(selectedPrefab, soulPrefabContainer.transform.position, Quaternion.identity);

        spawnedSoul.transform.SetParent(soulPrefabContainer.transform);

        spawnedSoul.transform.localPosition = Vector3.zero; 
        spawnedSoul.transform.localScale = new Vector3(30f, 30f, 1f); 
    }

    /**
    When the yes button is clicked, sort the current profile to the yes list
    */
    public void onYesButtonClicked() {
        if (currentCharacterProfile.verdict) {
            ShowToastMessage("It seems like you have chosen wisely", this.correctBackgroundColor);
        } else {
            ShowToastMessage("It seems like you have chosen poorly", this.incorrectBackgroundColor);
        }
        yesCharacterProfiles.Add(currentCharacterProfile);

        OnVerdictMade();
    }

    /**
    When the no button is clicked, sort the current profile to the no list
    */
    public void onNoButtonClicked() {
        if (currentCharacterProfile.verdict) {
            ShowToastMessage("It seems like you have chosen poorly", this.incorrectBackgroundColor);
        } else {
            ShowToastMessage("It seems like you have chosen wisely", this.correctBackgroundColor);
        }
        noCharacterProfiles.Add(currentCharacterProfile);

        OnVerdictMade();
    }

    /**
    * https://discussions.unity.com/t/how-to-make-text-pop-up-for-a-few-seconds/81183/3
    * Displays the toast message with the given message and background color
    */ 
    private void ShowToastMessage(string message, Color backgroundColor)
    {
        Image toastPopupImage = toastPopup.GetComponent<Image>();
        Text toastPopupText = toastPopup.GetComponentInChildren<Text>();

        // Set message and background color
        toastPopupText.text = message;
        toastPopupImage.color = backgroundColor;
        toastPopupText.color = backgroundColor;

        toastPopup.SetActive(true);

        // Start coroutine to hide the toast after 1 second
        StartCoroutine(HideToastAfterDelay(2f)); 
    }

    /**
    * Coroutine to hide the toast message after the given delay.
    */ 
    private IEnumerator HideToastAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        toastPopup.SetActive(false);
    }

    /**
    * This method should be called when the player has made the verdict on a soul.
    * It should be called after the player has clicked the yes or no button.
    */
    private void OnVerdictMade() {
        currentProfileIndex++;
        if (hasMoreProfilesToSort()) {
            QueueCharacterProfile();
        } else {
            EvaluatePerformance();
        }
    }

    /**
    * This method should be called when the player has finished sorting all the profiles 
    * to evaluate if the player has sorted all the souls correctly. 
    */
    private void EvaluatePerformance() {
        GameObject soulPrefabContainer = GameObject.Find("SoulPrefabContainer");
        foreach (Transform child in soulPrefabContainer.transform)
        {
            Destroy(child.gameObject); 
        }

        int correctCount = 0;
        int incorrectCount = 0;
        foreach (CharacterProfileScriptableObject profile in yesCharacterProfiles) {
            if (profile.verdict) {
                correctCount++;
            } else {
                incorrectCount++;
            }
        }
        foreach (CharacterProfileScriptableObject profile in noCharacterProfiles) {
            if (!profile.verdict) {
                correctCount++;
            } else {
                incorrectCount++;
            }
        }

        if (incorrectCount < 2) {
            ShowGameResult(true);
        } else {
            ShowGameResult(false);
        }
    }

    /**
    * Restarts the game if the player has not sorted all the souls correctly
    */
    public void Restart() {
        currentProfileIndex = 0;
        characterProfiles = new Queue<CharacterProfileScriptableObject>(allCharacterProfiles);
        yesCharacterProfiles = new List<CharacterProfileScriptableObject>();
        noCharacterProfiles = new List<CharacterProfileScriptableObject>();
        QueueCharacterProfile();
    }

    /**
    * Shows the final page after the player has sorted all the souls
    */
    private void ShowGameResult(bool won)
    {
        Text verdictText = verdictScreen.GetComponentInChildren<Text>();
        Button verdictButton = verdictScreen.GetComponentInChildren<Button>();

        // Set message and background color
        verdictScreen.SetActive(true);
        verdictText.text = won ? "Well done! You have done well and managed to sort all the souls correctly." : "Mistakes cannot be made. Use better judgement next time.";
        
        verdictButton.GetComponentInChildren<Text>().text = won ? "Continue" : "Try Again";

        verdictButton.gameObject.AddComponent(typeof(EventTrigger));
        EventTrigger trigger = verdictButton.gameObject.GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
 
        entry.callback.AddListener( (eventData) => { 
            if (won) {
                FindObjectOfType<SceneTransition>().LoadNextScene();
            } else {
                Restart();
                verdictScreen.SetActive(false);
            }
        });
        trigger.triggers.Add(entry);
    }
}
