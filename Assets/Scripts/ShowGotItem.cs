using System;
using System.Collections.Generic;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShowGotItem : MonoBehaviour
{
    [SerializeField] private ItemDatabase _itemDatabase;
    private List<ItemData> _itemDatabaseCopy;
    
    [SerializeField] private SelectingItem _selectingItem;
    [SerializeField] private List<ItemData> _gotItemDatas = new List<ItemData>();
    [SerializeField] private List<Image> _itemImages = new List<Image>();

    [SerializeField] private GameObject _showItemPanel;
    [SerializeField] private Button _showItemButton;

    [SerializeField] private ObjectRotate _objectRotate;
    private ObjSetActiveManager _objSetActiveManager;

    void Awake()
    {
        _itemDatabaseCopy = _itemDatabase.itemDatas;
        _objSetActiveManager = this.GetComponent<ObjSetActiveManager>();
    }
    
    /// <summary>
    /// 取得可能なアイテムをクリック時に実行．アイテムをゲットし，すぐアイテム欄を表示．シーン中のアイテムは削除する．
    /// </summary>
    /// <param name="itemID">取得したいアイテムのアイテムID</param>
    public void GetItem(int itemID)
    {
        _gotItemDatas.Add(_itemDatabaseCopy[itemID]);
        UpdateItemList();
        _selectingItem.selectingItemID.Value = _itemDatabaseCopy[itemID].itemID;
        ShowItemPanel();
        _objSetActiveManager.ObjSetActiveOff(itemID);
    }
    
    public void RemoveItem(int removeItemID)
    {
        foreach (var gotItem in _gotItemDatas)
        {
            if (gotItem.itemID == removeItemID)
            {
                _gotItemDatas.Remove(gotItem);
                _selectingItem.selectingItemID.Value = -1;
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
            _itemImages[index + 1].sprite = null;
            index++;
        }
    }

    /// <summary>
    /// アイテムのボックスをクリックするときに実行．アイテムの写真，説明文を変更
    /// アイテムがないところをクリックしたら処理しない．
    /// </summary>
    /// <param name="index">どこのアイテム枠をクリックしたか</param>
    public void ChangeSelectItem(int index)     
    {
        if (_gotItemDatas.Count <= index)
        {
            _selectingItem.selectingItemID.Value = -1;
            return;
        }
        _selectingItem.selectingItemID.Value = _gotItemDatas[index].itemID;   // 装備中アイテムのアイテムIDを更新
    }
    
    /// <summary>
    /// 「アイテム」　ボタンで実行．アイテム欄の表示，非表示．非表示時に全アイテムの角度をリセット
    /// </summary>
    public void ShowItemPanel()
    {
        _showItemPanel.SetActive(!_showItemPanel.activeSelf);
        _objectRotate.ResetRotate();
    }
}
