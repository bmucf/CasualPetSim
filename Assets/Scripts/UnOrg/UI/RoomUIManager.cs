using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomUIManager : MonoBehaviour
{
    private SessionContent sessionContent;

    [Header("Main UI Groups")]
    public GameObject mainHUD;              // normal HUD
    public GameObject feedingMinigameRoot;  // minigame world objs
    public GameObject feedingHUDCanvas;     // minigame HUD

    [Header("Popups")]
    public GameObject shopPopup;
    public GameObject settingsPopup;
    public GameObject inventoryPopup;       // NEW: inventory panel (Popup_Inventory)


    void Start()
    {
        // default state
        if (mainHUD != null) mainHUD.SetActive(true);
        if (feedingMinigameRoot != null) feedingMinigameRoot.SetActive(false);
        if (feedingHUDCanvas != null) feedingHUDCanvas.SetActive(false);

        if (shopPopup != null) shopPopup.SetActive(false);
        if (settingsPopup != null) settingsPopup.SetActive(false);
        if (inventoryPopup != null) inventoryPopup.SetActive(false);

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
    }

    // close everything popup-like
    public void CloseAllPopups()
    {
        if (shopPopup != null) shopPopup.SetActive(false);
        if (settingsPopup != null) settingsPopup.SetActive(false);
        if (inventoryPopup != null) inventoryPopup.SetActive(false);
    }

    // ---------- MINIGAME CONTROL ----------

    public void GoToFoodMiniGame()
    {
        // hide UI popups
        CloseAllPopups();
        


        // enable minigame world + HUD
        if (feedingMinigameRoot != null) feedingMinigameRoot.SetActive(true);
        if (feedingHUDCanvas != null) feedingHUDCanvas.SetActive(true);

    }
   
    public void GoToBathScene(string sceneName)
    {
        Time.timeScale = 1;

        CloseAllPopups();
        if (mainHUD != null) mainHUD.SetActive(false);

        SceneManager.LoadScene(sceneName);
    }

}
