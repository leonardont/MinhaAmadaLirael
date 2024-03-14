using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuDentroJogo : MonoBehaviour
{
   
    public void voltarMenuButton()
    {
        SceneManager.LoadScene("SceneMenu", LoadSceneMode.Single);
    }

    public void voltarJogoButton()
    {
        SceneManager.UnloadSceneAsync(1);
    }

}
