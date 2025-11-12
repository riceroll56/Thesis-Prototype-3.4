using UnityEngine;

public class Boiler : MonoBehaviour
{
    public GameObject boilerUI;
    public InputSlot boilerSlot;
    public Inventory inventory;

    public Item CaneSyrup;
    public Item HardBoiledEgg;
    public Item Salt;
    public Item Caramel;

    void Start()
    {
        boilerUI.SetActive(false);
    }

    private void Update()
    {
        if (!boilerUI.activeSelf) return;

        for (int i = 1; i <= 10; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + (i % 10)))
            {
                PlaceFromInventorySlot(i - 1);
            }
        }
    }

    private void PlaceFromInventorySlot(int slotIndex)
    {
        Item itemFromInventory = inventory.GetItemAt(slotIndex);
        if (itemFromInventory == null || itemFromInventory.quantity <= 0)
        {
            Debug.Log($"Inventory slot {slotIndex + 1} is empty.");
            return;
        }

        // Return existing counter item if any
        if (boilerSlot.currentItem != null)
        {
            inventory.AddItem(boilerSlot.currentItem.type, boilerSlot.currentItem.quantity);
            boilerSlot.ClearSlot();
        }

        // Create a new item to place on counter
        Item itemToPlace = ScriptableObject.CreateInstance<Item>();
        itemToPlace.type = itemFromInventory.type;
        itemToPlace.quantity = 1;

        boilerSlot.SetItem(itemToPlace);
        inventory.RemoveItem(itemFromInventory.type, 1);

        Debug.Log($"Placed {itemToPlace.GetDisplayName()} into boiler.");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        boilerUI.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        boilerUI.SetActive(false);
    }

    public void OnClickBoilItem()
    {
        if (boilerSlot.currentItem == null)
        {
            Debug.Log("No item in boiler");
            return;
        }

        Item inputItem = boilerSlot.currentItem;
        Item.ItemType outputType = Item.ItemType.Empty;

        switch (inputItem.type)
        {
            case Item.ItemType.Water:
                outputType = Item.ItemType.Salt;
                break;
            case Item.ItemType.CaneSyrup:
                outputType = Item.ItemType.Caramel;
                break;
            case Item.ItemType.SugarCane:
                outputType = Item.ItemType.CaneSyrup;
                break;
            case Item.ItemType.Egg:
                outputType = Item.ItemType.HardBoiledEggYolk;
                break;
            default:
                Debug.Log("Nothing happens with this item.");
                break;
        }

        if (outputType != Item.ItemType.Empty)
        {
            inventory.AddItem(outputType, 1);
            Debug.Log($"Boiled {inputItem.type} into {outputType}.");
        }

        // Clear the boiler slot
        boilerSlot.ClearSlot();
    }
}
