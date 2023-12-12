using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnFieldItem : MonoBehaviour
{
    public ItemData itemData;
    public static event HandleBoxCollected OnCollected;
    public delegate void HandleBoxCollected(ItemData item_data);
    private bool isThrough = false;

    private void Start()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = itemData.icon;

    }
    public void Collect()
    {
        
        OnCollected?.Invoke(itemData);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Wizard") && !isThrough)
        {
            Collect();
            isThrough = true;
        }
    }
} 
