using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class InventorySystem
{
    private List<InventorySlot> inventorySlots;

    public List<InventorySlot> InventorySlots { get => inventorySlots; }
    public int InventorySize { get => inventorySlots.Count; }

    public InventorySystem (int size)
    {
        inventorySlots = new List<InventorySlot>(size);
        
        for (int i = 0; i < size; i++)
        {
            inventorySlots.Add(new InventorySlot());
        }
    }

    public bool AddItemToInventory(ItemData itemData, int amount)
    {
        if(ContainsItem(itemData, out List<InventorySlot> invSlot))
        {
            foreach(var slot in invSlot)
            {
                if (slot.AddAmount(amount))
                {
                    Debug.Log("item stacked");
                    return true;
                }
            }
        }
        
        if (HasFreeSlot(out InventorySlot freeslot))
        {
            freeslot.UpdateSlot(itemData, amount);
            return true;
        }

        return false;
    }

    public bool ContainsItem(ItemData itemData, out List<InventorySlot> invSlot)
    {
        invSlot = inventorySlots.Where(s => s.ItemData == itemData).ToList();
        return invSlot != null ? true : false ;

    }
    public bool HasFreeSlot(out InventorySlot freeSlot)
    {
        freeSlot = inventorySlots.FirstOrDefault(s => s.ItemData == null);
        return freeSlot == null ? false : true;
    }

}
