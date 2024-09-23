using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// video on draggable UI
// https://www.youtube.com/watch?v=CuAEp4x2vnQ
public class DraggableTileScript : MonoBehaviour, IDragHandler 
{
    public Canvas canvas;

    private RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // overrides the OnDrag method
    void IDragHandler.OnDrag(PointerEventData eventData) 
    {
        // Calculate new position
        Vector2 newPosition = rectTransform.anchoredPosition + eventData.delta / canvas.scaleFactor;

        // Get canvas size
        RectTransform canvasRectTransform = canvas.GetComponent<RectTransform>();
        Vector2 canvasSize = canvasRectTransform.sizeDelta;

        // Get the width and height of the RectTransform
        Vector2 tileSize = rectTransform.sizeDelta;

        // Calculate min and max bounds
        float minX = -canvasSize.x / 2 + tileSize.x / 2;
        float maxX = canvasSize.x / 2 - tileSize.x / 2;
        float minY = -canvasSize.y / 2 + tileSize.y / 2;
        float maxY = canvasSize.y / 2 - tileSize.y / 2;

        // Clamp the position within the bounds
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        // Set the new position
        rectTransform.anchoredPosition = newPosition;
    }
}
