using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object/ItemData")]
public class cItemData : ScriptableObject
{
    [Header("ItemInfo")]
    public Sprite itemSprite;
    public string itmeType;
    public string itemName;
    public string itemDesc;
    public string itemPrice;
    public int price;

    [Header("ItemStat")]
    public float str;
    public float spell;
    public float defen;
    public float hp;
    public float coolTime;
}
