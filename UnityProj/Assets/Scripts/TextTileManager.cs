using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTileManager : MonoBehaviour {

    public Canvas canvas;
    public GameObject textTilePrefab;     
    public Transform textTileParent;    
    public string[] textTileStrings;
    public GameObject submitToast;

    public GameObject dropZonePrefab;
    public Transform dropZoneParent;
    
    public List<GameObject> dropZones = new List<GameObject>(); 

    private int numberOfTiles = 0;     
    private List<GameObject> textTiles = new List<GameObject>();


    void Start()
    {   
        numberOfTiles = textTileStrings.Length;
        GenerateDropZones();
        GenerateTiles();
    }
    /*
    Generates the drop zones and places them on the canvas
    */
    void GenerateDropZones() {
        // gets the canvas size and evenly spaces the drop zones vertically 
        RectTransform canvasRectTransform = canvas.GetComponent<RectTransform>();
        Vector2 canvasSize = canvasRectTransform.sizeDelta;

        int dropZoneSpacing = (int)(canvasSize.y / (numberOfTiles + 1));

        // creates each drop zone instance and places it on the canvas
        for (int i = 0; i < numberOfTiles; i++)
        {
            GameObject newDropZone = Instantiate(dropZonePrefab, dropZoneParent);
            RectTransform dropZoneRect = newDropZone.GetComponent<RectTransform>();
    
            newDropZone.name = "DropZone" + i;

            int dropZonePosition = dropZoneSpacing * (numberOfTiles / 2) - (dropZoneSpacing * i);
            dropZoneRect.anchoredPosition = new Vector2(0, dropZonePosition);

            dropZones.Add(newDropZone);
        }
    }
    /*
    Generates the tiles and places them on the canvas
    */
    void GenerateTiles()
    {
        string[] shuffledTextTileStrings = reshuffle(textTileStrings);

        // gets the canvas size and evenly spaces the tiles vertically 
        RectTransform canvasRectTransform = canvas.GetComponent<RectTransform>();
        Vector2 canvasSize = canvasRectTransform.sizeDelta;

        int tileSpacing = (int)(canvasSize.y / (numberOfTiles + 1));

        // creates each tile instance and places it on the canvas
        for (int i = 0; i < numberOfTiles; i++)
        {
            GameObject newTile = Instantiate(textTilePrefab, textTileParent);
            
            DraggableTileScript draggableTileScript = newTile.GetComponent<DraggableTileScript>();
            draggableTileScript.tileID = i + 1;
            draggableTileScript.dropZones = dropZones;
            
            RectTransform tileRect = newTile.GetComponent<RectTransform>();
            Text tileText = newTile.GetComponentInChildren<Text>();

            int tilePosition = tileSpacing * ( numberOfTiles / 2 ) - (tileSpacing * i);
            tileRect.anchoredPosition = new Vector2(0, tilePosition);  
            tileText.text = shuffledTextTileStrings[i];

            textTiles.Add(newTile);
        }
    }

    /**
    * Shuffles the given array of strings
    */
    string[] reshuffle(string[] texts)
    {
        string[] shuffledTexts = (string[])texts.Clone();

        for (int t = 0; t < shuffledTexts.Length; t++)
        {
            string tmp = shuffledTexts[t];
            int r = Random.Range(t, shuffledTexts.Length);
            shuffledTexts[t] = shuffledTexts[r];
            shuffledTexts[r] = tmp;
        }

        return shuffledTexts;
    }

    /**
    * Validates the order of the tiles in the drop zones.
    */
    public bool ValidateOrder()
    {
        string[] currentOrder = new string[dropZones.Count];
        for (int i = 0; i < dropZones.Count; i++)
        {
            DropZoneScript dropZoneScript = dropZones[i].GetComponent<DropZoneScript>();
            DraggableTileScript draggableTileScript = dropZoneScript.currentTile.GetComponent<DraggableTileScript>();

            Text tileText = draggableTileScript.GetComponentInChildren<Text>();
            
            currentOrder[i] = tileText.text;
            // Debug.Log("Current Order: " + string.Join(", ", currentOrder));
            // Debug.Log("Text Tile Strings: " + string.Join(", ", textTileStrings));
        }
        for (int i = 0; i < currentOrder.Length; i++)
        {
            if (currentOrder[i] != textTileStrings[i])
            {
                return false;
            }
        }
        return true;
    }

    /**
    * What to do when the user clicks the Submit button
    */ 
    public void Submit()
    {
        Image submitToastImage = submitToast.GetComponent<Image>();
        Text submitToastText = submitToast.GetComponentInChildren<Text>();

        if (ValidateOrder())
        {
            Debug.Log("Correct!");
            ShowToastMessage("Correct!", Color.green);
        }
        else
        {
            Debug.Log("Incorrect!");
            ShowToastMessage("Incorrect!", Color.red);
        }
    }

    /**
    * https://discussions.unity.com/t/how-to-make-text-pop-up-for-a-few-seconds/81183/3
    * Displays the toast message with the given message and background color
    */ 
    private void ShowToastMessage(string message, Color backgroundColor)
    {
        Image submitToastImage = submitToast.GetComponent<Image>();
        Text submitToastText = submitToast.GetComponentInChildren<Text>();

        // Set message and background color
        submitToast.SetActive(true);
        submitToastText.text = message;
        submitToastImage.color = backgroundColor;

        // Start coroutine to hide the toast after 1 second
        StartCoroutine(HideToastAfterDelay(1f)); 
    }

    /**
    * Coroutine to hide the toast message after the given delay.
    */ 
    private IEnumerator HideToastAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        submitToast.SetActive(false);
    }
}