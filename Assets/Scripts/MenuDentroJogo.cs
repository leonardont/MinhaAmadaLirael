using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuDentroJogo : MonoBehaviour
{
   
    public void voltarMenuButton()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void voltarJogoButton()
    {
        SceneManager.UnloadSceneAsync(1);
    }

}
