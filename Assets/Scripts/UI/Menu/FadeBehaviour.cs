using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeBehaviour : MonoBehaviour
{
    [SerializeField] private CanvasGroup UIGroup;

    private IEnumerator coroutine;
    public int StartGameSeconds;

    [SerializeField] private bool fadeIn = false;
    [SerializeField] private bool fadeOut = false;

    public bool fadeOnStart;

    public void ShowUI()
    {
        fadeIn = true;
    }

    public void HideUI()
    {
        fadeOut = true;
    }

    void Start()
    {
        if (fadeOnStart == true)
        {
            coroutine = StartFade(StartGameSeconds);
            StartCoroutine(coroutine);
        }
    }

    void Update()
    {
        if (fadeIn)
        {
            if (UIGroup.alpha < 1)
            {
                UIGroup.alpha += Time.deltaTime;
                if (UIGroup.alpha >= 1)
                {
                    fadeIn = false;
                    UIGroup.gameObject.GetComponent<GraphicRaycaster>().enabled = true;
                }
            }
        }

        if (fadeOut)
        {
            UIGroup.gameObject.GetComponent<GraphicRaycaster>().enabled = false;
            if (UIGroup.alpha >= 0)
            {
                UIGroup.alpha -= Time.deltaTime;
                if (UIGroup.alpha == 0)
                {
                    fadeOut = false;
                }
            }
        }
    }

    private IEnumerator StartFade(int StartGameSeconds)
    {
        yield return new WaitForSeconds(StartGameSeconds);
        ShowUI();
    }
}
