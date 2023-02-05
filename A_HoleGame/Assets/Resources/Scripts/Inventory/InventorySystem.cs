using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class InventorySystem
{
    [SerializeField] private List<InventorySlot> inventorySlots;

    public List<InventorySlot> InventorySlots { get => inventorySlots; }
    public int InventorySize { get => inventorySlots.Count; }

    public InventorySystem (int size)
    {
        inventorySlots = new List<InventorySlot>(size);
        
        for (int i = 0; i < size; i++)
        {
            inventorySlots.Add(new InventorySlot());
            inventorySlots[i].UpdateInventorySlot += UpdateSlot;
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

    #region Drop Item
    public bool DropItemFromInventory(ItemData itemData, int amount)
    {
        if (ContainsItem(itemData, out InventorySlot invSlot))
        {
            invSlot.DecreaseAmout(amount);
            return true;
        }
        return false;
    }
    public bool DropItemFromInventory(ItemData itemData)
    {
        if (ContainsItem(itemData, out InventorySlot invSlot))
        {
            invSlot.ClearSlot();
            return true;
        }
        return false;
    }
    public bool DropItemFromInventory(int id, int amount)
    {
        int idValidate = Mathf.Clamp(id, 0, inventorySlots.Count - 1);
        if (inventorySlots[idValidate].DecreaseAmout(amount))
        {
            return true;
        }

        return false;  
    }
    public bool DropItemFromInventory(int id)
    {
        int idValidate = Mathf.Clamp(id, 0, inventorySlots.Count - 1);
        if (inventorySlots[idValidate].DecreaseAmout(InventorySlot.MAX_AMOUNT))
        {
            return true;
        }
        return false;
    }
    #endregion

    private void UpdateSlot(InventorySlot inv)
    {
        if (inv.ItemData != null)
        {
            Debug.Log($"Update Slot => name: {inv.ItemData.name} amount: {inv.Amount}");
        }else
        {
            Debug.Log($"No have item here");
        }
        
    }
    public bool ContainsItem(ItemData itemData, out List<InventorySlot> invSlot)
    {
        invSlot = inventorySlots.Where(s => s.ItemData == itemData).ToList();
        return invSlot != null ? true : false;

    }
    public bool ContainsItem(ItemData itemData, out InventorySlot invSlot)
    {
        invSlot = inventorySlots.FirstOrDefault(s => s.ItemData == itemData);
        return invSlot != null ? true : false;
    }
    public bool HasFreeSlot(out InventorySlot freeSlot)
    {
        freeSlot = inventorySlots.FirstOrDefault(s => s.ItemData == null);
        return freeSlot == null ? false : true;
    }

}
