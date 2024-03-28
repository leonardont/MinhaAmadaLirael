using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInGame : MonoBehaviour
{
   
    public void backMenuButton()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    public void backGameButton()
    {
        SceneManager.UnloadSceneAsync(1);
    }

}
