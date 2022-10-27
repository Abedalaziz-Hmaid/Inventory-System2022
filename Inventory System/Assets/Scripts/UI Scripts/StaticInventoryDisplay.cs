using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticInventoryDisplay : InventoryDisplay
{
    [SerializeField] private InventoryHolder inventoryHolder;
    [SerializeField] private InventorySlot_UI[] slots;


    protected override void Start()
    {
        base.Start();

        if (inventoryHolder != null)
        {
            inventorySystem = inventoryHolder.InventorySystem;
            inventorySystem.OnInventoryChanged += UpdateSolt;
        }
        else
            Debug.Log("no inventory assigned to" + this.gameObject);

        AssignSlot(inventorySystem);
    }

    public override void AssignSlot(InventorySystem invToDisplay)
    {
        slotDictionary = new Dictionary<InventorySlot_UI, InventroySlot>();

        if (slots.Length != inventorySystem.InventorySize) Debug.Log("Inventory Slots out of sync" + this.gameObject);

        for (int i = 0; i < inventorySystem.InventorySize; i++)
        {
            slotDictionary.Add(slots[i], inventorySystem.InventroySlots[i]);
            slots[i].Init(inventorySystem.InventroySlots[i]);
        }
    }

    
}
