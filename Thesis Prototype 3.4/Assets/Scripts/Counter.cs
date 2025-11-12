using UnityEngine;

public class Counter : MonoBehaviour
{
    public GameObject counterUI;
    public InputSlot counterSlot;
    public Inventory inventory;

    public Item Flour;
    public Item Sugar;
    public Item ActiveYeast;
    public Item MilkFroth;

    public Item WhippedEggWhite;
    public Item Cream;

    void Start()
    {
        counterUI.SetActive(false);
    }


    private void Update()
    {
        if (!counterUI.activeSelf) return;

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
        if (counterSlot.currentItem != null)
        {
            inventory.AddItem(counterSlot.currentItem.type, counterSlot.currentItem.quantity);
            counterSlot.ClearSlot();
        }

        // Create a new item to place on counter
        Item itemToPlace = ScriptableObject.CreateInstance<Item>();
        itemToPlace.type = itemFromInventory.type;
        itemToPlace.quantity = 1;

        counterSlot.SetItem(itemToPlace);
        inventory.RemoveItem(itemFromInventory.type, 1);

        Debug.Log($"Placed {itemToPlace.GetDisplayName()} into counter.");
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        counterUI.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        counterUI.SetActive(false);
    }

    public void OnClickProcessItem()
    {
        if (counterSlot.currentItem == null)
        {
            Debug.Log("No item in counter");
            return;
        }

        Item inputItem = counterSlot.currentItem;
        Item.ItemType outputType = Item.ItemType.Empty;

        switch (inputItem.type)
        {
            case Item.ItemType.Wheat:
                outputType = Item.ItemType.Flour;
                break;
            case Item.ItemType.SugarCane:
                outputType = Item.ItemType.Sugar; 
                break;
            case Item.ItemType.YeastSpore:
                outputType = Item.ItemType.ActiveYeast; 
                break;
            case Item.ItemType.Milk:
                outputType = Item.ItemType.MilkFroth;
                break;
            default:
                Debug.Log("Nothing happens with this item.");
                break;
        }

        if (outputType != Item.ItemType.Empty)
        {
            inventory.AddItem(outputType, 1);
            Debug.Log($"Processed {inputItem.type} into {outputType}.");
        }

        counterSlot.ClearSlot();
    }

    public void OnClickAgigateItem()
    {
        if (counterSlot.currentItem == null)
        {
            Debug.Log("No item in counter");
            return;
        }

        Item inputItem = counterSlot.currentItem;
        Item.ItemType outputType = Item.ItemType.Empty;

        switch (inputItem.type)
        {
            case Item.ItemType.Cream:
                outputType = Item.ItemType.Butter;
                break;
            case Item.ItemType.Egg:
                outputType = Item.ItemType.WhippedEggWhite;
                break;
            case Item.ItemType.Milk:
                outputType = Item.ItemType.Cream;
                break;
            default:
                Debug.Log("Nothing happens with this item.");
                break;
        }

        if (outputType != Item.ItemType.Empty)
        {
            inventory.AddItem(outputType, 1);
            Debug.Log($"Agitated {inputItem.type} into {outputType}.");
        }

        counterSlot.ClearSlot();
    }
}
