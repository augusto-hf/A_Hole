using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private Image iconSprite;
    [SerializeField] private TextMeshProUGUI countText;

    private void Awake()
    {
        ClearSlotUI();   
    }

    public void ClearSlotUI()
    {
        iconSprite.sprite = null;
        iconSprite.color = Color.clear;
        countText.text = "";
    }

    public void UpdateSlotUI(ItemData itemData, int amount)
    {
        iconSprite.sprite = itemData.Icon;
        iconSprite.color = Color.white;
        countText.text = amount.ToString();

    }

}
