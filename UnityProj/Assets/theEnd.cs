using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class theEnd : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private float timePassed = 0f;
    public float fadeDuration = 3f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        originalColor.a = 0f;
        spriteRenderer.color = originalColor;
    }

    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed <= fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, timePassed / fadeDuration);
            originalColor.a = alpha;
            spriteRenderer.color = originalColor;
        }
        else
        {
            Invoke("goToNextScene", 1.5f);
        }
    }

    void goToNextScene()
    {
        FindObjectOfType<SceneTransition>().LoadNextScene();
    }
}
