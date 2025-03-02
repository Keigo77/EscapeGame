using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Scriptable Objects/ItemDatabase")]
public class ItemDatabase : ScriptableObject
{
    public List<ItemData> itemDatas = new List<ItemData>();
}

[System.Serializable]
public class ItemData
{
    [SerializeField] private GameObject _itemObject;
    [SerializeField] private string _itemID;
    [SerializeField] private string _itemName;
    [SerializeField] private string _itemExplain;
}
