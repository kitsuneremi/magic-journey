using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class QuickSlotList : MonoBehaviour
{
    private static QuickSlotList instance;
    private GameObject screenQuickSlotUI;
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
        screenQuickSlotUI = GameObject.Find("Screen quick slot UI");
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
        OnQuickSlotChangged += CallRender;
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

    void CallRender(List<InventoryItem> list)
    {
        for (int i = 0; i < 1; i++)
        {
            if (i > list.Count - 1)
            {
                Debug.Log("no item");
            }
            else
            {
                InventoryItem item = list[i];
                Debug.Log(item.item_data.display_name);
                GameObject ui = Instantiate(screenQuickSlotUIPrefab);
                ui.GetComponent<ScreenQuickSlotUI>().Render(item.item_data.icon, item.quantity);

                if (screenQuickSlotUI.transform.GetChild(i) == null)
                {
                    ui.transform.SetParent(screenQuickSlotUI.transform.GetChild(i));
                }
                else
                {
                    ui.transform.SetParent(screenQuickSlotUI.transform.GetChild(i + 1));
                }
            }

        }
    }
}
