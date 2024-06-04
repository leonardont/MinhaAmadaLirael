using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class IntroBehaviour : MonoBehaviour
{
    public GameObject LoadingScreen;
    VideoPlayer video;

    void Awake()
    {
        video = GetComponent<VideoPlayer>();
        video.Play();
        video.loopPointReached += CheckOver;    
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            LoadingScreen.SetActive(true);
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
        LoadingScreen.SetActive(true);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
