using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public int id;
    public string display_name;
    public bool canConsume = false;
    public bool canDrop = true;
    public Sprite icon;
}
