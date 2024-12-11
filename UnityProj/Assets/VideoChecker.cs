using UnityEngine;
using UnityEngine.Video;

public class VideoChecker : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    private float timePassed = 0f;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    void Update()
    {
        timePassed += Time.deltaTime;
        if (timePassed > 10f && !videoPlayer.isPlaying)
        {
            FindObjectOfType<SceneTransition>().LoadNextScene();
        }
    }
}