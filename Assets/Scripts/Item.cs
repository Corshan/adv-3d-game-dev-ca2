using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    public string Name;
    public string Description;
    public int MaxAmount;
    public int Price;
    public Sprite Sprite;
    public GameObject Prefab;
}
