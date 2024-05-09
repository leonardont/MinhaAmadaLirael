using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public bool canMove = true;

    public int lastRoomEntered;

    public GameObject player;
    public Transform door3;
    public Transform door4;
    public Transform door5;
    
    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            lastRoomEntered = PlayerPrefs.GetInt("lastRoomEntered");

            switch(lastRoomEntered)
            {
                case 2:
                    player.transform.position = door3.position;
                    break;

                case 3:
                    player.transform.position = door4.position;
                    break;

                case 4:
                    player.transform.position = door5.position;
                    break;

                default:
                    break;
            }
        }
    }
}
