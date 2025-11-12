using UnityEngine;

public class StandMixer : MonoBehaviour
{
    public GameObject standMixerUI;
    public Inventory inventory;

    public GameObject breadMaker;
    public GameObject pastryMaker;
    public GameObject cookieMaker;
    public GameObject cakeMaker;

    public GameObject breadText;
    public GameObject cakeText;
    public GameObject cookieText;
    public GameObject pastryText;

    public Item BreadDough;
    public Item CookieDough;
    public Item CakeBatter;
    public Item PastryDough;

    void Start()
    {
        standMixerUI.SetActive(false);
        breadMaker.SetActive(false);
        pastryMaker.SetActive(false);
        cookieMaker.SetActive(false);
        cakeMaker.SetActive(false);

        breadText.SetActive(false);
        cookieText.SetActive(false);
        pastryText.SetActive(false);
        cakeText.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        standMixerUI.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        standMixerUI.SetActive(false);
    }

    public void MakeBreadDough()
    {
        Item.ItemType[] required = new Item.ItemType[]
        {
            Item.ItemType.Flour,
            Item.ItemType.Water,
            Item.ItemType.ActiveYeast,
            Item.ItemType.Salt
        };

        foreach (var ingredient in required)
        {
            if (!inventory.HasItem(ingredient, 1))
            {
                Debug.Log($"Missing ingredient: {ingredient}");
                return; // stop if any ingredient is missing
            }
        }

        foreach (var ingredient in required)
        {
            inventory.RemoveItem(ingredient, 1);
        }

        inventory.AddItem(BreadDough.type, 1);
        Debug.Log("Made Bread Dough");
        breadMaker.SetActive(true);
        breadText.SetActive(true);
    }

    public void MakeCookieDough()
    {
        Item.ItemType[] required = new Item.ItemType[]
        {
            Item.ItemType.Flour,
            Item.ItemType.Sugar,
            Item.ItemType.Butter,
            Item.ItemType.Egg
        };

        foreach (var ingredient in required)
        {
            if (!inventory.HasItem(ingredient, 1))
            {
                Debug.Log($"Missing ingredient: {ingredient}");
                return;
            }
        }

        foreach (var ingredient in required)
        {
            inventory.RemoveItem(ingredient, 1);
        }

        inventory.AddItem(CookieDough.type, 1);
        Debug.Log("Made Cookie Dough");
        cookieMaker.SetActive(true);
        cookieText.SetActive(true);
    }

    public void MakeCakeBatter()
    {
        Item.ItemType[] required = new Item.ItemType[]
        {
            Item.ItemType.Flour,
            Item.ItemType.Sugar,
            Item.ItemType.Butter,
            Item.ItemType.Egg,
            Item.ItemType.Milk
        };

        foreach (var ingredient in required)
        {
            if (!inventory.HasItem(ingredient,1))
            {
                Debug.Log($"Missing ingredient: {ingredient}");
                return;
            }
        }

        foreach (var ingredient in required)
        {
            inventory.RemoveItem(ingredient, 1);
        }

        inventory.AddItem(CakeBatter.type, 1);
        Debug.Log("Made Cake Batter");
        cakeMaker.SetActive(true);
        cakeText.SetActive(true);
    }

    public void MakePastryDough()
    {
        Item.ItemType[] required = new Item.ItemType[]
        {
            Item.ItemType.Flour,
            Item.ItemType.Water,
            Item.ItemType.Butter,
            Item.ItemType.Salt,
        };

        foreach (var ingredient in required)
        {
            if (!inventory.HasItem(ingredient, 1))
            {
                Debug.Log($"Missing Ingredient: {ingredient}");
                return;
            }
        }

        foreach (var ingredient in required)
        {
            inventory.RemoveItem(ingredient, 1);
        }

        inventory.AddItem(PastryDough.type, 1);
        Debug.Log("Made Pastry Dough");
        pastryMaker.SetActive(true);
        pastryText.SetActive(true);
    }
}
