using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot_UI : MonoBehaviour
{
    [SerializeField] private Image itemSprite;
    [SerializeField] private TextMeshProUGUI itemCount;
    [SerializeField] private InventroySlot assignedInventorySlot;

    private Button button;

    public InventroySlot AssignedInventorySlot => assignedInventorySlot;

    public InventoryDisplay ParentDisplay { get; private set; }

    private void Awake()
    {
        ClearSlot();         
        button = GetComponent<Button>();
        button?.onClick.AddListener(OnUISlotClick);

        ParentDisplay = transform.parent.GetComponent<InventoryDisplay>();
    }

    public void Init (InventroySlot slot)
    {
        assignedInventorySlot = slot;
        UpdateUISlot(slot); ;
    }

    public void UpdateUISlot(InventroySlot slot)
    {
        if (slot.ItemData != null )
        {
            itemSprite.sprite = slot.ItemData.Icon;
            itemSprite.color = Color.white;

            if (slot.StackSize > 1) itemCount.text = slot.StackSize.ToString();
            else itemCount.text = "";
        }
        else
        {
            ClearSlot();
        }
    }

    public void UpdateUISlot()
    {
        if (assignedInventorySlot != null) UpdateUISlot(assignedInventorySlot);
    }

    public void ClearSlot()
    {
        assignedInventorySlot?.ClearSlot();
        itemSprite.sprite = null;
        itemSprite.color = Color.clear;
        itemCount.text = "";
    }
    public void OnUISlotClick()
    {
        ParentDisplay?.SlotClicked(this);
    }
}
