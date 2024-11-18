using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Reference to the VideoPlayer
    public GameObject canvas;       // Reference to the Canvas

    void Start()
    {
        // Subscribe to the video end event
        videoPlayer.loopPointReached += OnVideoFinished;

        // Ensure the Canvas is disabled at the start
        canvas.SetActive(false);
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        // Enable the Canvas after the video finishes
        canvas.SetActive(true);
    }
}
