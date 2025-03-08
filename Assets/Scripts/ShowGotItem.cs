using System;
using System.Collections.Generic;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShowGotItem : MonoBehaviour
{
    [SerializeField] private ScriptableObject _itemDatasScriptableObject;
    private List<ItemData> _itemDatas = new List<ItemData>();
    
    [SerializeField] private List<ItemData> _gotItemDatas = new List<ItemData>();
    [SerializeField] private List<Image> _itemImages = new List<Image>();

    public static int selectingItemID = -1;

    private void Awake()
    {
        _itemDatas = _itemDatasScriptableObject.GetComponent<ItemDatabase>().itemDatas;
    }

    public void GetItem(int itemID)
    {
        _gotItemDatas.Add(_itemDatas[itemID]);
        UpdateItemList();
    }
    
    public void RemoveItem(int RemoveItemID)
    {
        foreach (var gotItem in _gotItemDatas)
        {
            if (gotItem._itemID == RemoveItemID)
            {
                _gotItemDatas.Remove(gotItem);
                break;
            }
        }
        UpdateItemList();
    }

    private void UpdateItemList()
    {
        int index = 0;
        foreach (var gotItem in _gotItemDatas)
        {
            _itemImages[index] = gotItem._itemImage;
        }
    }

    public void ChangeSelectItem(int index)     // アイテムのボックスをクリックするときに実行
    {
        // アイテムの写真，説明文を変更
        selectingItemID = _gotItemDatas[index]._itemID;   // 装備中アイテムのアイテムIDを更新
    }
}
