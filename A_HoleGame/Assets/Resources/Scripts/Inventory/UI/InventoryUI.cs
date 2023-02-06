using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private InventoryHolder inventory;
    [SerializeField] private List<InventorySlotUI> inventorySlotsUIs;
    private Dictionary<InventorySlot, InventorySlotUI> inventoryUI;


    private void Start() 
    {
        int slotCount = inventory.SizeInventory;
        if (slotCount == inventorySlotsUIs.Count)
        {
            inventoryUI = new Dictionary<InventorySlot, InventorySlotUI>(slotCount);
            
            for (int i = 0 ; i < slotCount; i++)
            {
                inventoryUI.Add(inventory.Inventory.InventorySlots[i], inventorySlotsUIs[i]);
            }
            
            inventory.Inventory.OnChangeSlot += UpdateInventoryUI;
        
        }
        else
        {
            
            Debug.LogError("Inventory Size não é igual ao Inventory Size UI");
        
        }
        
    }

    private void UpdateInventoryUI(InventorySlot slot)
    {
        inventoryUI.TryGetValue(slot, out InventorySlotUI slotUI);
        slotUI.UpdateSlotUI(slot); 
    }

}
