using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShoppingCartManager : MonoBehaviour
{
    public GrabManagerLeft leftHandGrab;
    public GrabManagerRight rightHandGrab;
    public TextMeshProUGUI shoppingCartTextLeft;
    public TextMeshProUGUI shoppingCartTextRight;
    // Start is called before the first frame update
    void Start()
    {
        AutoSingleton.items = new List<string>();
        AutoSingleton.itemsText = "Cart:\n\n";
    }

    public void addItem()
    {

        string leftItemName = leftHandGrab.objectName;
        string rightItemName = rightHandGrab.objectName;
        if (!leftItemName.Equals(""))
        {

            if (!AutoSingleton.items.Contains(leftItemName)) 
            {
                AutoSingleton.items.Add(leftItemName);
                AutoSingleton.itemsText += " " + leftItemName; 
                //shoppingCartText.text += " " + leftItemName;
            }
        }
        else if (!rightItemName.Equals(""))
        {
            if (!AutoSingleton.items.Contains(rightItemName))
            {
                AutoSingleton.items.Add(rightItemName);
                AutoSingleton.itemsText += " " + rightItemName;
                //shoppingCartText.text += " " + rightItemName;


            }

        }
        shoppingCartTextLeft.text = AutoSingleton.itemsText;
        shoppingCartTextRight.text = AutoSingleton.itemsText;


    }
   
}
