using UnityEngine;

public class Trash : MonoBehaviour
{

    public GameObject TrashUI;

    public Inventory inventory;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TrashUI.SetActive(false);
    }

    private void Update()
    {
        if (!TrashUI.activeSelf) return;

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

        // Create a new item to place on counter
        Item itemToPlace = ScriptableObject.CreateInstance<Item>();
        itemToPlace.type = itemFromInventory.type;
        itemToPlace.quantity = 1;

        inventory.RemoveItem(itemFromInventory.type, 1);

        Debug.Log($"Placed {itemToPlace.GetDisplayName()} into trash.");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        TrashUI.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        TrashUI.SetActive(false);
    }
}
