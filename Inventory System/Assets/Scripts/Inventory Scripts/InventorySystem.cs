using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

[System.Serializable]
public class InventorySystem
{
    [SerializeField] private List<InventroySlot> inventroySlots;

    public List<InventroySlot> InventroySlots => inventroySlots;

    public int InventorySize => InventroySlots.Count;

    public UnityAction<InventroySlot> OnInventoryChanged;

    public InventorySystem(int size) // Constructor that set s the amouts of slot.
    {
        inventroySlots = new List<InventroySlot>(size);
        for (int i = 0; i < size; i++)
        {
            inventroySlots.Add(new InventroySlot());
        }
    }

    public bool AddToInventory(InventoryItemData itemToAdd, int amountToAdd)
    {
        if (ContainsItem(itemToAdd , out List<InventroySlot> invSlot)) // Check whether item exists in invantory.
        {
            foreach (var slot in invSlot)
            {
                if (slot.EnoughRoomLeftInStack(amountToAdd))
                {
                    slot.AddToStack(amountToAdd);
                    OnInventoryChanged?.Invoke(slot);
                    return true;
                }
            }
        }

        if(HasFreeSlot(out InventroySlot freeSlot)) // Gets the first avaliable slot .
        {
            if (freeSlot.EnoughRoomLeftInStack(amountToAdd))
            {
                freeSlot.UpdateInventorySlot(itemToAdd, amountToAdd);
                OnInventoryChanged?.Invoke(freeSlot);
                return true;
            }
            // add implementaion to only take waht can fill the stack , and check for antoher free slot to put the remainder in . 
        }

        return false;
    }

    public bool ContainsItem(InventoryItemData itemToAdd , out List<InventroySlot> invSlot) // Do any of our slot have the item to add in them ?
    {
        invSlot = InventroySlots.Where(i => i.ItemData == itemToAdd).ToList(); // If they do , the get a list of all of them . 
        return invSlot == null ? false : true; // if they do return true , if not return false . 
    }

    public bool HasFreeSlot(out InventroySlot freeSlot)
    {
        freeSlot = InventroySlots.FirstOrDefault(i =>  i.ItemData == null); // Get the first free slot .
        return freeSlot == null ? false : true;
    }
}
