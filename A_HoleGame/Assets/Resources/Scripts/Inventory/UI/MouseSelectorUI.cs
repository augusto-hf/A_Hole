using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class MouseSelectorUI : MonoBehaviour
{
    [SerializeField] private Image iconSprite;
    [SerializeField] private TextMeshProUGUI countText;

    private InventorySlot slot;
    private InventorySlotUI slotUI;
    private Camera cam;
    

    private void Awake()
    {
        
        slot = new InventorySlot();
        ClearSlotUI();
        InventorySlotUI.OnClickSlot += GrabItem;

    }
    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        Vector2 mousePositon = cam.ScreenToWorldPoint(Input.mousePosition);
        this.transform.position = mousePositon;

    }

    private void GrabItem(InventorySlotUI ui, InventorySlot slot)
    {

        if (slot.ItemData == null && this.slot.ItemData == null || slot.ItemData == null && this.slot.ItemData == null)
        {
            Debug.Log("deve fazer o L");
            return;
        }
        else if (slot.ItemData == null && this.slot.ItemData != null) //Slot do botão n tem nada e o slot do mouse esta com conteudo
        {
            slot.UpdateSlot(this.slot.ItemData, this.slot.Amount);
            ui.UpdateSlotUI(this.slot);
            this.ClearSlotUI();
            
        } 
        else if (slot.ItemData != null && this.slot.ItemData == null) //Caso o slot do botão tenha algo e Caso o slot do mouse esteja vazio
        {

            this.slot.UpdateSlot(slot.ItemData, slot.Amount);
            UpdateSlotUI();
            ui.ClearSlotUI();
        }
        

    }
    

    public void ClearSlotUI()
    {
        slot.ClearSlot();
        iconSprite.sprite = null;
        iconSprite.color = Color.clear;
        countText.text = "";
    }
    public void UpdateSlotUI()
    {
        iconSprite.sprite = slot.ItemData.Icon;
        iconSprite.color = Color.white;
        countText.text = slot.Amount.ToString();

    }

}
