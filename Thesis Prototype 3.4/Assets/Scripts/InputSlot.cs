using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class InputSlot : MonoBehaviour, IDropHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Item currentItem;
    public TextMeshProUGUI itemText;
    public Inventory inventory;

    //public Image image;
    private CanvasGroup canvasGroup;
    [HideInInspector] public Transform parentAfterDrag;

    private Transform originalParent;
    private Canvas canvas;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();

        if (itemText == null)
            itemText = GetComponent<TextMeshProUGUI>();
    }

    public void SetItem(Item item)
    {
        currentItem = item;
        if (item == null || item.type == Item.ItemType.Empty)
        {
            itemText.text = ""; // empty slot
        }
        else
        {
            itemText.text = $"{item.GetDisplayName()} x{item.quantity}";
        }
    }

    public void ClearSlot()
    {
        currentItem = null;
        itemText.text = "";
    }

    public void OnDrop(PointerEventData eventData)
    {
        InventorySlot draggedSlot = eventData.pointerDrag?.GetComponent<InventorySlot>();

        if (currentItem != null)
        {
            Debug.Log($"Returning {currentItem.GetDisplayName()} to inventory before replacing it.");
            inventory.AddItem(currentItem.type, 1);
        }

        currentItem = ScriptableObject.CreateInstance<Item>();
        currentItem.type = draggedSlot.storedItem.type;
        currentItem.quantity = 1;

        itemText.text = $"{currentItem.GetDisplayName()} x{currentItem.quantity}";
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

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag");

        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        canvasGroup.blocksRaycasts = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
        transform.position = Input.mousePosition;

    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
        if (eventData.pointerEnter == null || eventData.pointerEnter.GetComponent<InputSlot>() == null)
        {
            Debug.Log($"Returning {currentItem.GetDisplayName()} to inventory.");
            inventory.AddItem(currentItem.type, 1);
            ClearSlot();

            transform.SetParent(parentAfterDrag);
            canvasGroup.blocksRaycasts = true;
        }
    }

}