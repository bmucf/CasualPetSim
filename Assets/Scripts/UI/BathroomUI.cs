using UnityEngine;
using UnityEngine.SceneManagement; 
public class BathroomUI : MonoBehaviour
{
    [Header("Optional Panels")]
    public GameObject bathPanel;
    public GameObject mirrorPanel;
    public GameObject cabinetPanel;

    public void OnBathtubPressed()
    {
        Debug.Log("Bathtub pressed �� open bath UI");
        if (bathPanel) bathPanel.SetActive(true);
    }

    public void OnMirrorPressed()
    {
        Debug.Log("Mirror pressed �� open grooming UI");
        if (mirrorPanel) mirrorPanel.SetActive(true);
    }

    public void OnCabinetPressed()
    {
        Debug.Log("Cabinet pressed �� open cabinet UI");
        if (cabinetPanel) cabinetPanel.SetActive(true);
    }

    
    public void OnHomePressed()
    {
        Debug.Log("Home button pressed �� returning to main scene");
        SceneManager.LoadScene("LRWhitebox");
        
    }
}
