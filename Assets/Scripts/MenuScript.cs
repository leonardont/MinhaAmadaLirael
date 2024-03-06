using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void JogarButton()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    public void mostrarOpcoesDuranteJogoButton()
    {
        StartCoroutine(SetActive());
    }

    public IEnumerator SetActive()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        asyncOperation.allowSceneActivation = false;
        //When the load is still in progress, output the Text and progress bar
        while (!asyncOperation.isDone)
        {
            if (asyncOperation.progress >= 0.9f)
            {
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }

        if (asyncOperation.allowSceneActivation == true)
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("SceneMenuJogo"));
    }

    public void SairButton()
    {
        Application.Quit();
    }
}
