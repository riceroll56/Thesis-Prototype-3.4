using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    public ItemType type;
    public ItemState state;
    public string description;
    public Sprite icon;
    public int quantity;

    public enum ItemType
    {
        Empty,
        Wheat,
        SugarCane,
        YeastSpore,
        Water,
        SaltWater,
        Egg,
        Milk,
        Flour,
        Sugar,
        ActiveYeast,
        MilkFroth,
        CaneSyrup,
        Caramel,
        Salt,
        HardBoiledEggYolk,
        Butter,
        Cream,
        WhippedEggWhite,
        BreadDough,
        CookieDough,
        CakeBatter,
        SimpleDough,
    }

    public enum ItemState
    {
        Empty,
        Collected,
        Boiled,
        Processed,
        Agitated,
        Dough
    }
    public string GetName()
    {
        return type.ToString();
    }

    public string GetDisplayName()
    {
        return System.Text.RegularExpressions.Regex
            .Replace(type.ToString(), "(\\B[A-Z])", " $1");
    }
}