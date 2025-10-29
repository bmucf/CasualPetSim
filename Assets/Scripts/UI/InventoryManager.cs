using UnityEngine;

[System.Serializable]
public class InventoryData
{
    public int premiumFoodCount;
    public int premiumSoapCount;
}

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public InventoryData data = new InventoryData();

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void AddPremiumFood(int amount = 1)
    {
        data.premiumFoodCount += amount;
        Debug.Log("[Inventory] Premium Food + " + amount +
                  " => now " + data.premiumFoodCount);
    }

    public void AddPremiumSoap(int amount = 1)
    {
        data.premiumSoapCount += amount;
        Debug.Log("[Inventory] Premium Soap + " + amount +
                  " => now " + data.premiumSoapCount);
    }

    public bool UsePremiumFood(int amount = 1)
    {
        if (data.premiumFoodCount >= amount)
        {
            data.premiumFoodCount -= amount;
            Debug.Log("[Inventory] Used Premium Food x" + amount +
                      " => now " + data.premiumFoodCount);
            return true;
        }
        else
        {
            Debug.Log("[Inventory] Tried to use Premium Food but none left.");
            return false;
        }
    }

    public bool UsePremiumSoap(int amount = 1)
    {
        if (data.premiumSoapCount >= amount)
        {
            data.premiumSoapCount -= amount;
            Debug.Log("[Inventory] Used Premium Soap x" + amount +
                      " => now " + data.premiumSoapCount);
            return true;
        }
        else
        {
            Debug.Log("[Inventory] Tried to use Premium Soap but none left.");
            return false;
        }
    }
}
