using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenQuickSlotUI : MonoBehaviour
{
    public GameObject iconUI;
    public GameObject quantityUI;

    public void Render(Sprite sprite, int quantity)
    {
        iconUI.GetComponent<Image>().sprite = sprite;
        quantityUI.GetComponent<TextMeshProUGUI>().text = quantity.ToString();
    }
}
