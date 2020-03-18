using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class PlayVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string SceneName;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer.prepareCompleted += VideoPlayer_prepareCompleted;
        videoPlayer.Prepare();
        Debug.Log("does this even run");
    }

    private void VideoPlayer_prepareCompleted(VideoPlayer source)
    {
        videoPlayer.Play();
        Debug.Log("this actually runs");
    }
    // Update is called once per frame
    void Update()
    {
        videoPlayer.prepareCompleted += VideoPlayer_prepareCompleted;
        videoPlayer.loopPointReached += LoadScene;
    }

    void LoadScene(VideoPlayer vp)
    {

        Debug.Log("OOOOOOOOOOOOOOOOO");
        SceneManager.LoadScene(SceneName);
    }
}
