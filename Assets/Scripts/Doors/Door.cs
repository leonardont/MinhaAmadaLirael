using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;
using UnityEngine.SceneManagement;
using System;
using Cinemachine;

public class Door : MonoBehaviour
{
    public Animator animator;
    public Animator animatorChangeLevel;

    [SerializeField] int TargetLevel;

    [SerializeField] public GameObject LevelManager;
    [NonSerialized] public LevelManager lmScript;
    [SerializeField] public GameObject player;
    [SerializeField] public GameObject cutsceneCamera;
    public CinemachineBrain cmBrain;
    public GameObject TargetPosition;
    public int speed = 1;
    bool camera_move_enabled = false;

    private int level;

    public AudioSource audioSource;
    public AudioClip openDoor;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (camera_move_enabled)
        {
            cmBrain.enabled = false;
            cutsceneCamera.transform.position = Vector3.Lerp(cutsceneCamera.transform.position, TargetPosition.transform.position, speed * Time.deltaTime);
            cutsceneCamera.transform.rotation = Quaternion.Lerp(cutsceneCamera.transform.rotation, TargetPosition.transform.rotation, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            lmScript = LevelManager.GetComponent<LevelManager>();

            if (this.gameObject.tag == "door5")
            {
                if (lmScript.doorLocked != true)
                {
                    audioSource.PlayOneShot(openDoor, 0.25f);

                    lmScript.canMove = false;

                    TargetPosition.transform.position = new Vector3(TargetPosition.transform.position.x, TargetPosition.transform.position.y + 6, TargetPosition.transform.position.z - 8f);
                    TargetPosition.transform.rotation = Quaternion.Euler(25, 0, 0);

                    camera_move_enabled = true;

                    animator.SetBool("isOpen", true);
                    animatorChangeLevel.SetInteger("level", (TargetLevel % 1 == 0 ? TargetLevel : 1));
                }
            }
            else
            {
                audioSource.PlayOneShot(openDoor, 0.25f);

                lmScript.canMove = false;

                TargetPosition.transform.position = new Vector3(TargetPosition.transform.position.x, TargetPosition.transform.position.y + 6, TargetPosition.transform.position.z - 8f);
                TargetPosition.transform.rotation = Quaternion.Euler(25, 0, 0);

                camera_move_enabled = true;

                animator.SetBool("isOpen", true);
                animatorChangeLevel.SetInteger("level", (TargetLevel % 1 == 0 ? TargetLevel : 1));
            }
        }
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
