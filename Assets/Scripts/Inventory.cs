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

    public PlayerController playerController;
    public List<Item> characterItems = new List<Item>(); // creates a list of Items (defined in class Items.cs) used for inventory 
    public ItemDatabase itemDatabase; // reference to item database; attached to same named game object in scene
    public TextMeshProUGUI musherNum; // assigned in Inspector
    public TextMeshProUGUI invincibilityNum; // assigned in Inspector
    public TextMeshProUGUI goldenNum; // assigned in Inspector
    public TextMeshProUGUI toolkitNum; // assigned in Inspector


    private void Start()
    {
        playerController = gameObject.GetComponentInChildren<PlayerController>();
        // initialize inventory items
        AddItem(0);
        AddItem(1);
        AddItem(2);
        AddItem(3);
        characterItems[0].amount = 0;
        characterItems[1].amount = 0;
        characterItems[2].amount = 0;
        characterItems[3].amount = 0;
        musherNum.text = characterItems[0].ToString();
        invincibilityNum.text = characterItems[1].ToString();
        goldenNum.text = characterItems[2].ToString();
        toolkitNum.text = characterItems[3].ToString();
        // need to do this zero out the initial inventory
        // Add items
        AddItem(0);
        AddItem(1);
        AddItem(2);
        AddItem(3);
        // then remove them
        RemoveItem(0); // mushing whip is removed three times because when it is picked up / added, you get 3 vs. 1 with other powerups
        RemoveItem(0);
        RemoveItem(0);
        RemoveItem(1);
        RemoveItem(2);
        RemoveItem(3);
        // Do not delete the inventory initialization

        //for testing
        AddItem(0);
        AddItem(0);
        AddItem(0);
        AddItem(1);
        AddItem(1);
        AddItem(1);
        AddItem(2);
        AddItem(2);
        AddItem(2);
        AddItem(2);
        AddItem(3);
        AddItem(3);
        AddItem(3);
        AddItem(3);
        AddItem(3);
        AddItem(3);

    }

    public void AddItem(int id) // add item to inventory using item id
    {
        Item itemToAdd = itemDatabase.GetItem(id);

        if (id == 0)
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
        else if (id == 1 || id == 2 || id == 3)
        {
            if (null == ItemChecker(id)) // check if item is not in list
            {
                characterItems.Add(itemToAdd); // if so, add it
            }
            else
            {
                characterItems[id].amount += 1; // if it is, just increment amount
                // then update relevant UI
                if (id == 1) { invincibilityNum.text = ItemChecker(id).amount.ToString(); }
                if (id == 2) { goldenNum.text = ItemChecker(id).amount.ToString(); }
                if (id == 3) { toolkitNum.text = ItemChecker(id).amount.ToString(); }
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
        updateUI(id);
    }

    public void updateUI(int id)
    {
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
            case 3:
                toolkitNum.text = (characterItems[id].amount).ToString();
                break;

        }
    }
}