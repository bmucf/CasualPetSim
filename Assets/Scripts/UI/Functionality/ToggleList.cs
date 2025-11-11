using UnityEngine;

public class ToggleList : MonoBehaviour
{
    public GameObject [] toggableList;
    public void ToggleItems()
    {
        if (toggableList != null)
        {
            foreach (var item in toggableList)
            {
                item.SetActive(!item.activeSelf);
            }
        }
    }
}