using UnityEngine;

public class RoomUIManager : MonoBehaviour
{
    public bool gameIsPaused = false;
    public bool minigameHasStarted = false;
    public bool musicIsMuted = false;
    public bool sfxIsMuted = false;

    public GameObject mainHUD;
    public GameObject feedingMinigameRoot;
    public GameObject feedingSpawner;
    public GameObject bowlObject;
    public GameObject feedingHUDCanvas;

    public GameObject popupSettings;
    public GameObject popupShop;

    public GameObject mainCamera;
    public GameObject feedingCamera;

    public AudioSource musicSource;
    public AudioSource sfxSource;

    public RoomUI popupController;

    void Start()
    {
        EnterMainRoomMode();
    }

    void EnterMainRoomMode()
    {
        minigameHasStarted = false;
        Time.timeScale = 1f;

        if (mainHUD != null) mainHUD.SetActive(true);

        if (feedingMinigameRoot != null) feedingMinigameRoot.SetActive(false);
        if (feedingSpawner != null) feedingSpawner.SetActive(false);
        if (bowlObject != null) bowlObject.SetActive(false);

        if (mainCamera != null) mainCamera.SetActive(true);
        if (feedingCamera != null) feedingCamera.SetActive(false);

        if (popupController != null)
        {
            popupController.CloseAll();
        }
        else
        {
            if (popupSettings != null) popupSettings.SetActive(false);
            if (popupShop != null) popupShop.SetActive(false);
        }
    }

    public void OpenSettings()
    {
        if (popupController != null && popupSettings != null)
        {
            popupController.Open(popupSettings);
        }
        else if (popupSettings != null)
        {
            popupSettings.SetActive(true);
        }
    }

    public void CloseSettings()
    {
        if (popupController != null)
        {
            popupController.CloseAll();
        }
        else if (popupSettings != null)
        {
            popupSettings.SetActive(false);
        }
    }

    public void OpenShop()
    {
        if (popupController != null && popupShop != null)
        {
            popupController.Open(popupShop);
        }
        else if (popupShop != null)
        {
            popupShop.SetActive(true);
        }
    }

    public void CloseShop()
    {
        if (popupController != null)
        {
            popupController.CloseAll();
        }
        else if (popupShop != null)
        {
            popupShop.SetActive(false);
        }
    }

    public void TogglePause()
    {
        gameIsPaused = !gameIsPaused;
        Time.timeScale = gameIsPaused ? 0f : 1f;
    }

    public void ToggleMusic()
    {
        musicIsMuted = !musicIsMuted;

        if (musicSource != null)
        {
            musicSource.mute = musicIsMuted;
            if (!musicIsMuted && !musicSource.isPlaying)
            {
                musicSource.Play();
            }
        }
    }

    public void ToggleSFX()
    {
        sfxIsMuted = !sfxIsMuted;

        if (sfxSource != null)
        {
            sfxSource.mute = sfxIsMuted;
            if (!sfxIsMuted && !sfxSource.isPlaying)
            {
                sfxSource.Play();
            }
        }
    }

    public void StartFeedingMinigame()
    {
        minigameHasStarted = true;

        if (mainHUD != null) mainHUD.SetActive(false);

        if (feedingMinigameRoot != null) feedingMinigameRoot.SetActive(true);

        if (feedingSpawner != null) feedingSpawner.SetActive(true);

        if (mainCamera != null) mainCamera.SetActive(false);
        if (feedingCamera != null) feedingCamera.SetActive(true);

        if (bowlObject != null) bowlObject.SetActive(true);

        if (feedingHUDCanvas != null) feedingHUDCanvas.SetActive(true);

        if (popupController != null) popupController.CloseAll();
    }

    public void ExitFeedingMinigame()
    {
        minigameHasStarted = false;

        if (mainHUD != null) mainHUD.SetActive(true);

        if (feedingMinigameRoot != null) feedingMinigameRoot.SetActive(false);

        if (feedingSpawner != null) feedingSpawner.SetActive(false);

        if (mainCamera != null) mainCamera.SetActive(true);
        if (feedingCamera != null) feedingCamera.SetActive(false);

        if (bowlObject != null) bowlObject.SetActive(false);

        if (feedingHUDCanvas != null) feedingHUDCanvas.SetActive(false);
    }


}
