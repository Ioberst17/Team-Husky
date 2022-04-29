using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    // Manages player inventory (a list of the class Items) in a specific scene
    // n.b. GameManager should deal with data in inventory between scene transitions


    public List<Item> characterItems = new List<Item>(); // creates a list of Items (defined in class Items.cs) used for inventory 
    public ItemDatabase itemDatabase; // reference to item database; attached to same named game object in scene
    public UIInventory inventoryUI; // reference to UI Inventory class
    public TextMeshProUGUI musherNum; // assigned in Inspector
    public TextMeshProUGUI invincibilityNum; // assigned in Inspector
    public TextMeshProUGUI goldenNum; // assigned in Inspector


    private void Start()
    {
        // initialize inventory items
        AddItem(0);
        AddItem(1);
        musherNum.text = characterItems[0].ToString();
        invincibilityNum.text = characterItems[1].ToString();
        goldenNum.text = characterItems[2].ToString();

        //for testing
        AddItem(0);
        AddItem(0);
        AddItem(0);
        AddItem(1);
        AddItem(2);
    }

    public void AddItem(int id) // add item to inventory using item id
    {
        Item itemToAdd = itemDatabase.GetItem(id);
        

        if(id == 0)
        {
            if (null == ItemChecker(id)) // check if item is not in list
            {
                characterItems.Add(itemToAdd); // if so, add it
            }
            else
            {
                characterItems[id].amount += 3; // if it is, just increment amount
                musherNum.text = ItemChecker(id).amount.ToString(); //update UI
            }
        }
        else if (id == 1 || id == 2)
        {
            if (null == ItemChecker(id)) // check if item is not in list
            {
                characterItems.Add(itemToAdd); // if so, add it
            }
            else
            {
                characterItems[id].amount += 1; // if it is, just increment amount
                if(id == 1) invincibilityNum.text = ItemChecker(id).amount.ToString();
                else goldenNum.text = ItemChecker(id).amount.ToString();
            }
        }
        Debug.Log("Added item: " + itemToAdd.title);
    }

    public Item ItemChecker(int id) // find item in inventory using item's id
    {
        return characterItems.Find(item => item.id == id);
    }

    public void RemoveItem(int id) // remove item in inventory based on item id, only works with amount / UI elements 
    {
        characterItems[id].amount -= 1; // decrement inventory

        //update UI
        switch (id)
        {
            case 0:
                musherNum.text = (characterItems[id].amount).ToString();
                break;
            case 1:
                invincibilityNum.text = (characterItems[id].amount).ToString();
                break;
            case 2:
                goldenNum.text = (characterItems[id].amount).ToString();
                break;

        }
    }
}
