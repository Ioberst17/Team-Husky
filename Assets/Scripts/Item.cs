using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    // defines an item e.g. powerup and contains methods for their creation and copying

    // class attributes
    public int id;
    public string title;
    public string description;
    public int amount;
    public Sprite icon;
    public Dictionary<string, int> stats = new Dictionary<string, int>(); // to store item modifiers 'stats'

    public Item(int id, string title, string description, int amount, Dictionary<string, int> stats) // constructor method to build items
    {
        this.id = id;
        this.title = title;
        this.description = description;
        this.amount = amount;
        this.icon = Resources.Load<Sprite>("Resources/Sprites/Powerups/" + title); // file path in quotes must be to correct icon directory
        this.stats = stats;
    }

    public Item(Item item) // constructor method to copy existing items
    {
        this.id = item.id;
        this.title = item.title;
        this.description = item.description;
        this.amount = item.amount;
        this.icon = Resources.Load<Sprite>("Resources/Sprites/Powerups/" + item.title); // file path in quotes must be to correct icon directory
        this.stats = item.stats;
    }
}