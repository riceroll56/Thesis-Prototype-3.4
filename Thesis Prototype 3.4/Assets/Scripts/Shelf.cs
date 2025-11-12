using System;
using UnityEngine;

public class Shelf : MonoBehaviour
{
    public int pickupAmount = 1;
    public GameObject shelfUI;

    private Inventory playerInventory;

    private void Start()
    {
        shelfUI.SetActive(false);
    }

    public void Update()
    {
        if (shelfUI.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                GiveItem(Item.ItemType.Wheat);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                GiveItem(Item.ItemType.SugarCane);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                GiveItem(Item.ItemType.YeastSpore);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                GiveItem(Item.ItemType.Milk);
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                GiveItem(Item.ItemType.Egg);
            }
            if(Input.GetKeyDown(KeyCode.Alpha6))
            {
                GiveItem(Item.ItemType.Water);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        playerInventory = other.GetComponent<Inventory>();
        //Debug.Log("Touching Shelf");
        shelfUI.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Inventory>() == playerInventory)
        {
            playerInventory = null;
            //Debug.Log("Leaving Shelf");
            shelfUI.SetActive(false);
        }
    }

    private void GiveItem(Item.ItemType type)
    {
        playerInventory.AddItem(type, pickupAmount);
        Debug.Log($"Gave {pickupAmount}x {type} to player.");
    }

    public void OnClickGiveItem(string itemTypeName)
    {
        if (playerInventory != null)
        {
            Item.ItemType type = (Item.ItemType)System.Enum.Parse(typeof(Item.ItemType), itemTypeName);
            playerInventory.AddItem(type, pickupAmount);
            Debug.Log($"Gave {pickupAmount}x {type} to player.");
        }

    }
}
