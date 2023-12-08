using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI label_text;
    public TextMeshProUGUI stack_text;

    private void Start()
    {
/*        ClearSlot();*/
    }

    public void ClearSlot()
    {
        icon.enabled = false;
        label_text.enabled = false;
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
            icon.enabled = true;
            label_text.enabled = true;
            stack_text.enabled = true;
            icon.sprite = item.item_data.icon;
            label_text.text = item.item_data.display_name;
            stack_text.text = item.quantity.ToString();
        }
    }


}
