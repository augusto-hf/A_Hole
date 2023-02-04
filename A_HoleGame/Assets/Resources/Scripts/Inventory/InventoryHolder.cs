using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryHolder : MonoBehaviour
{
    [SerializeField] private int sizeInventory;
    [SerializeField] private InventorySystem inventory;

    public InventorySystem Inventory { get => inventory; }

    private void Awake() {

        inventory = new InventorySystem(sizeInventory);
        
    }
}
