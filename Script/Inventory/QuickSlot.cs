using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuickSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        if (dropped.CompareTag("Inventory item") && dropped.GetComponent<InventoryData>().inventoryItemData.item_data.canConsume)
        {
            DragableItem dragableItem = dropped.GetComponent<DragableItem>();
            if (!dragableItem.isQuickSlot)
            {
                QuickSlotList.Instance.AddItem(dropped.GetComponent<InventoryData>().inventoryItemData.item_data);
                dragableItem.isQuickSlot = true;
            }
            dragableItem.parentAfterDrag = this.transform;
/*            transform.parent.gameObject.GetComponent<QuickSlotList>().AddItem(dropped.GetComponent<InventoryData>().inventoryItemData.item_data);*/
        }

    }
}
