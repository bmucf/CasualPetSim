using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public enum ItemType
    {
        PremiumFood,
        PremiumSoap
    }

    [Header("Config")]
    public ItemType itemType;
    public int amountToGive = 1; // how many per buy

    // called by Buy button
    public void OnBuy()
    {
        if (InventoryManager.Instance == null)
        {
            Debug.LogError("No InventoryManager found.");
            return;
        }

        switch (itemType)
        {
            case ItemType.PremiumFood:
                InventoryManager.Instance.AddPremiumFood(amountToGive);
                break;

            case ItemType.PremiumSoap:
                InventoryManager.Instance.AddPremiumSoap(amountToGive);
                break;
        }

        Debug.Log("Bought " + itemType + " x" + amountToGive);
    }
}
