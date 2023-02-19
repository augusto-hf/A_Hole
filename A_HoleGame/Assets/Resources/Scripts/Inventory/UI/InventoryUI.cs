using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private InventoryHolder inventory;
    [SerializeField] private CraftHandle craftHandle;
    [SerializeField] private List<InventorySlotUI> inventorySlotsUIs;
    private Dictionary<InventorySlot, InventorySlotUI> inventoryUI;


    private void Start() 
    {
        if (inventory == null)
        {
            SetInventory(craftHandle);
        }
        else
        {
            SetInventory(inventory);
        }
        
    }

    private void SetInventory(InventoryHolder inventoryHolder)
    {
        int slotCount = inventoryHolder.SizeInventory;
        if (slotCount == inventorySlotsUIs.Count)
        {
            inventoryUI = new Dictionary<InventorySlot, InventorySlotUI>(slotCount);
            
            for (int i = 0 ; i < slotCount; i++)
            {
                inventorySlotsUIs[i].SetSlot(inventoryHolder.Inventory.InventorySlots[i]);
                inventoryUI.Add(inventoryHolder.Inventory.InventorySlots[i], inventorySlotsUIs[i]);
            }
            
                inventoryHolder.Inventory.OnChangeSlot += UpdateInventoryUI;
        
        }
        else
        {
            
            Debug.LogError("Inventory Size não é igual ao Inventory Size UI");
        
        }
    }
    private void SetInventory(CraftHandle craftHandle)
    {
        int slotCount = craftHandle.CraftSlots.InventorySlots.Count;
        if (slotCount == inventorySlotsUIs.Count)
        {
            inventoryUI = new Dictionary<InventorySlot, InventorySlotUI>(slotCount);
            
            for (int i = 0 ; i < slotCount; i++)
            {
                inventorySlotsUIs[i].SetSlot(craftHandle.CraftSlots.InventorySlots[i]);
                inventoryUI.Add(craftHandle.CraftSlots.InventorySlots[i], inventorySlotsUIs[i]);
            }
            
                craftHandle.CraftSlots.OnChangeSlot += UpdateInventoryUI;
        
        }
        else
        {
            
            Debug.LogError("Inventory Size não é igual ao Inventory Size UI");
        
        }
    }

    private void UpdateInventoryUI(InventorySlot slot)
    {
        inventoryUI.TryGetValue(slot, out InventorySlotUI slotUI);
        slotUI.UpdateSlotUI(slot.ItemData, slot.Amount); 
    }

}
