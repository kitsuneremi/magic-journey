using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class QuickSlotList : MonoBehaviour
{
    private static QuickSlotList instance;
    [SerializeField] private GameObject screenQuickSlotUI;
    [SerializeField] private GameObject screenQuickSlotUIPrefab;
    public static QuickSlotList Instance {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<QuickSlotList>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("QuickSlotList");
                    instance = obj.AddComponent<QuickSlotList>();
                }
            }
            return instance;
        }
    }

    public static event Action<List<InventoryItem>> OnQuickSlotChangged;

    public List<InventoryItem> listQuickSlotItem = new(4);
    [HideInInspector] public Dictionary<ItemData, InventoryItem> quickSlotDictionary = new Dictionary<ItemData, InventoryItem>(4);

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
        OnQuickSlotChangged += Print;
    }

    public void AddItem(ItemData item)
    {
        if (quickSlotDictionary.TryGetValue(item, out InventoryItem inventory))
        {
            inventory.quantity += item.quantity;
        }
        else
        {
            InventoryItem inventoryItem = new(item);
            quickSlotDictionary.Add(item, inventoryItem);
            listQuickSlotItem.Add(inventoryItem);
        }
        OnQuickSlotChangged.Invoke(listQuickSlotItem);
    }

    public void RemoveItem(InventoryItem inventoryItem)
    {
        if(quickSlotDictionary.TryGetValue(inventoryItem.item_data, out InventoryItem inventory))
        {
            listQuickSlotItem.Remove(inventory);
            quickSlotDictionary.Remove(inventory.item_data);
        }
        OnQuickSlotChangged.Invoke(listQuickSlotItem);
    }

    void Print(List<InventoryItem> list)
    {

    }
    public void CallRender(InventoryItem item, int index)
    {
        ClearSLot(index);
        GameObject ui = Instantiate(screenQuickSlotUIPrefab);
        ui.GetComponent<ScreenQuickSlotUI>().Render(item.item_data.icon, item.quantity);
        ui.transform.SetParent(screenQuickSlotUI.transform.GetChild(index));
    }


    public void ClearSLot(int index)
    {
        for (int i = 0; i < screenQuickSlotUI.transform.GetChild(index).childCount; i++)
        {
            Destroy(screenQuickSlotUI.transform.GetChild(index).GetChild(i).gameObject);
        }
    }


}
