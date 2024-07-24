using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialScript : MonoBehaviour
{
    public GameObject WalkingText;
    public GameObject JumpingText;
    public GameObject ShootingText;
    public GameObject WASD;
    public GameObject Arrows;
    public GameObject Spacebar;
    public GameObject CTRL;
    public GameObject Click;
    public GameObject Panel;

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            StartCoroutine(movementTutorial());
        } else if (SceneManager.GetActiveScene().buildIndex == 5)
        {
            StartCoroutine(shootingTutorial());
        }
    }

    private IEnumerator movementTutorial()
    {
        yield return new WaitForSeconds(1f);

        Panel.SetActive(true);
        WalkingText.SetActive(true);
        WASD.SetActive(true);
        Arrows.SetActive(true);

        yield return new WaitForSeconds(8f);

        WalkingText.SetActive(false);
        WASD.SetActive(false);
        Arrows.SetActive(false);

        JumpingText.SetActive(true);
        Spacebar.SetActive(true);

        yield return new WaitForSeconds(8f);

        Panel.SetActive(false);
        JumpingText.SetActive(false);
        Spacebar.SetActive(false);
    }

    private IEnumerator shootingTutorial()
    {
        yield return new WaitForSeconds(1f);

        Panel.SetActive(true);
        ShootingText.SetActive(true);
        CTRL.SetActive(true);
        Click.SetActive(true);

        yield return new WaitForSeconds(6f);

        Panel.SetActive(false);
        ShootingText.SetActive(false);
        CTRL.SetActive(false);
        Click.SetActive(false);
    }
}
