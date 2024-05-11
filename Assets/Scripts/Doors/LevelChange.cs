using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
    public Animator animatorChangeLevel;
    public GameObject LoadingScreen;

    public void OnFadeCompleted()
    {
        int level = animatorChangeLevel.GetInteger("level");
        AsyncOperation operation = SceneManager.LoadSceneAsync(level);
        LoadingScreen.SetActive(true);
    }
}
