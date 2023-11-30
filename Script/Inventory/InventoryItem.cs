using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class InventoryItem
{
    public ItemData item_data;
    public int quantity = 0;

    public InventoryItem(ItemData item)
    {
        item_data = item;
        Increase();
    }
    public InventoryItem(ItemData item, int quan)
    {
        item_data = item;
        quantity = quan;
    }

    public void Increase()
    {
        quantity++;
    }

    public void Decrease() { 
        quantity--;
    }
}
