using UnityEngine;
using TMPro;

public class InventoryPanelUI : MonoBehaviour
{
    public TMP_Text foodText; // shows premium food count
    public TMP_Text soapText; // shows premium soap count

    private void OnEnable()
    {
        Refresh();
    }

    public void Refresh()
    {
        if (InventoryManager.Instance == null) return;

        int food = InventoryManager.Instance.data.premiumFoodCount;
        int soap = InventoryManager.Instance.data.premiumSoapCount;

        foodText.text = "x" + food;
        soapText.text = "x" + soap;
    }
}
