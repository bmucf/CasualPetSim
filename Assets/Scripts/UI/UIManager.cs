using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

// Istvan Wallace

// TODO: Figure out logic to disable MainNav while popups are up and to renable when they arent
public class UIManager : MonoBehaviour
{
    private GameObject lastPopUp;
    // private ToggleList toggles;
    public GameObject[] toggableList;
    private bool isHidden = false;
    //private void Awake()
    //{
    //    toggles = GetComponent<ToggleList>();
    //}
    public void GoToScene(string sceneName)
    {
        if (sceneName != null)
            SceneManager.LoadScene(sceneName);
    }

    public void OpenPopup(GameObject popUp)
    {
        popUp.SetActive(!popUp.activeSelf);

        if (lastPopUp != popUp && lastPopUp != null)
            lastPopUp.SetActive(false);

        lastPopUp = popUp;

        if (popUp.activeSelf == true)
        {
            isHidden = true;
        }
        else if (popUp.activeSelf == false)
        {
            isHidden = false;
        }
        Toggle();
    }
    public void ClosePopup()
    {
        if (lastPopUp != null)
            lastPopUp.SetActive(false);
    }

    public void Toggle()
    {
        if (!isHidden && toggableList != null)
        {
            foreach (var item in toggableList)
            {
                item.SetActive(true);
            }
        }
        else if (isHidden && toggableList != null)
        {
            foreach (var item in toggableList)
            {
                item.SetActive(false);
            }
        }
    }
}
