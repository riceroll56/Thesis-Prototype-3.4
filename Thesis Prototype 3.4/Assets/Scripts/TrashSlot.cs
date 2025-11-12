using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class TrashSlot : MonoBehaviour, IDropHandler
{
    public Item currentItem;
    public TextMeshProUGUI itemText;
    //public Inventory inventory;

    public void SetItem(Item item)
    {
        currentItem = item;
        if (item == null || item.type == Item.ItemType.Empty)
        {
            itemText.text = ""; // empty slot
        }
        //else
        //{
        //    itemText.text = $"{item.GetDisplayName()} x{item.quantity}";
        //}
    }

    public void ClearSlot()
    {
        currentItem = null;
        itemText.text = "";
    }

    public void OnDrop(PointerEventData eventData)
    {
        InventorySlot draggedSlot = eventData.pointerDrag?.GetComponent<InventorySlot>();

        //if (currentItem != null)
        //{
        //    Debug.Log($"Returning {currentItem.GetDisplayName()} to inventory before replacing it.");
        //    inventory.AddItem(currentItem.type, 1);
        //}

        currentItem = ScriptableObject.CreateInstance<Item>();
        currentItem.type = draggedSlot.storedItem.type;
        currentItem.quantity = 1;

        //itemText.text = $"{currentItem.GetDisplayName()}";
        Debug.Log($"Dropped {currentItem.type} into input slot!");

        // Decrease the dragged slot’s quantity
        draggedSlot.storedItem.quantity -= 1;

        // If no more items left, clear the inventory slot
        if (draggedSlot.storedItem.quantity <= 0)
        {
            draggedSlot.SetItem(null);
        }
        else
        {
            draggedSlot.SetItem(draggedSlot.storedItem); // update UI text
        }
    }

}