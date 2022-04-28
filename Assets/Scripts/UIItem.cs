using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // needed to access Image class used below

public class UIItem : MonoBehaviour
{
    // Purpose of UI item is to handle showing and hiding a item's UI icon
    // if there is an item to show - render it with color opaque white (so it is seen)
    // otherwise set alpha to 0, so the item is not seen


    public Item item;
    private Image spriteImage;

    private void Awake()
    {
        spriteImage = GetComponent<Image>(); // assigns image inside item panel object
    }

    public void UpdateItem(Item item) // handles updating an image
    {
        this.item = item;
        if(this.item != null) // if item exists, make visible
        {
            spriteImage.color = Color.white;
            spriteImage.sprite = this.item.icon;
        }
        else // else make invisible (clear)
        {
            spriteImage.color = Color.clear;
        }
    }

}
