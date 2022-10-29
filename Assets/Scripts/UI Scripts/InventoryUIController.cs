using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryUIController : MonoBehaviour
{
    public DynamicInventoryDisplay chestPanel;
    public DynamicInventoryDisplay playerBackPackPanel;

    private void Awake()
    {
        chestPanel.gameObject.SetActive(false);
        playerBackPackPanel.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        InventoryHolder.OnDynamicInveantoryDisplayRequested += DisplayInventory;
        PlayerInventoryHolder.OnPlayerBackPackDisplayRequsted += DisplayPlayerBackPack;
    }

    private void OnDisable()
    {
        InventoryHolder.OnDynamicInveantoryDisplayRequested -= DisplayInventory;
        PlayerInventoryHolder.OnPlayerBackPackDisplayRequsted -= DisplayPlayerBackPack;
    }

    private void Update()
    {
        if (chestPanel.gameObject.activeInHierarchy && Keyboard.current.escapeKey.wasPressedThisFrame) 
            chestPanel.gameObject.SetActive(false);

        if (playerBackPackPanel.gameObject.activeInHierarchy && Keyboard.current.escapeKey.wasPressedThisFrame)
            playerBackPackPanel.gameObject.SetActive(false);
    }

    void DisplayInventory(InventorySystem invToDisplay)
    {
        chestPanel.gameObject.SetActive(true);
        chestPanel.RefreshDynamicInventory(invToDisplay);
    }
    
    void DisplayPlayerBackPack(InventorySystem invToDisplay)
    {
        playerBackPackPanel.gameObject.SetActive(true);
        playerBackPackPanel.RefreshDynamicInventory(invToDisplay);
    }




}
