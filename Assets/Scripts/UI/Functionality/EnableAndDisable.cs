using UnityEngine;

public class EnableAndDisable : MonoBehaviour
{
    public GameObject [] disableList;
    public GameObject[] enableList;

    public void DisableAll()
    {
        if (disableList != null)
        {
            foreach (var item in disableList)
            {
                item.SetActive(false);
            }
        }
    }
    public void EnableAll()
    {
        if (enableList != null)
        {
            foreach (var item in enableList)
            {
                item.SetActive(true);
            }
        }
    }
}
