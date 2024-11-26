using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBoxController : MonoBehaviour
{
    public float widthScalar = 40;
    public float charPerLine = 35;
    public float heightScalar = 90;
    public float buttonHeight = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Resize(string text, int buttons)
    {
        if (buttons > 0)
        {
            transform.GetChild(1).GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Min(text.Length, 45) * widthScalar, (heightScalar * (Mathf.Clamp(Mathf.Floor(text.Length / charPerLine), 1.0f, 99.0f)) + (buttonHeight * buttons)));
        }
        else
        {
            transform.GetChild(1).GetComponent<RectTransform>().sizeDelta = new Vector2(Mathf.Min(text.Length, 45) * widthScalar, heightScalar * Mathf.Clamp(Mathf.Floor(text.Length / charPerLine), 1.0f, 99.0f));
        }
    }
}
