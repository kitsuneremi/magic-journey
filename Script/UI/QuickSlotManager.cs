using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class QuickSlotManager : MonoBehaviour
{
    private static QuickSlotManager instance;
    [SerializeField] private GameObject screenQuickSlotUI;
    [SerializeField] private GameObject screenQuickSlotUIPrefab;
    public static QuickSlotManager Instance {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<QuickSlotManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("QuickSlotList");
                    instance = obj.AddComponent<QuickSlotManager>();
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
        /*        Debug.Log(item.item_data.id + " child: " + screenQuickSlotUI.transform.GetChild(index).childCount);
                if (screenQuickSlotUI.transform.GetChild(index).childCount > 0)
                {
                    Debug.Log(screenQuickSlotUI.transform.GetChild(index).GetComponentInChildren<InventoryData>().data.item_data.id);
                    if (screenQuickSlotUI.transform.GetChild(index).GetComponentInChildren<InventoryData>().data.item_data.id == item.item_data.id)
                    {
                        screenQuickSlotUI.transform.GetChild(index).GetComponentInChildren<InventoryData>().data.quantity += item.quantity;
                    }
                }
                else
                {

                }*/
        ClearSLot(index);
        GameObject ui = Instantiate(screenQuickSlotUIPrefab);
        ui.GetComponent<ScreenQuickSlotUI>().Render(item.item_data.icon, item.quantity);
        ui.GetComponent<InventoryData>().data = item;
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
