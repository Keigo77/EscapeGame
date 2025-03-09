using System;
using System.Collections.Generic;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShowGotItem : MonoBehaviour
{
    [SerializeField] private ItemDatabase _itemDatasScriptableObject;
    
    [SerializeField] private List<ItemData> _gotItemDatas = new List<ItemData>();
    [SerializeField] private List<Image> _itemImages = new List<Image>();

    [SerializeField] private GameObject _showItemPanel;
    [SerializeField] private Button _showItemButton;

    public void GetItem(int itemID)
    {
        _gotItemDatas.Add(_itemDatasScriptableObject.itemDatas[itemID]);
        UpdateItemList();
        SelectingItem.selectingItemID.Value = _itemDatasScriptableObject.itemDatas[itemID].itemID;
        ShowItemPanel();
    }
    
    public void RemoveItem(int RemoveItemID)
    {
        foreach (var gotItem in _gotItemDatas)
        {
            if (gotItem.itemID == RemoveItemID)
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
            _itemImages[index].sprite = gotItem.itemHalfImage;
            index++;
        }
    }

    public void ChangeSelectItem(int index)     // アイテムのボックスをクリックするときに実行
    {
        // アイテムの写真，説明文を変更
        if (_gotItemDatas.Count <= index) return;    // アイテムがないところをクリックしたら処理しない
        SelectingItem.selectingItemID.Value = _gotItemDatas[index].itemID;   // 装備中アイテムのアイテムIDを更新
    }

    public void ShowItemPanel()     // アイテム　ボタンで実行．アイテム欄の表示，非表示
    {
        _showItemPanel.SetActive(!_showItemPanel.activeSelf);
    }
}
