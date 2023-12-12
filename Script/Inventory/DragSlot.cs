using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        if(dropped.CompareTag("Inventory item"))
        {
            DragableItem dragableItem = dropped.GetComponent<DragableItem>();
            InventoryItem data = dropped.GetComponent<InventoryData>().data;
            // check có vật phẩm nào bên dưới chưa
            if (transform.childCount == 0)
            {
                InventoryManager.Instance.AddItem(data.item_data);
                SetPosition(dragableItem);
            }
            else
            {
                // nếu đã có thì check trùng id?
                if (GetComponentInChildren<InventoryData>().data.item_data.id == data.item_data.id)
                {
                    // cộng số lượng và render lại -> clear
                    /*                    QuickSlotManager.Instance.AddItem();*/
                    data.quantity += GetComponentInChildren<InventoryData>().data.item_data.quantity;
                    InventoryManager.Instance.AddItem(data.item_data);
                    ClearSlot();
                    InventoryManager.Instance.DrawInventory(InventoryManager.Instance.inventory);
                    Destroy(dropped);
                    if (dragableItem.fromQuickSlot)
                    {
                        QuickSlotManager.Instance.ClearSLot(dragableItem.quickSlotIndex);
                    }
                    SetPosition(dragableItem);

                }
                else
                {
                    //nhả
                }
            }


            //
/*            DragableItem dragableItem = dropped.GetComponent<DragableItem>();
            if (dragableItem.fromQuickSlot)
            {
                transform.parent.gameObject.transform.parent.GetComponent<QuickSlotManager>();
                InventoryManager.Instance.AddItem(dropped.GetComponent<InventoryData>().data.item_data);
                QuickSlotManager.Instance.RemoveItem(dropped.GetComponent<InventoryData>().data);
                QuickSlotManager.Instance.ClearSLot(dragableItem.quickSlotIndex);
                dragableItem.fromQuickSlot = false;
            }*/

        }
        
    }

    void SetPosition(DragableItem dragableItem)
    {
        dragableItem.endQuickSlot = false;
        dragableItem.quickSlotIndex = -1;
        dragableItem.parentAfterDrag = this.transform;
    }

    void ClearSlot()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
