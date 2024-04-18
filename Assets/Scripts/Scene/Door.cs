using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private void OnTriggerEnter(Collider other)
    {
        animator.SetBool("isOpen", true);
        string tag = this.tag;
        level = Convert.ToInt32(tag.Replace("door", ""));
        Debug.Log(level);
        animatorChangeLevel.SetInteger("level", level);
    }
    public void FadeToLevel()
    {
        animatorChangeLevel.SetTrigger("FadeIn");
    }

    private void OnCollisionExit(Collision collision)
    {
        animator.SetBool("isOpen", false);
    }
}
