using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UI : MonoBehaviour
{
    public bool minigameHasStarted;
    public GameObject mainHUD;
    public GameObject settingsMenu;
    public GameObject shopMenu;
    public GameObject eatMinigame;
    public GameObject bathMinigame;
    public GameObject bathRocko;
    public bool gameIsPaused;
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public bool musicIsMuted;
    public bool sfxIsMuted;
    public GameObject mainCamera;
    public GameObject foodCamera;
    public GameObject bowl;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (minigameHasStarted)
        {
            
        }
    }

    public void GoIntoSettings()
    {
        settingsMenu.SetActive(true);

    }

    public void BackOutSettings()
    {
        settingsMenu.SetActive(false);
    }

    public void GoIntoShop()
    {
        shopMenu.SetActive(true);
    }

    public void BackOutShop()
    {
        shopMenu.SetActive(false);
    }

    public void PauseGame()
    {
        gameIsPaused = !gameIsPaused;
        
        if (gameIsPaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void EatMinigame()
    {
        minigameHasStarted = true;
        mainHUD.SetActive(false);
        eatMinigame.SetActive(true);
        mainCamera.SetActive(false);
        foodCamera.SetActive(true);
        bowl.SetActive(true);
    }

    public void BathMinigame()
    {
        minigameHasStarted = true;
        mainHUD.SetActive(false);
        bathRocko.SetActive(true);
    }

    public void ToggleMusic()
    {
        musicIsMuted = !musicIsMuted;

        if (musicIsMuted)
        {
            musicSource.mute = musicIsMuted;
        }
        else
        {
            musicSource.Play();
        }
    }

    public void ToggleSFX()
    {
        sfxIsMuted = !sfxIsMuted;

        if (musicIsMuted)
        {
            sfxSource.mute = sfxIsMuted;
        }
        else
        {
            sfxSource.Play();
        }
    }

}
