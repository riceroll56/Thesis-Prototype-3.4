using System.Collections.Generic;
using UnityEngine;

public class BreadMachine : MonoBehaviour
{
    public GameObject breadMakerUI;
    public InputSlot doughSlot;
    public InputSlot ingredientSlot1;
    public InputSlot ingredientSlot2;
    public Inventory inventory;

    public RecipeDatabase recipeDatabase;
    public PopupScript popupScript;


    void Start()
    {
        breadMakerUI.SetActive(false);
        LoadRecipes();
    }

    private void Update()
    {
        if (!breadMakerUI.activeSelf) return;

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

        // Determine which slot to place the item in
        InputSlot targetSlot = null;

        if (doughSlot.currentItem == null)
        {
            targetSlot = doughSlot;
        }
        else if (ingredientSlot1.currentItem == null)
        {
            targetSlot = ingredientSlot1;
        }
        else if (ingredientSlot2.currentItem == null)
        {
            targetSlot = ingredientSlot2;
        }
        else
        {
            Debug.Log("All bread machine slots are full!");
            return;
        }

        // Create a new item to place on counter
        Item itemToPlace = ScriptableObject.CreateInstance<Item>();
        itemToPlace.type = itemFromInventory.type;
        itemToPlace.quantity = 1;

        targetSlot.SetItem(itemToPlace);
        inventory.RemoveItem(itemFromInventory.type, 1);

        Debug.Log($"Placed {itemToPlace.GetDisplayName()} into bread machine.");
    }

    void LoadRecipes()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("bread_recipes");
        recipeDatabase = JsonUtility.FromJson<RecipeDatabase>(jsonFile.text);
        Debug.Log($"Loaded {recipeDatabase.recipes.Count} recipes from Bread JSON");
    }

    public void OnClickMakeBread()
    {
        List<Item.ItemType> slotItems = new List<Item.ItemType>();
        if (doughSlot.currentItem != null) slotItems.Add(doughSlot.currentItem.type);
        if (ingredientSlot1.currentItem != null) slotItems.Add(ingredientSlot1.currentItem.type);
        if (ingredientSlot2.currentItem != null) slotItems.Add(ingredientSlot2.currentItem.type);

        Debug.Log($"Trying to mix: {string.Join(", ", slotItems)}");

        //bad recipes
        if (slotItems.Contains(Item.ItemType.Water))
        {
            Debug.Log("Created: Soggy Bread");
            Debug.Log("Hint: The dough is drowning.");
            doughSlot.ClearSlot();
            ingredientSlot1.ClearSlot();
            ingredientSlot2.ClearSlot();
            return;
        }

        if (slotItems.Contains(Item.ItemType.Flour))
        {
            Debug.Log("Created: Brick Bread");
            Debug.Log("Hint: The dough is too dense.");
            doughSlot.ClearSlot();
            ingredientSlot1.ClearSlot();
            ingredientSlot2.ClearSlot();
            return;
        }

        if (slotItems.Contains(Item.ItemType.ActiveYeast))
        {
            Debug.Log("Created: Exploding Bread");
            Debug.Log("Hint: The dough already contains yeast.");
            doughSlot.ClearSlot();
            ingredientSlot1.ClearSlot();
            ingredientSlot2.ClearSlot();
            return;
        }

        //normal recipes
        foreach (Recipe recipe in recipeDatabase.recipes)
        {
            List<Item.ItemType> required = recipe.IngredientTypes;

            if (required.Count != slotItems.Count)
                continue;

            List<Item.ItemType> temp = new List<Item.ItemType>(slotItems);
            bool match = true;
            foreach (var r in required)
            {
                if (temp.Contains(r)) temp.Remove(r);
                else { match = false; break; }
            }

            if (match)
            {
                Debug.Log($"Created: {recipe.product} (Quality: {recipe.quality})");
                Debug.Log($"Hint: {recipe.hint}");
                popupScript.ShowPopup(recipe);
                doughSlot.ClearSlot();
                ingredientSlot1.ClearSlot();
                ingredientSlot2.ClearSlot();
                return;
            }
        }

        Debug.Log("No matching recipe found!");
        doughSlot.ClearSlot();
        ingredientSlot1.ClearSlot();
        ingredientSlot2.ClearSlot();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        breadMakerUI.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        breadMakerUI.SetActive(false);
    }

}
