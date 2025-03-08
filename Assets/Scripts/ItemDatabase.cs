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
    public Image _itemHalfImage;
    public bool _isHaveObject;
    public Image _itemImage;
    public GameObject _itemObject;
    public int _itemID;
    public string _itemExplain;
}
