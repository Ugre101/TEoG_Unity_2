using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Items", menuName = "Items")]
[System.Serializable]
public class Items : ScriptableObject
{
        public List<Item> items;   
}
public enum ItemRefs
{
    Item,
    TestPotion
}
