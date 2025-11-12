using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public enum ItemType { PremiumFood, PremiumSoap }

    [Header("Config")]
    public ItemType itemType;
    public int amountToGive = 1; // per purchase

    // called by BUY button
    public void OnBuy()
    {
        if (InventoryManager.Instance == null) return;

        if (itemType == ItemType.PremiumFood)
            InventoryManager.Instance.AddPremiumFood(amountToGive);
        else
            InventoryManager.Instance.AddPremiumSoap(amountToGive);

        // optional: ping inventory UI if open
        var inv = FindAnyObjectByType<InventoryPanelUI>();
        if (inv != null) inv.Refresh();
    }
}
