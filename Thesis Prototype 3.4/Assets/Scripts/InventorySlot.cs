using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public TextMeshProUGUI itemText;
    public Item storedItem;

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
        storedItem = item;
        if (item == null || item.type == Item.ItemType.Empty)
        {
            itemText.text = ""; // empty slot
        }
        else
        {
            itemText.text = $"{item.GetDisplayName()} x{item.quantity}";
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag");
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        //image.raycastTarget = false;
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
        transform.SetParent(parentAfterDrag);
        //image.raycastTarget = true;
        canvasGroup.blocksRaycasts = true;
    }
}
