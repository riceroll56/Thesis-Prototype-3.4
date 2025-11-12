using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PopupScript : MonoBehaviour
{
    public GameObject popupCanvas;
    public TextMeshProUGUI productText;
    public TextMeshProUGUI qualityText;
    //public Image itemImage;
    public TextMeshProUGUI ingredientListText;
    public TextMeshProUGUI attribute1Text;
    public TextMeshProUGUI attribute2Text;
    public TextMeshProUGUI hintText;

    void Start()
    {
        popupCanvas.SetActive(false);
    }

    public void ShowPopup(Recipe recipe)
    {
        popupCanvas.SetActive(true);

        productText.text = recipe.product;
        qualityText.text = recipe.quality;
        ingredientListText.text = string.Join(", ",
            recipe.IngredientTypes.ConvertAll(type =>
                System.Text.RegularExpressions.Regex.Replace(type.ToString(), "(\\B[A-Z])", " $1")
            )
        );

        attribute1Text.text = string.IsNullOrEmpty(recipe.attribute1) ? "-" : recipe.attribute1;
        attribute2Text.text = string.IsNullOrEmpty(recipe.attribute2) ? "-" : recipe.attribute2;
        hintText.text = recipe.hint;
    }

    public void HidePopup()
    {
        popupCanvas.SetActive(false);
    }
}
