using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenInventory : MonoBehaviour
{
    public GameObject InventoryUI;
    // Start is called before the first frame update
    void Start()
    {
        InventoryUI.GetComponent<RectTransform>().LeanSetLocalPosX(805f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenUpInventory()
    {
        InventoryUI.GetComponent<RectTransform>().LeanSetLocalPosX(0f);
    }

    public void CloseDownInventory()
    {
        InventoryUI.GetComponent<RectTransform>().LeanSetLocalPosX(805f);
    }
}
