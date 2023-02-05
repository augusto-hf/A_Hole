using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    public const int MAX_AMOUNT = 99;
    private ItemData itemData;
    private int amount;

    public ItemData ItemData { get => itemData; }
    public int Amount { get => amount; }

    public InventorySlot(ItemData itemData, int amount)
    {

        int minAmount = 1;
        int nullAmount = -1;

        if (itemData == null)
        {
            this.itemData = null;
            this.amount = nullAmount;
            return;
        }
        else if (amount <= 0 && itemData != null)
        {
            amount = minAmount;
        }

        this.itemData = itemData;
        this.amount = amount;
        

    }
    public InventorySlot()
    {
        ClearSlot();
    }

    public bool AddAmount(int amount)
    {
        int amountResult = this.amount + amount;
        
        if (amountResult > MAX_AMOUNT)
        {
            this.amount = MAX_AMOUNT;
            return false;
        }

        this.amount = amountResult;

        return true;
    }
    public bool DecreaseAmout (int amount)
    {
        int minAmount = 1;
        int amountResult = this.amount - amount;
        
        if (amountResult <= 0)
        {
            ClearSlot();
            return false;
        }
        
        int validateAmount = Mathf.Max(this.amount - amount, minAmount);
        this.amount = validateAmount;

        return true;

    }

    public void ClearSlot()
    {
        this.itemData = null;
        this.amount = -1;
    }

    public void UpdateSlot(ItemData itemData, int amount)
    {
        this.itemData = itemData;
        this.amount = amount;
    }

}
