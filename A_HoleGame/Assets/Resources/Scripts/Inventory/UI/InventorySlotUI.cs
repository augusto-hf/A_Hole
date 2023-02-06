using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private Image iconSprite;
    [SerializeField] private TextMeshProUGUI countText;
    private Button button;
    private InventorySlot slot;

    public static Action<InventorySlotUI,InventorySlot> OnClickSlot;

    private void Start()
    {
        slot = new InventorySlot();
        ClearSlotUI();
        button = GetComponent<Button>();
        button.onClick.AddListener(() => ClickSlot());
    }

    public void ClearSlotUI()
    {
        slot.ClearSlot();
        iconSprite.sprite = null;
        iconSprite.color = Color.clear;
        countText.text = "";
    }
    public void UpdateSlotUI(InventorySlot inv)
    {
        slot.UpdateSlot(inv.ItemData, inv.Amount);
        iconSprite.sprite = inv.ItemData.Icon;
        iconSprite.color = Color.white;
        countText.text = inv.Amount.ToString();

    }
    private void ClickSlot()
    {
        OnClickSlot?.Invoke(this, slot);
    }



}
