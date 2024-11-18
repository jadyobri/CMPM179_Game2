using System.Diagnostics;
using UnityEngine;
using UnityEngine.Video;

public class VideoShowingManager : MonoBehaviour
{
    public VideoPlayer videoPlayer1;
    public VideoPlayer videoPlayer2;
    public AudioSource audioSource; // Assign via Inspector
    void Start()
    {
        // Set up the video players
        videoPlayer1.loopPointReached += OnVideo1Finished;
        videoPlayer2.loopPointReached += OnVideo2Finished;
        videoPlayer1.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer1.SetTargetAudioSource(0, audioSource);
        // Start the first video
        videoPlayer1.time = 0;
        PlayVideo(videoPlayer1);
        audioSource.Play();
    }
    void Update()
    {
        if (audioSource.isPlaying)
        {
            UnityEngine.Debug.Log("Audio is playing.");
        }
        else
        {
            UnityEngine.Debug.Log("Audio is not playing.");
        }
    }
    void OnVideo1Finished(VideoPlayer vp)
    {
        // When video 1 finishes, play video 2
        videoPlayer1.Stop();
        videoPlayer2.Play();
    }

    void OnVideo2Finished(VideoPlayer vp)
    {
        // When video 2 finishes, loop back to video 1
        videoPlayer2.Stop();
        videoPlayer2.time = 0;

        videoPlayer2.Play();
    }
    void PlayVideo(VideoPlayer videoPlayer)
    {
        videoPlayer.Play(); // Play the video
        audioSource.Play(); // Play the associated audio
    }
}
