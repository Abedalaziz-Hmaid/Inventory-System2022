using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class InventroySlot 
{
    [SerializeField] private InventoryItemData itemData; // Reference of the data
    [SerializeField] private int stackSize; // Current  stack size - how many of the data  do we have

    public InventoryItemData ItemData => itemData;
    public int StackSize => stackSize;

    public InventroySlot(InventoryItemData source , int amount) // Constructor to make a accupied inventory slot.
    {
        itemData = source;
        stackSize = amount;
    }   

    public InventroySlot()  // Constructor to Clear the slot. 
    {
        ClearSlot();
    }

    public void ClearSlot()
    {
        itemData = null;
        stackSize = -1; 
    }

    public void AssignItem(InventroySlot invSlot) // Assigns an item to the slot.
    {
        if (itemData == invSlot.ItemData) AddToStack(invSlot.stackSize);// dose the slot containe the same item? Add to stack if so.
        else // Overwrite slot with the inventory slot that we're passing in .
        {
            itemData = invSlot.itemData;
            stackSize = 0;
            AddToStack(invSlot.stackSize);
        }
    }
    public void UpdateInventorySlot(InventoryItemData data, int amount) // Update slot Directly
    {
        itemData = data;
        stackSize = amount;
    }

    public bool EnoughRoomLeftInStack(int amountToAdd , out int amountRemaining) // wolud the enough room in the stack for amount we're try to add.
    {
        amountRemaining = ItemData.MaxStavkSize - stackSize;
        return EnoughRoomLeftInStack(amountToAdd);
    }

    public bool EnoughRoomLeftInStack(int amountToAdd)
    {
        if (itemData == null || itemData != null && stackSize + amountToAdd <= itemData.MaxStavkSize) return true;
        else return false;
    }


    public void AddToStack(int amount)
    {
        stackSize += amount;
    }

    public void RemoveFromStack(int amount)
    {
        stackSize -= amount;
    }

    public bool SplitStack(out InventroySlot splitStack)
    {
        if(stackSize <= 1) // is there enough to actually split? if not return false 
        {
            splitStack = null;
            return false;
        }

        int halfStack = Mathf.RoundToInt(stackSize / 2 ); // get half the stack
        RemoveFromStack(halfStack);

        splitStack = new InventroySlot(itemData , halfStack); // Create Copy of this slot with half the stack size.
        return true;
    }



}
