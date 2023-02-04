using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemState
{
    Material,
    Final
}

[CreateAssetMenu(menuName = "item/testObject", fileName = "testObject")]
public class ObjectItem : ScriptableObject
{
    [SerializeField] private Sprite iconSprite;
    [SerializeField] private Sprite worldIconSprite;
    [SerializeField] private string nameItem;
    [SerializeField] [TextArea(2, 3)] private string descriptionItem;
    [SerializeField] private ItemState itemState;

    #region Get and Set
    
    public Sprite Icon { get => iconSprite; }
    public Sprite WorldIcon { get => worldIconSprite; }
    public string Name { get => nameItem; }
    public string Description { get => descriptionItem; }
    public ItemState State { get => itemState; }

    #endregion

}
