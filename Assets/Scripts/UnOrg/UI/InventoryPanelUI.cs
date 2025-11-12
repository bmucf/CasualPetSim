using UnityEngine;
using TMPro;

public class InventoryPanelUI : MonoBehaviour
{
    public TMP_Text foodText; // premium food count
    public TMP_Text soapText; // premium soap count

    private void OnEnable() { Refresh(); } // auto update when opened

    public void Refresh()
    {
        if (InventoryManager.Instance == null) return;
        int food = InventoryManager.Instance.data.premiumFoodCount;
        int soap = InventoryManager.Instance.data.premiumSoapCount;
        if (foodText) foodText.text = "Premium Food: x" + food;
        if (soapText) soapText.text = "Premium Soap: x" + soap;
    }
}
