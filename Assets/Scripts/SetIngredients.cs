using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetIngredients : MonoBehaviour
{   
    private void Awake()
    {
        GetComponent<TextMeshProUGUI>().text = "Ingredients:\n";
        string dishName = AutoSingleton.dishName;
        if (dishName == "FruitSalad")
        {
            GetComponent<TextMeshProUGUI>().text += "- Watermelon\n- Bananas\n- Pineapple\n- Condensed Milk\n- Strawberries";
        }
        else if (dishName == "AjiDeGallina")
        {
            GetComponent<TextMeshProUGUI>().text += "- Yellow chili pepper\n- Chicken breast\n- Rice\n- Potatoes\n- Eggs";
        }
        else if (dishName == "Estofado")
        {
            GetComponent<TextMeshProUGUI>().text += "- Beef\n- Red onion\n- Tomato\n- Carrot\n- Potatoes";
        }
        
    }
}
