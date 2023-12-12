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
        InventoryItem data = dropped.GetComponent<InventoryData>().data;
        if (dropped.CompareTag("Inventory item") && data.item_data.canConsume)
        {
            DragableItem dragableItem = dropped.GetComponent<DragableItem>();
            // check có vật phẩm nào bên dưới chưa
            if (transform.childCount == 0)
            {
                QuickSlotManager.Instance.CallRender(data, index);
                dragableItem.quickSlotIndex = index;
                dragableItem.endQuickSlot = true;
                dragableItem.parentAfterDrag = this.transform;
                if (!dragableItem.fromQuickSlot)
                {
                    Debug.Log("remove");
                    InventoryManager.Instance.Remove(data.item_data);
                }
            }
            else
            {
                // nếu đã có thì check trùng id?
                if (GetComponentInChildren<InventoryData>().data.item_data.id == data.item_data.id)
                {

                    // cộng số lượng và render lại -> clear
                    /*                    QuickSlotManager.Instance.AddItem();*/
                    data.quantity += GetComponentInChildren<InventoryData>().data.item_data.quantity;
                    GetComponentInChildren<InventoryData>().data.quantity = data.quantity;
                    ClearSlot();
                    dropped.GetComponent<InventorySlot>().DrawSlot(GetComponentInChildren<InventoryData>().data);
                    QuickSlotManager.Instance.ClearSLot(index);
                    QuickSlotManager.Instance.CallRender(data, index);
                    dragableItem.quickSlotIndex = index;
                    dragableItem.endQuickSlot = true;
                    dragableItem.parentAfterDrag = this.transform;
                    if (!dragableItem.fromQuickSlot)
                    {
                        InventoryManager.Instance.Remove(data.item_data);
                    }
                }
                else
                {
                    //nhả
                }
            }
        }

    }


    void ClearSlot()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

}
