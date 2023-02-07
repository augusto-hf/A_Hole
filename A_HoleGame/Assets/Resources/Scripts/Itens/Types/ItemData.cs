using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Item/TestObject", fileName = "ItemObject")]
public class ItemData : ScriptableObject
{
    [SerializeField] private Sprite iconSprite;
    [SerializeField] private Sprite worldIconSprite;
    [SerializeField] private string nameItem;
    [SerializeField] [TextArea(2, 3)] private string descriptionItem;

    #region Get and Set
    
    public Sprite Icon { get => iconSprite; }
    public Sprite WorldIcon { get => worldIconSprite; }
    public string Name { get => nameItem; }
    public string Description { get => descriptionItem; }

    #endregion

}
