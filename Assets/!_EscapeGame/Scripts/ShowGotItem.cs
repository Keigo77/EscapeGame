using System;
using System.Collections.Generic;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ShowGotItem : MonoBehaviour
{
    [SerializeField] private ItemDatabase _itemDatabase;
    private List<ItemData> _itemDatabaseCopy;
    
    [SerializeField] private SelectingItem _selectingItem;
    private List<ItemData> _gotItemDatas = new();
    [SerializeField] private List<Image> _itemImages = new();
#region //UI系統
    [SerializeField] private GameObject _showItemPanel;
    public GameObject ItemPanel => _showItemPanel;
    [SerializeField] private GameObject _showItemPanelButtonObj;
    private Button _showItemButton;
    [SerializeField] private GameObject _rightButton;
    [SerializeField] private GameObject _leftButton;
    [SerializeField] private GameObject _gearItemRawImageObj;
    [SerializeField] private UIManager _uiManager;
#endregion
    [SerializeField] private ObjectRotate _objectRotate;
    private ObjSetActiveManager _objSetActiveManager;
    [SerializeField] private AudioClip _itemGetSe;
    [SerializeField] private AudioClip _noItemClickSe;
    [SerializeField] private AudioClip _showItemPanelSe;
    [SerializeField] private AudioClip _deleteItemPanelSe;
    

    void Awake()
    {
        _itemDatabaseCopy = _itemDatabase.itemDatas;
        _objSetActiveManager = this.GetComponent<ObjSetActiveManager>();
        _showItemButton = _showItemPanelButtonObj.GetComponent<Button>();
    }
    
    /// <summary>
    /// 取得可能なアイテムをクリック時に実行．アイテムをゲットし，すぐアイテム欄を表示．シーン中のアイテムは削除する．
    /// </summary>
    /// <param name="itemID">取得したいアイテムのアイテムID</param>
    public void GetItem(int itemID)
    {
        SEManager.PlaySe(_itemGetSe);
        _gotItemDatas.Add(_itemDatabaseCopy[itemID]);
        UpdateItemList();
        _selectingItem.SelectingItemID.Value = _itemDatabaseCopy[itemID].itemID;
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
                _selectingItem.SelectingItemID.Value = -1;
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
            SEManager.PlaySe(_noItemClickSe);
            _selectingItem.SelectingItemID.Value = -1;
            return;
        }
        _selectingItem.SelectingItemID.Value = _gotItemDatas[index].itemID;   // 装備中アイテムのアイテムIDを更新
    }
    
    /// <summary>
    /// 「アイテム」　ボタンで実行．アイテム欄の表示，非表示．非表示時に全アイテムの角度をリセット
    /// </summary>
    public void ShowItemPanel()
    {
        _objectRotate.ResetRotate();
        _showItemPanel.SetActive(!_showItemPanel.activeSelf);
        if (_showItemPanel.activeSelf) { SEManager.PlaySe(_showItemPanelSe); }
        else { SEManager.PlaySe(_deleteItemPanelSe); }
        if (_showItemPanel.activeSelf)
        {
            _uiManager.DontShowUI();
            _showItemPanelButtonObj.SetActive(true);
            _showItemButton.image.color = new Color(0.25f, 0.28f, 0.64f, 1);
        }
        else
        {
            _uiManager.ShowUI();
            _showItemButton.image.color = new Color(0.41f, 0.45f, 1, 1);
        }
    }
}
