using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public GameObject HUD;

    public int coinCount;
    public TextMeshProUGUI coinText;

    public GameObject topHalfKeyHUD;
    public GameObject bottomHalfKeyHUD;
    public GameObject topHalfKey;
    public GameObject bottomHalfKey;

    public bool canMove = true;

    public int lastRoomEntered;

    public GameObject player;
    public Transform door3;
    public Transform door4;
    public Transform door5;

    public bool doorLocked = true;

    public GameObject LoadingScreen;
    public GameObject PauseMenu;
    
    void Start()
    {
        Time.timeScale = 1;
        
        doorLocked = true;
        coinCount = PlayerPrefs.GetInt("CoinCount", 0);

        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            lastRoomEntered = PlayerPrefs.GetInt("lastRoomEntered");

            switch(lastRoomEntered)
            {
                case 3:
                    player.transform.position = door3.position;
                    break;

                case 4:
                    player.transform.position = door4.position;
                    break;

                case 5:
                    player.transform.position = door5.position;
                    break;

                default:
                    break;
            }

            if (PlayerPrefs.GetInt("keyTopHalfCollected") == 1 && PlayerPrefs.GetInt("keyBottomHalfCollected") == 1)
            {
                doorLocked = false;
            }
        }

        if (SceneManager.GetActiveScene().buildIndex == 3 || SceneManager.GetActiveScene().buildIndex == 4)
        {
            if (PlayerPrefs.GetInt("keyTopHalfCollected") == 1 && topHalfKey)
            {
                topHalfKey.SetActive(false);
            }

            if (PlayerPrefs.GetInt("keyBottomHalfCollected") == 1 && bottomHalfKey)
            {
                bottomHalfKey.SetActive(false);
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PauseMenu.activeSelf)
            {
                Time.timeScale = 1;
                PauseMenu.SetActive(false);
            } else {
                PauseMenu.SetActive(true);
                Time.timeScale = 0;
            }
        }

        coinText = GameObject.Find("CoinCount").GetComponent<TextMeshProUGUI>();
        coinText.text = coinCount.ToString();

        if (SceneManager.GetActiveScene().buildIndex != 5 && SceneManager.GetActiveScene().buildIndex != 0 && SceneManager.GetActiveScene().buildIndex != 1)
        {
            if (PlayerPrefs.GetInt("keyTopHalfCollected") == 1)
            {
                topHalfKeyHUD.SetActive(true);
            }

            if (PlayerPrefs.GetInt("keyBottomHalfCollected") == 1)
            {
                bottomHalfKeyHUD.SetActive(true);
            }
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        LoadingScreen.SetActive(true);
        SceneManager.LoadSceneAsync(2);
    }

    public void GoToMenu()
    {
        HUD.SetActive(false);
        LoadingScreen.SetActive(true);
        Time.timeScale = 1;

        SceneManager.LoadSceneAsync(0);
    }
}
