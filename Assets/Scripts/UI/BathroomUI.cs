using UnityEngine;
using UnityEngine.SceneManagement;

public class BathroomUI : MonoBehaviour
{
    [Header("Popups")]
    public GameObject popupMirror;
    public GameObject popupBathtub;
    public GameObject popupCabinet;
    public GameObject popupCart;
    public GameObject popupSettings;

    public void CloseAll()
    {
        if (popupMirror) popupMirror.SetActive(false);
        if (popupBathtub) popupBathtub.SetActive(false);
        if (popupCabinet) popupCabinet.SetActive(false);
        if (popupCart) popupCart.SetActive(false);
        if (popupSettings) popupSettings.SetActive(false);
    }
    //Button_Mirror
    public void OnMirrorPressed()
    {
        CloseAll();
        if (popupMirror) popupMirror.SetActive(true);
    }
    //Button_Bathtub
    public void OnBathtubPressed()
    {
        CloseAll();
        if (popupBathtub) popupBathtub.SetActive(true);
    }
    //Button_Cabinet
    public void OnCabinetPressed()
    {
        CloseAll();
        if (popupCabinet) popupCabinet.SetActive(true);
    }
    //Button_Cart
    public void OnCartPressed()
    {
        CloseAll(); if (popupCart) popupCart.SetActive(true);
    }
    //Button_Setting
    public void OnSettingsPressed()
    {
        CloseAll(); if (popupSettings) popupSettings.SetActive(true);
    }
    //Button_Close
    public void OnClosePressed()
    {
        CloseAll();
    }

    

    //Home
    public void OnHomePressed()
    {
        SceneManager.LoadScene("LRWhitebox");
    }
}
