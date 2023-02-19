using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( menuName = "Item/Recipe", fileName = "New Recipe Item")]
public class ItemRecipe : ScriptableObject
{
    [SerializeField] private List<ItemData> input = new List<ItemData>(3);

    [SerializeField] private ItemData output;


    public List<ItemData> Input { get => input; }
    public ItemData Output { get => output;}
    
}
