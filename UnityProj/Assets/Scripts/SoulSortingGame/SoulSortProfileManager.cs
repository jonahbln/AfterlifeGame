using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
This class manages the character profiles used for the soul sorting game
*/
public class SoulSortProfileManager : MonoBehaviour
{
    public List<CharacterProfileScriptableObject> allCharacterProfiles;
    public GameObject toastPopup;

    private CharacterProfileScriptableObject currentCharacterProfile; // the current profile we are sorting
    private Queue<CharacterProfileScriptableObject> characterProfiles;  // the queue of profiles to sort
    private List<CharacterProfileScriptableObject> yesCharacterProfiles; // the profiles the player has said yes to 
    private List<CharacterProfileScriptableObject> noCharacterProfiles; // the profiles the player has said no to 
    private Button yesButton;
    private Button noButton;

    // Start is called before the first frame update
    void Start()
    {
        yesButton = GameObject.Find("YesButton").GetComponent<Button>();
        noButton = GameObject.Find("NoButton").GetComponent<Button>();
        characterProfiles = new Queue<CharacterProfileScriptableObject>(allCharacterProfiles);
        yesCharacterProfiles = new List<CharacterProfileScriptableObject>();
        noCharacterProfiles = new List<CharacterProfileScriptableObject>();
        QueueCharacterProfile();
    }

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
        Text nameText = GameObject.Find("CharacterProfile/Name").GetComponent<Text>();
        Text descriptionText = GameObject.Find("CharacterProfile/Description").GetComponent<Text>();
        nameText.text = currentCharacterProfile.characterName;
        descriptionText.text = currentCharacterProfile.characterDescription;
    }

    /**
    When the yes button is clicked, sort the current profile to the yes list
    */
    public void onYesButtonClicked() {
        ShowToastMessage("It seems like you have chosen wisely", Color.green);
        if (hasMoreProfilesToSort()) {
            Debug.Log("Yes button clicked. Current profile: " + currentCharacterProfile.name);
            yesCharacterProfiles.Add(currentCharacterProfile);
            QueueCharacterProfile();
        }
    }

    /**
    When the no button is clicked, sort the current profile to the no list
    */
    public void onNoButtonClicked() {
        ShowToastMessage("It seems like you have chosen poorly", Color.red);
        if (hasMoreProfilesToSort()) {
            Debug.Log("No button clicked. Current profile: " + currentCharacterProfile.characterName);
            noCharacterProfiles.Add(currentCharacterProfile);
            QueueCharacterProfile();
        }
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
        toastPopup.SetActive(true);
        toastPopupText.text = message;
        toastPopupImage.color = backgroundColor;

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
}
