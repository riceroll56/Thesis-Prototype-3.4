using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Recipe
{
    public List<string> ingredients; // list of ingredient names

    public string product;       
    public string quality;
    public string attribute1;
    public string attribute2;
    public string hint;
    public List<Item.ItemType> IngredientTypes
    {
        get
        {
            List<Item.ItemType> types = new List<Item.ItemType>();
            foreach (var s in ingredients)
                types.Add((Item.ItemType)System.Enum.Parse(typeof(Item.ItemType), s));
            return types;
        }
    }
}
