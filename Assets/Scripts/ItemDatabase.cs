using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Scriptable Objects/ItemDatabase")]
public class ItemDatabase : ScriptableObject
{
    public List<ItemData> itemDatas = new List<ItemData>();
}

[System.Serializable]
public class ItemData
{
    public Sprite itemHalfImage;
    public RenderTexture renderTexture;
    public int itemID;
    public string itemExplain;
}
