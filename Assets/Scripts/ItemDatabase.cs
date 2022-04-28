using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    // ItemDatabase contains a List used to store and keep track of items e.g. a Dictionary Database and related methods

    public List<Item> items = new List<Item>();

    private void Awake()
    {
        BuildDatabase();
    }

    public Item GetItem(int id) //used to grab items from database using ID
    {
        return items.Find(item => item.id == id);
    }

    void BuildDatabase()
    {
        items = new List<Item>()
        {
            new Item( // 1st item
                0, "Musher", "Grants 3 speed boosts", //id, title, description
                new Dictionary<string, int> // stats
                    {
                        {"Power", 15 },
                        {"Defense", 10 }
                    }),
            new Item( // 2nd item
                1, "Invincibility", "Grants temporary invincibility", //id, title, description
                new Dictionary<string, int> // stats
                    {
                        {"Power", 15 },
                        {"Defense", 10 }
                    }),
            new Item( // 3rd item
                2, "Golden", "Grants zero slow down in all terrains", //id, title, description
                new Dictionary<string, int> // stats
                    {
                        {"Power", 15 },
                        {"Defense", 10 }
                    })
        };
    }

}
