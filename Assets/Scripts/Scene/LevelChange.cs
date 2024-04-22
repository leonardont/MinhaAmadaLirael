using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
    public Animator animatorChangeLevel;
   
    public void OnFadeCompleted()
    {
        int level = animatorChangeLevel.GetInteger("level");
        SceneManager.LoadScene(level);
    }
}
