using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Items", menuName = "Item")]
public class Item : ScriptableObject
{
    public AudioClip vhsClip;
    public Sprite icon;
}