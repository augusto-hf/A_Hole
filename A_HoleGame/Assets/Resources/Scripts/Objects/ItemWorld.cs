using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorld : MonoBehaviour
{
    [SerializeField] private ItemData ItemData;



    private void OnTriggerEnter2D(Collider2D other) 
    {

        if (other.CompareTag("Player"))
        {
            
            InventoryHolder playerInventory = other.GetComponent<InventoryHolder>();
            
            if (playerInventory.Inventory.AddItemToInventory(ItemData, 1))
            {
                Debug.Log("item added");
                Destroy(this.gameObject);

            }

        }
        
    }

}
