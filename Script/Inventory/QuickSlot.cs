using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;

public class QuickSlot : MonoBehaviour, IDropHandler
{
    public int index;
    public GameObject screenQuickSlotUI;
    [SerializeField] private GameObject screenQuickSlotUIPrefab;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        if (dropped.CompareTag("Inventory item") && dropped.GetComponent<InventoryData>().inventoryItemData.item_data.canConsume)
        {
            DragableItem dragableItem = dropped.GetComponent<DragableItem>();
            if (!dragableItem.fromQuickSlot)
            {
                QuickSlotList.Instance.AddItem(dropped.GetComponent<InventoryData>().inventoryItemData.item_data);
                QuickSlotList.Instance.CallRender(dropped.GetComponent<InventoryData>().inventoryItemData, index);
                dragableItem.fromQuickSlot = true;
            }
            dragableItem.quickSlotIndex = index;
            dragableItem.endQuickSlot = true;
            dragableItem.parentAfterDrag = this.transform;
/*            transform.parent.gameObject.GetComponent<QuickSlotList>().AddItem(dropped.GetComponent<InventoryData>().inventoryItemData.item_data);*/
        }

    }


}
