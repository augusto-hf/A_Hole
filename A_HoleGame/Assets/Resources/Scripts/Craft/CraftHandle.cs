using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftHandle : MonoBehaviour
{
    private InventorySystem craftSlot;
    private List<InventorySlot> inputSlots;
    private List<ItemData> input; 
    private InventorySlot output;

    [SerializeField] private List<ItemRecipe> recipes;
    public InventorySlot Output { get => output; }
    public InventorySystem CraftSlots { get => craftSlot; }

    private void Awake()
    {
        int totalSlots = 4;
        int inputSlotCount = 3;

        craftSlot = new InventorySystem(totalSlots);
        inputSlots = new List<InventorySlot>(inputSlotCount);
        input = new List<ItemData>(inputSlotCount);
        output = new InventorySlot();
        
        for (int i = 0; i < inputSlotCount; i++)
        {
            inputSlots.Add(new InventorySlot());
            input.Add(null);
        }

        for (int i = 0; i < inputSlotCount; i++)
        {
            inputSlots[i] = craftSlot.InventorySlots[i];
        }

        output = craftSlot.InventorySlots[totalSlots - 1];

        craftSlot.OnChangeSlot += UpdateCraftSlot;

    }

    private void UpdateCraftSlot(InventorySlot inv)
    {
        int maxInputCount = craftSlot.InventorySlots.Count - 1;

        input = new List<ItemData>(maxInputCount);
        
        for (int i = 0; i < maxInputCount; i++)
        {
            input.Add(craftSlot.InventorySlots[i].ItemData);
        }
    }

    public void Craft()
    {
        if (inputSlots.All(a => a.ItemData == null) || output.ItemData != null) return;

        foreach (var recipe in recipes)
        {
            if (recipe.Input.SequenceEqual(input))
            {
                if (output.ItemData == null)
                {
                    output.UpdateSlot(recipe.Output, 1);
                }
                else
                {
                    if (output.ItemData == recipe.Output)
                    {
                        output.AddAmount(1);
                    }
                }
                
                return;
            }
        }

    }
}
