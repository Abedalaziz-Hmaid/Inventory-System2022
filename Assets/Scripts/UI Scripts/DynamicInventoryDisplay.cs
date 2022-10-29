using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class DynamicInventoryDisplay : InventoryDisplay
{
    [SerializeField] protected InventorySlot_UI slotPrefab;

    protected override void Start()
    {
        base.Start();
    }

    public void RefreshDynamicInventory(InventorySystem invTODisplay)
    {
        ClearSlots();
        inventorySystem = invTODisplay;
        AssignSlot(invTODisplay);
    }

    public override void AssignSlot(InventorySystem invToDisplay)
    {
        ClearSlots();

        slotDictionary = new Dictionary<InventorySlot_UI, InventroySlot>();

        if (invToDisplay == null) return;

        for (int i = 0; i < invToDisplay.InventorySize ; i++)
        {
            var uiSlot = Instantiate(slotPrefab , transform);
            slotDictionary.Add(uiSlot , invToDisplay.InventroySlots[i]);
            uiSlot.Init(invToDisplay.InventroySlots[i]);
            uiSlot.UpdateUISlot();
        }
    }

    private void ClearSlots()
    {
        foreach (var item in transform.Cast<Transform>())
        {
            Destroy(item.gameObject);
        }

        if (slotDictionary != null) slotDictionary.Clear();
    }
}
