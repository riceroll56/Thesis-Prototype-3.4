using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerGather : MonoBehaviour
{
    public GameObject cow;
    public GameObject chicken;
    public GameObject wheat;
    public GameObject sugarcane;
    public GameObject ocean;
    public GameObject river;

    private Inventory playerInventory;
    private PlayerTerrainDetector playerTerrainDetector;

    private bool nearCow = false;
    private bool nearChicken = false;
    private bool nearWheat = false;
    private bool nearSugarcane = false;

    public int pickupAmount = 1;


    private void Start()
    {
        playerInventory = GetComponent<Inventory>();
        playerTerrainDetector = GetComponent<PlayerTerrainDetector>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (playerTerrainDetector.currentTerrain == "River")
            {
                CollectFreshwater();
            }

            else if (playerTerrainDetector.currentTerrain == "Ocean")
            {
                CollectSaltwater();
            }

            if (nearCow)
                CollectFromCow();
            if (nearChicken)
                CollectFromChicken();
            if (nearWheat)
                CollectFromWheat();
            if (nearSugarcane)
                CollectFromSugarcane();
        }
    }
    private void GiveItem(Item.ItemType type)
    {
        if (playerInventory != null)
        {
            playerInventory.AddItem(type, pickupAmount);
            Debug.Log($"Gave {pickupAmount}x {type} to player.");
        }
    }

    private void CollectFreshwater()
    {
        Debug.Log("Collected Freshwater");
        GiveItem(Item.ItemType.Water);
    }

    private void CollectSaltwater()
    {
        Debug.Log("Collected Saltwater");
        GiveItem(Item.ItemType.SaltWater);
    }

    private void CollectFromCow()
    {
        Debug.Log("Collected Milk from Cow");
        GiveItem(Item.ItemType.Milk);
    }

    private void CollectFromChicken()
    {
        Debug.Log("Collected Egg from Chicken");
        GiveItem(Item.ItemType.Egg);
    }

    private void CollectFromWheat()
    {
        Debug.Log("Collected Wheat");
        GiveItem(Item.ItemType.Wheat);
    }

    private void CollectFromSugarcane()
    {
        Debug.Log("Colelcted Sugarcane");
        GiveItem(Item.ItemType.SugarCane);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cows"))
        {
            nearCow = true;
            Debug.Log("Near cow");
        }
        else if (collision.CompareTag("Chickens"))
        {
            nearChicken = true;
            Debug.Log("Near chicken");
        }
        else if (collision.CompareTag("Wheat"))
        {
            nearWheat = true;
            Debug.Log("Near wheat");
        }
        else if (collision.CompareTag("Sugarcane"))
        {
            nearSugarcane = true;
            Debug.Log("Near sugarcane");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Cows"))
        {
            nearCow = false;
        }
        else if (collision.CompareTag("Chickens"))
        {
            nearChicken = false;
        }
        else if (collision.CompareTag("Wheat"))
        {
            nearWheat = false;
        }
        else if (collision.CompareTag("Sugarcane"))
        {
            nearSugarcane = false;
        }
    }
}
