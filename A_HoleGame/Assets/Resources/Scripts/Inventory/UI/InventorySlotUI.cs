using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private Image iconSprite;
    [SerializeField] private TextMeshProUGUI countText;
    private Button button;
    private InventorySlot slot;

    public static Action<InventorySlotUI,InventorySlot> OnClickSlot;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => ClickSlot());
    }

    public void SetSlot(InventorySlot slot)
    {
        this.slot = slot;
    }

    public void ClearSlotUI()
    {
        iconSprite.sprite = null;
        iconSprite.color = Color.clear;
        countText.text = "";
    }
    public void UpdateSlotUI(ItemData itemData, int amount)
    {
        if ( itemData ==  null)
        {
            ClearSlotUI();
            return;
        }
        iconSprite.sprite = itemData.Icon;
        iconSprite.color = Color.white;
        countText.text = amount.ToString();

    }
    private void ClickSlot()
    {
        OnClickSlot?.Invoke(this, slot);
    }



}
