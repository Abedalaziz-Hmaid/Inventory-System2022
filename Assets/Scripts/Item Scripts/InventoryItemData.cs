using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This is a Scriptable Object , that defines what and item is in our game.
/// It could be inherited from to have branched version of item , for example potions and equipment .
/// </summary>
[CreateAssetMenu (menuName ="Inventory System / Inventory Itme")]
public class InventoryItemData : ScriptableObject 
{
    public int ID;
    public string DisplayName;
    [TextArea(4, 4)]
    public string Description;
    public Sprite Icon;
    public int MaxStavkSize;
    public int GoldValue;

}
