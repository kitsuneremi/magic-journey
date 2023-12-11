using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    
    public string id;
    public string display_name;
    public bool canConsume = false;
    public bool canDrop = true;
    public int quantity = 1;
    public Sprite icon;
    [ContextMenu("genrate id")]
    public void GenerateId()
    {
        id = System.Guid.NewGuid().ToString();
    }
}
