using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI stack_text;

    public void ClearSlot()
    {
        icon.enabled = false;
        stack_text.enabled = false;
    }

    public void DrawSlot(InventoryItem item)
    {
        if(item == null)
        {
            ClearSlot();
            return;
        }
        else
        {
            GetComponent<InventoryData>().data = item;
            icon.enabled = true;
            stack_text.enabled = true;
            icon.sprite = item.item_data.icon;
            stack_text.text = item.quantity.ToString();
        }
    }


}
