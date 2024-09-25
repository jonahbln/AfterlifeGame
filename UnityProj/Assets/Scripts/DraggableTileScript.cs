using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// video on draggable UI
// https://www.youtube.com/watch?v=CuAEp4x2vnQ
public class DraggableTileScript : MonoBehaviour, IDragHandler, IEndDragHandler
{
    
    public List<GameObject> dropZones;
    public int tileID;

    private RectTransform rectTransform;
    private Vector2 originalPosition;
    private Canvas canvas;
    private string canvasUITag = "CanvasUI";
    private string dropZoneTag = "DropZoneTag";

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition;
        canvas = GameObject.FindGameObjectWithTag(canvasUITag).GetComponent<Canvas>();
        InitializeDropZones();
    }

    void InitializeDropZones()
    {
        dropZones = new List<GameObject>();
        GameObject[] dropZoneObjects = GameObject.FindGameObjectsWithTag(dropZoneTag);
        foreach (var dropZoneObject in dropZoneObjects)
        {
            dropZones.Add(dropZoneObject);
        }

        RectTransform closestDropZoneRect = null;
        DropZoneScript closestDropZoneScript = null;
        float closestDistance = Mathf.Infinity;

        foreach (var dropZone in dropZones)
        {
            RectTransform dropZoneRect = dropZone.GetComponent<RectTransform>();
            float distance = Vector2.Distance(rectTransform.anchoredPosition, dropZoneRect.anchoredPosition);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestDropZoneRect = dropZoneRect;
                closestDropZoneScript = dropZone.GetComponent<DropZoneScript>();
            }
        }

        if (closestDropZoneScript != null)
        {
            closestDropZoneScript.currentTile = gameObject;
            originalPosition = closestDropZoneRect.anchoredPosition;
        }

        // for (int i = 0; i < dropZones.Count; i++)
        // {   
        //     Debug.Log(i);
        //     Debug.Log(dropZones[i].GetComponent<DropZoneScript>().currentTile);
        // }

    }

    // overrides the OnDrag method
    void IDragHandler.OnDrag(PointerEventData eventData) 
    {
        Vector2 newPosition = rectTransform.anchoredPosition + eventData.delta / canvas.scaleFactor;

        RectTransform canvasRectTransform = canvas.GetComponent<RectTransform>();
        Vector2 canvasSize = canvasRectTransform.sizeDelta;

        Vector2 tileSize = rectTransform.sizeDelta;

        float minX = -canvasSize.x / 2 + tileSize.x / 2;
        float maxX = canvasSize.x / 2 - tileSize.x / 2;
        float minY = -canvasSize.y / 2 + tileSize.y / 2;
        float maxY = canvasSize.y / 2 - tileSize.y / 2;

        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        rectTransform.anchoredPosition = newPosition;
    }

    /**
    * Overrides the OnEndDrag method
    */
    public void OnEndDrag(PointerEventData eventData)
    {
        SnapToClosestDropZone();
    }

    /**
    * Updates the original position of this tile.
    */
    public void UpdateOriginalPosition()
    {
        originalPosition = rectTransform.anchoredPosition;
    }

    /**
    * Returns a reference to the original drop zone
    */
    private GameObject GetOriginalDropZone() {
        GameObject closestDropZone = null;
        float closestDistance = Mathf.Infinity;

        foreach (var dropZone in dropZones)
        {
            RectTransform dropZoneRect = dropZone.GetComponent<RectTransform>();
            float distance = Vector2.Distance(originalPosition, dropZoneRect.anchoredPosition);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestDropZone = dropZone;
            }
        }
        return closestDropZone;
    }

    // snaps to the closest drop zone 
    private void SnapToClosestDropZone()
    {
        GameObject otherDropZone = null;
        float closestDistance = Mathf.Infinity;

        foreach (var dropZone in dropZones)
        {
            RectTransform dropZoneRect = dropZone.GetComponent<RectTransform>();
            float distance = Vector2.Distance(rectTransform.anchoredPosition, dropZoneRect.anchoredPosition);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                otherDropZone = dropZone;
            }
        }

        DropZoneScript otherDropZoneScript = otherDropZone.GetComponent<DropZoneScript>();
        if (otherDropZoneScript != null) 
        {   
            GameObject otherDraggableTile = otherDropZoneScript.currentTile;
            if (otherDraggableTile != gameObject) {
                GameObject originalDropZone = GetOriginalDropZone();

                RectTransform otherDropZoneRect = otherDropZone.GetComponent<RectTransform>();

                DropZoneScript originalDropZoneScript = originalDropZone.GetComponent<DropZoneScript>();
                RectTransform otherDraggableTileRect = otherDraggableTile.GetComponent<RectTransform>();
                DraggableTileScript otherDraggableTileScript = otherDraggableTile.GetComponent<DraggableTileScript>();

                originalDropZoneScript.currentTile = otherDropZoneScript.currentTile;
                otherDropZoneScript.currentTile = gameObject;

                rectTransform.anchoredPosition = otherDropZoneRect.anchoredPosition;
                otherDraggableTileRect.anchoredPosition = originalPosition;

                otherDraggableTileScript.UpdateOriginalPosition();
                this.UpdateOriginalPosition();
            } else {
                rectTransform.anchoredPosition = originalPosition;
            }
        }
    }
}
