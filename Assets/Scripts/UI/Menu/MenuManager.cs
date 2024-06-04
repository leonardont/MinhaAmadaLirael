using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public AudioClip click;
    public AudioClip back;
    AudioSource audioSource;

    [SerializeField] Slider volumeSlider;
    [SerializeField] TMPro.TMP_Dropdown graphicsDropdown;
    [SerializeField] TMPro.TMP_Dropdown difficultyDropdown;

    public GameObject LiraelAnim;
    public Animator animator;
    public GameObject LoadingScreen;

    void Start()
    {
        Time.timeScale = 1;
        difficultyDropdown.value = PlayerPrefs.GetInt("difficulty");
        LoadData();
        
        audioSource = GetComponent<AudioSource>();
        animator = LiraelAnim.GetComponent<Animator>();
        animator.Play("liraelMenu");

        if (!PlayerPrefs.HasKey("volume"))
        {
            PlayerPrefs.SetFloat("volume", 0.5f);
        }

        if (!PlayerPrefs.HasKey("graphics"))
        {
            PlayerPrefs.SetInt("graphics", 1);
        }

        if (!PlayerPrefs.HasKey("difficulty"))
        {
            PlayerPrefs.SetInt("difficulty", 1);
        }
    }

    /// Carrega opções do jogador
    /// Volume e gráficos
    private void LoadData()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("volume");
        graphicsDropdown.value = PlayerPrefs.GetInt("graphics");
        difficultyDropdown.value = PlayerPrefs.GetInt("difficulty");
    }

    /// Carrega a primeira fase
    public void StartGame()
    {
        StartCoroutine("startGameFadeDelay");
    }

    /// Sai do jogo
    public void QuitGame()
    {
        StartCoroutine("quitGameFadeDelay");
    }

    /// Toca sons de click e back para feedback sonoro da UI
    public void PlayClickSound()
    {
        audioSource.PlayOneShot(click, 0.4f);
    }

    public void PlayBackSound()
    {
        audioSource.PlayOneShot(back, 0.4f);
    }

    /// Define o volume dos sons e músicas do jogo
    public void SetVolume()
    {
        AudioListener.volume = volumeSlider.value;
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
        PlayerPrefs.Save();
    }

    /// Define o nível de qualidade gráfica do jogo
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("graphics", qualityIndex);
        PlayerPrefs.Save();
    }

    /// Define o nível de dificuldade do jogo
    public void SetDifficulty(int diffIndex)
    {
        PlayerPrefs.SetInt("difficulty", diffIndex);
        PlayerPrefs.Save();
    }

    /// Exclui todas as opções do jogador e também seu progresso no jogo
    public void EraseData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

    private IEnumerator startGameFadeDelay()
    {
        yield return new WaitForSeconds(2);

        AsyncOperation operation = SceneManager.LoadSceneAsync(1);

        LoadingScreen.SetActive(true);
    }

    private IEnumerator quitGameFadeDelay()
    {
        yield return new WaitForSeconds(3);
        Application.Quit();
    }
}
