using System;
using System.Collections.Generic;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ShowGotItem : MonoBehaviour
{
    [SerializeField] private ItemDatabase itemDatabase;
    private List<ItemData> _itemDatabaseCopy;
    
    [SerializeField] private SelectingItem selectingItem;
    [SerializeField] private List<ItemData> gotItemDatas = new List<ItemData>();
    [SerializeField] private List<Image> itemImages = new List<Image>();

    [SerializeField] private GameObject showItemPanel;
    [SerializeField] private Button showItemButton;

    [SerializeField] private ObjectRotate objectRotate;
    private ObjSetActiveManager _objSetActiveManager;

    void Awake()
    {
        _itemDatabaseCopy = itemDatabase.itemDatas;
        _objSetActiveManager = this.GetComponent<ObjSetActiveManager>();
    }
    
    /// <summary>
    /// 取得可能なアイテムをクリック時に実行．アイテムをゲットし，すぐアイテム欄を表示．シーン中のアイテムは削除する．
    /// </summary>
    /// <param name="itemID">取得したいアイテムのアイテムID</param>
    public void GetItem(int itemID)
    {
        gotItemDatas.Add(_itemDatabaseCopy[itemID]);
        UpdateItemList();
        selectingItem.SelectingItemID.Value = _itemDatabaseCopy[itemID].itemID;
        ShowItemPanel();
        _objSetActiveManager.ObjSetActiveOff(itemID);
    }
    
    public void RemoveItem(int removeItemID)
    {
        foreach (var gotItem in gotItemDatas)
        {
            if (gotItem.itemID == removeItemID)
            {
                gotItemDatas.Remove(gotItem);
                selectingItem.SelectingItemID.Value = -1;
                break;
            }
        }
        UpdateItemList();
    }

    private void UpdateItemList()
    {
        int index = 0;
        foreach (var gotItem in gotItemDatas)
        {
            itemImages[index].sprite = gotItem.itemHalfImage;
            itemImages[index + 1].sprite = null;
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
        if (gotItemDatas.Count <= index)
        {
            selectingItem.SelectingItemID.Value = -1;
            return;
        }
        selectingItem.SelectingItemID.Value = gotItemDatas[index].itemID;   // 装備中アイテムのアイテムIDを更新
    }
    
    /// <summary>
    /// 「アイテム」　ボタンで実行．アイテム欄の表示，非表示．非表示時に全アイテムの角度をリセット
    /// </summary>
    public void ShowItemPanel()
    {
        showItemPanel.SetActive(!showItemPanel.activeSelf);
    }
}
