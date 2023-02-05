using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemTest : MonoBehaviour
{
    [SerializeField] private InventoryHolder inventory;
    [SerializeField] private int id;

    public void DropItem()
    {
        if (inventory.Inventory.DropItemFromInventory(id))
        {
            Debug.Log("Item Droped");
        }
    }
}
