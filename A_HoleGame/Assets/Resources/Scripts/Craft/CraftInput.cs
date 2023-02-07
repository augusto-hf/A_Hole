using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftInput : MonoBehaviour
{
    [SerializeField] private List<InventorySlot> craftSlots;
    [SerializeField] private int sizeSlots;
    public Action<InventorySlot> OnChangeSlot;
    
    private void Awake()
    {
        craftSlots = new List<InventorySlot>(sizeSlots);
        
        for (int i = 0; i < sizeSlots; i++)
        {
            craftSlots.Add(new InventorySlot());
            craftSlots[i].UpdateInventorySlot += UpdateCraftSlot;
        }
    
    }

    public void SetItem(ItemMaterial material, int amount)
    {
        if (HasFreeSlot(out InventorySlot freeSlot))
        {
            freeSlot.UpdateSlot(material, amount);
        }
    }

    private void UpdateCraftSlot(InventorySlot slot)
    {
        OnChangeSlot?.Invoke(slot);
    }

    public bool HasFreeSlot(out InventorySlot freeSlot)
    {
        freeSlot = craftSlots.FirstOrDefault(s => s.ItemData == null);
        return freeSlot == null ? false : true;
    }

}
