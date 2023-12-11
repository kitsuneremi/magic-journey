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
            if (dragableItem.fromQuickSlot)
            {
                transform.parent.gameObject.transform.parent.GetComponent<QuickSlotList>();
                QuickSlotList.Instance.RemoveItem(dropped.GetComponent<InventoryData>().inventoryItemData);
                QuickSlotList.Instance.ClearSLot(dragableItem.quickSlotIndex);
                dragableItem.fromQuickSlot = false;
            }
            dragableItem.endQuickSlot = false;
            dragableItem.quickSlotIndex = -1;
            dragableItem.parentAfterDrag = this.transform;
        }
        
    }
}
