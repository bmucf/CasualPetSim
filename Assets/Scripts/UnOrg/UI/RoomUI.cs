using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoomUI : MonoBehaviour
{
    public List<GameObject> panels = new List<GameObject>();

    public Image barFullness;
    public Image barCleanliness;
    public Image barEnergy;
    public Image barMood;

    void Awake()
    {
        CloseAll();
    }

    // open 1 panel, close others
    public void Open(GameObject panel)
    {
        CloseAll();

        if (panel)
        {
            panel.SetActive(true);

            // try refresh inventory numbers if this panel is inventory
            var inv = panel.GetComponent<InventoryPanelUI>();
            if (inv != null)
            {
                inv.Refresh();
            }
        }
    }

    // close all panels
    public void CloseAll()
    {
        if (panels != null)
        {
            foreach (var p in panels)
            {
                if (p) p.SetActive(false);
            }
        }
    }

    // load scene
    public void LoadScene(string sceneName)
    {
        CloseAll();
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("RoomUI.LoadScene: sceneName is null");
        }
    }

    // stat bars
    public void SetFullness(float v) => SetBar(barFullness, v);
    public void SetCleanliness(float v) => SetBar(barCleanliness, v);
    public void SetEnergy(float v) => SetBar(barEnergy, v);
    public void SetMood(float v) => SetBar(barMood, v);

    void SetBar(Image bar, float v)
    {
        if (!bar) return;
        bar.type = Image.Type.Filled;
        bar.fillMethod = Image.FillMethod.Horizontal;
        bar.fillOrigin = 0;
        bar.fillAmount = Mathf.Clamp01(v);
    }
}
