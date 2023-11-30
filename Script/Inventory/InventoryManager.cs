using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static event Action<List<InventoryItem>> OnInventoryChanged;

    public List<InventoryItem> inventory = new List<InventoryItem>(0);
    private Dictionary<ItemData, InventoryItem> itemDictionary = new Dictionary<ItemData, InventoryItem>();

    public GameObject slotPrefab;
    public List<InventorySlot> slots = new(6);

    private void OnEnable()
    {
        OnInventoryChanged += DrawInventory;
        DefaultItem.onCollected += AddItem;
    }

    private void OnDisable()
    {
        OnInventoryChanged -= DrawInventory;
        DefaultItem.onCollected -= AddItem;
    }

    public void AddItem(ItemData item_data)
    {
        if (itemDictionary.TryGetValue(item_data, out InventoryItem item))
        {
            item.Increase();
            OnInventoryChanged?.Invoke(inventory);
        }
        else
        {
            InventoryItem inventoryItem = new InventoryItem(item_data);
            inventory.Add(inventoryItem);
            itemDictionary.Add(item_data, inventoryItem);
            OnInventoryChanged.Invoke(inventory);
        }
    }

    public void Remove(ItemData item_data)
    {
        if (itemDictionary.TryGetValue(item_data, out InventoryItem item))
        {
            item.Decrease();
            if (item.quantity == 0)
            {
                inventory.Remove(item);
                itemDictionary.Remove(item_data);
            }
        }
    }

    public void ResetInventory()
    {
        foreach(Transform childTransform in this.transform)
        {
            Destroy(childTransform.gameObject);
        }
        slots = new List<InventorySlot>(6);
    }

    public void DrawInventory(List<InventoryItem> inventory)
    {
        ResetInventory();

        for (int i = 0; i < inventory.Count; i++)
        {
            CreateInventorySlot();
            slots[i].DrawSlot(inventory[i]);
            
        }
    }


    void CreateInventorySlot()
    {
        GameObject slot = Instantiate(slotPrefab);
        slot.transform.SetParent(transform, false);

        InventorySlot newSlotComponent = slot.GetComponent<InventorySlot>();
        newSlotComponent.ClearSlot();
        slots.Add(newSlotComponent);
    }
}
