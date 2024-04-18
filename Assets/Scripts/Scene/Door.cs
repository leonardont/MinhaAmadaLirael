using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;
using UnityEngine.SceneManagement;
using System;

public class Door : MonoBehaviour
{
    public Animator animator;
    public Animator animatorChangeLevel;
    private int level;

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        animator.SetBool("isOpen",true);
        string tag = this.tag;
        level = Convert.ToInt32(tag.Replace("door", ""));
    }

    private void OnCollisionExit(Collision collision)
    {
        animator.SetBool("isOpen", false);
    }

    public void FadeToLevel()
    {
        animatorChangeLevel.SetTrigger("FadeIn");
    }

    public void OnFadeCompleted()
    {
        SceneManager.LoadScene(level);
    }
}
