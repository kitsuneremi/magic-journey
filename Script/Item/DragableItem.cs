using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragableItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [HideInInspector] public Image image;
    [HideInInspector] public CanvasGroup canvasGrp;
    [HideInInspector] public Transform parentAfterDrag;
    [HideInInspector] public bool fromQuickSlot;
    [HideInInspector] public bool endQuickSlot;
    [HideInInspector] public int quickSlotIndex = -1;
    public void OnBeginDrag(PointerEventData eventData)
    {
        try
        {
            if (transform.parent != null)
            {
                if (transform.parent.gameObject.TryGetComponent<QuickSlot>(out var quickSlotComponent))
                {
                    fromQuickSlot = true;
                }
                else
                {
                    fromQuickSlot = false;
                }
            }

            // Các dòng code khác ở đây
        }
        catch (NullReferenceException ex)
        {
            Debug.LogError($"Exception: {ex.Message}");
        }
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        canvasGrp.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        canvasGrp.blocksRaycasts = true;
/*        if(fromQuickSlot && !endQuickSlot)
        {
            QuickSlotList.Instance.ClearSLot(quickSlotIndex);
        }*/
/*        QuickSlotList.Instance.ClearSLot(quickSlotIndex);*/
    }

    void Start()
    {
        canvasGrp = GetComponent<CanvasGroup>();
    }
}
