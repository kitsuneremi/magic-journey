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
    [HideInInspector] public bool isQuickSlot;

    public void OnBeginDrag(PointerEventData eventData)
    {
        try
        {
            if (transform.parent != null)
            {
                DragSlot dragSlotComponent = transform.parent.gameObject.GetComponent<DragSlot>();
                if (dragSlotComponent != null)
                {
                    isQuickSlot = false;
                }
                else
                {
                    isQuickSlot = true;
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
    }

    void Start()
    {
        canvasGrp = GetComponent<CanvasGroup>();
    }
}
