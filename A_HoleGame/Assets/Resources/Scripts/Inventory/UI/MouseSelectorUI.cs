using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MouseSelectorUI : MonoBehaviour
{
    [SerializeField] private Image iconSprite;
    [SerializeField] private TextMeshProUGUI countText;

    private InventorySlot slot;
    private Camera cam;

    private void Awake()
    {

        slot = new InventorySlot();
        ClearSlotUI();
        
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

    public void GrabItem(InventorySlot inv)
    {
        
    }

    public void ClearSlotUI()
    {
        iconSprite.sprite = null;
        iconSprite.color = Color.clear;
        countText.text = "";
    }
    public void UpdateSlotUI()
    {
        iconSprite.sprite = slot.ItemData.Icon;
        iconSprite.color = Color.white;
        countText.text = slot.ToString();

    }

}
