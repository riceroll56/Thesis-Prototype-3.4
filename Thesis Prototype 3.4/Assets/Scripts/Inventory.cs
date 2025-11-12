using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    public int inventorySize = 10;
    public Item[] items; // Player inventory

    public InventorySlot[] slotUIs;

    private void Awake()
    {
        items = new Item[inventorySize];

        for (int i = 0; i < inventorySize; i++)
        {
            items[i] = ScriptableObject.CreateInstance<Item>();
            items[i].type = Item.ItemType.Empty;
            items[i].quantity = 0;
        }
    }

    // Add item by type
    public bool AddItem(Item.ItemType type, int amount = 1)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].type == type && items[i].quantity > 0)
            {
                items[i].quantity += amount;
                UpdateUI();
                return true;
            }
        }

        // Find first empty slot
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].type == Item.ItemType.Empty || items[i].quantity <= 0)
            {
                items[i].type = type;
                items[i].quantity = amount;
                UpdateUI();
                return true;
            }
        }

        Debug.Log("Inventory full!");
        return false;
    }

    public void UpdateUI()
    {
        string slotSummary = "";
        for (int i = 0; i < slotUIs.Length; i++)
        {

            slotSummary += $"[{i + 1}: {items[i].type} x{items[i].quantity}] ";

            if (i < items.Length)
            {
                // Automatically clear out items with 0 quantity
                if (items[i].quantity <= 0 || items[i].type == Item.ItemType.Empty)
                {
                    items[i].type = Item.ItemType.Empty;
                    items[i].quantity = 0;
                    slotUIs[i].SetItem(null);
                }
                else
                {
                    slotUIs[i].SetItem(items[i]);
                }
            }
        }

        Debug.Log($"Inventory: {slotSummary}");

    }

    // Remove item
    public bool RemoveItem(Item.ItemType type, int amount = 1)
    {
        foreach (var item in items)
        {
            if (item.type == type && item.quantity >= amount)
            {
                item.quantity -= amount;
                if (item.quantity <= 0)
                {
                    item.quantity = 0;
                    item.type = Item.ItemType.Empty;
                }

                UpdateUI();
                return true;
            }
        }

        Debug.Log("Item not found or insufficient quantity!");
        return false;
    }

    public bool HasItem(Item.ItemType type, int amount = 1)
    {
        foreach (var item in items)
        {
            if (item.type == type && item.quantity >= amount)
                return true;
        }
        return false;
    }

    public Item GetItemAt(int index)
    {
        if (index < 0 || index >= items.Length)
            return null;

        return items[index];
    }
}