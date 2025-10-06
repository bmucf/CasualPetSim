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
    public bool gameIsPaused;
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public bool musicIsMuted;
    public bool sfxIsMuted;
    
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

    private void GoIntoSettings()
    {
        settingsMenu.SetActive(true);

    }

    private void BackOutSettings()
    {
        settingsMenu.SetActive(false);
    }

    private void GoIntoShop()
    {
        shopMenu.SetActive(true);
    }

    private void BackOutShop()
    {
        shopMenu.SetActive(false);
    }

    private void PauseGame()
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

    private void EatMinigame()
    {
        minigameHasStarted = true;
        mainHUD.SetActive(false);
        eatMinigame.SetActive(true);
        
    }

    private void ToggleMusic()
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

    private void ToggleSFX()
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
