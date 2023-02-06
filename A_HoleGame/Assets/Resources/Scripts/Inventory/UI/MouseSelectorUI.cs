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
    [SerializeField] private Vector3 offsetMousePosition;

    private InventorySlot slot;
    private InventorySlotUI slotUI;
    private RectTransform rect;
    private Camera cam;

    private void Awake()
    {
        
        slot = new InventorySlot();
        ClearSlotMouseUI();
        InventorySlotUI.OnClickSlot += GrabItem;
        rect = GetComponent<RectTransform>();

    }
    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        Vector3 mousePositon = Input.mousePosition + offsetMousePosition;        
        rect.position = mousePositon;

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
            this.ClearSlotMouseUI();
            
        } 
        else if (slot.ItemData != null && this.slot.ItemData == null) //Caso o slot do botão tenha algo e Caso o slot do mouse esteja vazio
        {

            this.slot.UpdateSlot(slot.ItemData, slot.Amount);
            UpdateSlotMouseUI();
            slot.ClearSlot();
            
            
        }
        

    }
    

    public void ClearSlotMouseUI()
    {
        slot.ClearSlot();
        iconSprite.sprite = null;
        iconSprite.color = Color.clear;
        countText.text = "";
    }
    public void UpdateSlotMouseUI()
    {
        iconSprite.sprite = slot.ItemData.Icon;
        iconSprite.color = Color.white;
        countText.text = slot.Amount.ToString();

    }

}
