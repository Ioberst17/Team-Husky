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
        AddItem(0);
        AddItem(1);
        RemoveItem(0);
        AddItem(0);
    }

    public void AddItem(int id) // add item to inventory using item id
    {
        Item itemToAdd = itemDatabase.GetItem(id);
        
        if(id == 0)
        {
            characterItems.Add(itemToAdd);
        }
        else if (id == 1 || id == 2)
        {
            characterItems.Add(itemToAdd);
            
        }
        Debug.Log("Added item: " + itemToAdd.title);
    }

    public Item ItemChecker(int id) // find item in inventory using item's id
    {
        return characterItems.Find(item => item.id == id);
    }

    //RemoveItem: NEEDS MORE TESTING TO MAKE SURE CHARACTER ITEMS ARE ACTUALLY REMOVED (they are checked, but not removed)
    public void RemoveItem(int id) // remove item in inventory based on item name; 
    {
        Item itemToRemove = ItemChecker(id);
        Debug.Log("Item Checked-in:" + itemToRemove.title);
        if (itemToRemove.title != null)
        {
            characterItems.Remove(itemToRemove);
            Debug.Log("Item removed: " + itemToRemove.title);
        }
    }
}
