using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour
{
    public Button rightHub;
    public Button feedButton;
    public Button cleanButton;
    public Button lullButton;

    public Vector3 rightOrigin;
    public float offset = 

    private void Awake()
    {
        Debug.Log("Position")
        rightOrigin = rightHub.transform.position;
        feedButton.transform.position = rightOrigin;
        cleanButton.transform.position = rightOrigin;
        lullButton.transform.position = rightOrigin;

    }

    public void CheckPress()
    {
        GameObject pressed = EventSystem.current.currentSelectedGameObject;
        if (pressed != null)
        {
            Debug.Log($"{pressed.name} was pushed.");
        }
        else
        {
            Debug.Log("No button detected.");
        }
    }

    public void ExpandPaw()
    {
        feedButton.transform.position = feedButton.transform.position + (transform.right * offset)

    }
}
