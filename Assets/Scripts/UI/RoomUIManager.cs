using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomUIManager : MonoBehaviour
{
    [Header("Main UI Groups")]
    public GameObject mainHUD;              // normal HUD
    public GameObject feedingMinigameRoot;  // minigame world objs
    public GameObject feedingHUDCanvas;     // minigame HUD

    [Header("Popups")]
    public GameObject shopPopup;
    public GameObject settingsPopup;
    public GameObject inventoryPopup;       // NEW: inventory panel (Popup_Inventory)

    [Header("Minigame Components")]
    public GameObject bowlObject;
    public GameObject mainCamera;
    public GameObject feedingCamera;
    public SpawningFood feedingSpawner;

    [HideInInspector] public bool minigameHasStarted = false;

    void Start()
    {
        // default state
        if (mainHUD != null) mainHUD.SetActive(true);
        if (feedingMinigameRoot != null) feedingMinigameRoot.SetActive(false);
        if (feedingHUDCanvas != null) feedingHUDCanvas.SetActive(false);

        if (shopPopup != null) shopPopup.SetActive(false);
        if (settingsPopup != null) settingsPopup.SetActive(false);
        if (inventoryPopup != null) inventoryPopup.SetActive(false);

        if (mainCamera != null) mainCamera.SetActive(true);
        if (feedingCamera != null) feedingCamera.SetActive(false);
        if (bowlObject != null) bowlObject.SetActive(false);
    }

    // ---------- OPEN / CLOSE POPUPS ----------

    public void OpenShop()
    {
        CloseAllPopups();
        if (shopPopup != null) shopPopup.SetActive(true);
        if (mainHUD != null) mainHUD.SetActive(false);
    }

    public void CloseShop()
    {
        if (shopPopup != null) shopPopup.SetActive(false);
        if (!minigameHasStarted && mainHUD != null) mainHUD.SetActive(true);
    }

    public void OpenSettings()
    {
        CloseAllPopups();
        if (settingsPopup != null) settingsPopup.SetActive(true);
        if (mainHUD != null) mainHUD.SetActive(false);
    }

    public void CloseSettings()
    {
        if (settingsPopup != null) settingsPopup.SetActive(false);
        if (!minigameHasStarted && mainHUD != null) mainHUD.SetActive(true);
    }

    // NEW: Inventory popup
    public void OpenInventory()
    {
        CloseAllPopups();
        if (inventoryPopup != null)
        {
            // show inventory
            inventoryPopup.SetActive(true);

            // update counts in UI
            var invUI = inventoryPopup.GetComponent<InventoryPanelUI>();
            if (invUI != null)
            {
                invUI.Refresh();
            }
        }

        if (mainHUD != null) mainHUD.SetActive(false);
    }

    public void CloseInventory()
    {
        if (inventoryPopup != null) inventoryPopup.SetActive(false);
        if (!minigameHasStarted && mainHUD != null) mainHUD.SetActive(true);
    }

    // close everything popup-like
    public void CloseAllPopups()
    {
        if (shopPopup != null) shopPopup.SetActive(false);
        if (settingsPopup != null) settingsPopup.SetActive(false);
        if (inventoryPopup != null) inventoryPopup.SetActive(false);
    }

    // ---------- MINIGAME CONTROL ----------

    public void StartFeedingMinigame()
    {
        minigameHasStarted = true;

        // hide UI popups
        CloseAllPopups();

        // hide normal HUD
        if (mainHUD != null) mainHUD.SetActive(false);

        // enable minigame world + HUD
        if (feedingMinigameRoot != null) feedingMinigameRoot.SetActive(true);
        if (feedingHUDCanvas != null) feedingHUDCanvas.SetActive(true);

        // camera swap
        if (mainCamera != null) mainCamera.SetActive(false);
        if (feedingCamera != null) feedingCamera.SetActive(true);

        // bowl + spawner ON
        if (bowlObject != null) bowlObject.SetActive(true);
        // feedingSpawner should already be in scene and start doing its job
    }

    public void ExitFeedingMinigame()
    {
        minigameHasStarted = false;

        // turn off minigame world + HUD
        if (feedingMinigameRoot != null) feedingMinigameRoot.SetActive(false);
        if (feedingHUDCanvas != null) feedingHUDCanvas.SetActive(false);

        // cameras back
        if (mainCamera != null) mainCamera.SetActive(true);
        if (feedingCamera != null) feedingCamera.SetActive(false);

        // bowl OFF
        if (bowlObject != null) bowlObject.SetActive(false);

        // popups stay closed
        CloseAllPopups();

        // normal HUD back
        if (mainHUD != null) mainHUD.SetActive(true);
    }

   
    public void GoToBathScene(string sceneName)
    {
        Time.timeScale = 1;

        CloseAllPopups();
        if (mainHUD != null) mainHUD.SetActive(false);

        SceneManager.LoadScene(sceneName);
    }

}
