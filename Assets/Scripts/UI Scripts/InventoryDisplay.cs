using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
public abstract class InventoryDisplay : MonoBehaviour
{
    [SerializeField] MouseItemData mouseIventoryItem;
    protected InventorySystem inventorySystem;
    protected Dictionary<InventorySlot_UI, InventroySlot> slotDictionary; // Pair up the UI slots with the System slots.

    public InventorySystem InventorySystem => inventorySystem;
    public Dictionary<InventorySlot_UI, InventroySlot> SlotDictionary => slotDictionary;

    protected virtual void Start()
    {

    }

    public abstract void AssignSlot(InventorySystem invToDisplay); // Implemented in child classes.

    protected virtual void UpdateSolt(InventroySlot updateSlot)
    {
        foreach (var slot in SlotDictionary)
        {
            if (slot.Value == updateSlot) // Slot value - the " under the hood " inventory slot .
            {
                slot.Key.UpdateUISlot(updateSlot); // Slot Key - the UI representations of the vlaue.   
            }
        }
    }

    public void SlotClicked(InventorySlot_UI clickedUISlot)
    {
        bool isShiftPressed = Keyboard.current.leftShiftKey.isPressed;
        //Clicked the slot has an item - mouse doesnt have an item   - pick up that item 
        if(clickedUISlot.AssignedInventorySlot.ItemData != null &&  mouseIventoryItem.AssignedInventorySlot.ItemData == null)
        {
            //If player Hold Shift Key / splite the Stack
            if (isShiftPressed && clickedUISlot.AssignedInventorySlot.SplitStack(out InventroySlot halfStackSlot))// slpit stack
            {
                mouseIventoryItem.UpdateMouseSlot(halfStackSlot);
                clickedUISlot.UpdateUISlot();
                return;
            }
            else // pick up the item in the clicked slot .
            {
                mouseIventoryItem.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot);
                clickedUISlot.ClearSlot();
                return;
            }           
        }

        //Clicked the Slot doesn't have an item - mouse does have an item - place the mouse item into the empty slot
        if(clickedUISlot.AssignedInventorySlot.ItemData == null && mouseIventoryItem.AssignedInventorySlot.ItemData != null){

            clickedUISlot.AssignedInventorySlot.AssignItem(mouseIventoryItem.AssignedInventorySlot);
            clickedUISlot.UpdateUISlot();

            mouseIventoryItem.ClearSlot();
        }
        //Both slots have an item - decide what to do...
        
        //Is the slot stack size + mouse stack size > the slot max size ? if so > take from mouse.

        //if differnt items , then swap the items .
        if (clickedUISlot.AssignedInventorySlot.ItemData != null && mouseIventoryItem.AssignedInventorySlot.ItemData != null)
        {

            bool isSameItem = clickedUISlot.AssignedInventorySlot.ItemData == mouseIventoryItem.AssignedInventorySlot.ItemData ;
            //Are both items the same ? if so combine them .
            if (isSameItem && clickedUISlot.AssignedInventorySlot.EnoughRoomLeftInStack(mouseIventoryItem.AssignedInventorySlot.StackSize))
            {
                clickedUISlot.AssignedInventorySlot.AssignItem(mouseIventoryItem.AssignedInventorySlot);
                clickedUISlot.UpdateUISlot();

                mouseIventoryItem.ClearSlot();
            }else if(isSameItem && !clickedUISlot.AssignedInventorySlot.EnoughRoomLeftInStack(mouseIventoryItem.AssignedInventorySlot.StackSize , out int leftInStack))
            {
                if (leftInStack < 1) SwapSlots(clickedUISlot); // Stack is full swap the item .
                else // Slot is not at max , so take what's need from mouse inventory
                {
                    int remainigOnMouse = mouseIventoryItem.AssignedInventorySlot.StackSize - leftInStack;
                    clickedUISlot.AssignedInventorySlot.AddToStack(leftInStack);
                    clickedUISlot.UpdateUISlot();

                    var newItem = new InventroySlot(mouseIventoryItem.AssignedInventorySlot.ItemData , remainigOnMouse);
                    mouseIventoryItem.ClearSlot();
                    mouseIventoryItem.UpdateMouseSlot(newItem);
                    return;
                }
            }else if(!isSameItem)
            {
                SwapSlots(clickedUISlot);
                return;
            }
        }
    }

    private void SwapSlots(InventorySlot_UI clickedUISlot)
    {
        var clonedSlot = new InventroySlot(mouseIventoryItem.AssignedInventorySlot.ItemData , mouseIventoryItem.AssignedInventorySlot.StackSize);
        mouseIventoryItem.ClearSlot();

        mouseIventoryItem.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot);

        clickedUISlot.ClearSlot();

        clickedUISlot.AssignedInventorySlot.AssignItem(clonedSlot);
        clickedUISlot.UpdateUISlot();

    }

}
