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

    public GameObject LoadingScreen;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        /// Verifica se o jogador já alterou o volume
        if(!PlayerPrefs.HasKey("volume"))
        {
            PlayerPrefs.SetFloat("volume", 0.5f);
            LoadData();
        }
        else
        {
            LoadData();
        }

        /// Verifica se o jogador já alterou os gráficos
        if(!PlayerPrefs.HasKey("graphics"))
        {
            PlayerPrefs.SetInt("graphics", 1);
            LoadData();
        }
        else
        {
            LoadData();
        }
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
        SaveData();
    }

    /// Define o nível de qualidade gráfica do jogo
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        SaveData();
    }
    
    /// Carrega opções do jogador
    /// Volume e gráficos
    private void LoadData()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("volume");
        graphicsDropdown.value = PlayerPrefs.GetInt("graphics");
    }

    /// Salva opções do jogador
    private void SaveData()
    {
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
        PlayerPrefs.SetInt("graphics", graphicsDropdown.value);
    }

    /// Exclui todas as opções do jogador e também seu progresso no jogo
    public void EraseData()
    {
        PlayerPrefs.DeleteAll();
    }

    private IEnumerator startGameFadeDelay()
    {
        yield return new WaitForSeconds(2);

        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

        LoadingScreen.SetActive(true);
    }

    private IEnumerator quitGameFadeDelay()
    {
        yield return new WaitForSeconds(3);
        Application.Quit();
    }
}
