using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private static InventoryManager instance;
    public static InventoryManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<InventoryManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("InventoryManager");
                    instance = obj.AddComponent<InventoryManager>();
                }
            }
            return instance;
        }
    }

    public static event Action<List<InventoryItem>> OnInventoryChanged;

    public List<InventoryItem> inventory = new(0);
    private Dictionary<string, InventoryItem> itemDictionary = new();

    public GameObject slotPrefab;
    [HideInInspector]public List<InventorySlot> slots = new(6);

    private void Awake()
    {
        // Đảm bảo rằng chỉ có một instance tồn tại
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void OnEnable()
    {
        OnInventoryChanged += DrawInventory;
        OnFieldItem.OnCollected += AddItem;
    }

    private void OnDisable()
    {
        OnInventoryChanged -= DrawInventory;
        OnFieldItem.OnCollected -= AddItem;
    }

    public void AddItem(ItemData item_data)
    {

        if (itemDictionary.TryGetValue(item_data.id, out InventoryItem item))
        {
            item.Increase();
            OnInventoryChanged?.Invoke(inventory);
        }
        else
        {
            InventoryItem inventoryItem = new(item_data);
            inventory.Add(inventoryItem);
            itemDictionary.Add(item_data.id, inventoryItem);
            OnInventoryChanged.Invoke(inventory);
        }
    }

    public void Remove(ItemData item_data)
    {
        if (itemDictionary.TryGetValue(item_data.id, out InventoryItem item))
        {
            item.Decrease();
            if (item.quantity == 0)
            {
                inventory.Remove(item);
                itemDictionary.Remove(item_data.id);
            }
        }
    }

    public void ResetInventory()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            try
            {
                Destroy(transform.GetChild(i).GetChild(0).gameObject);
            }catch (Exception) { }

        }
        slots = new List<InventorySlot>(inventory.Count);
    }

    public void DrawInventory(List<InventoryItem> inventory)
    {
        ResetInventory();
        for (int i = 0; i < inventory.Count; i++)
        {
            CreateInventorySlot(i);
            slots[i].DrawSlot(inventory[i]);

        }
    }


    void CreateInventorySlot(int index)
    {
        GameObject slot = Instantiate(slotPrefab);
        slot.transform.SetParent(transform.GetChild(index), false);

        InventorySlot newSlotComponent = slot.GetComponent<InventorySlot>();
        slots.Add(newSlotComponent);
    }
}
