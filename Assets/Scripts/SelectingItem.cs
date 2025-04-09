using TMPro;
using UniRx;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SelectingItem : MonoBehaviour
{
    [SerializeField] private ItemDatabase _itemDatabase;
    private List<ItemData> _itemDatabaseCopy;
    [SerializeField] private GameObject _itemRawImageObj;
    private RawImage _itemRawImage;
    [SerializeField] private GameObject _gearItemRawImageObj;
    private RawImage _gearRawImage;
    [SerializeField] private RawImage _gearItemRawImage;
    [SerializeField] private TextMeshProUGUI _explainText;
    public ReactiveProperty<int> SelectingItemID = new (-1);
    
    [SerializeField] private ShowGotItem _showGotItem;
    
    void Awake()
    {
        _itemDatabaseCopy = _itemDatabase.itemDatas;
        _itemRawImage = _itemRawImageObj.GetComponent<RawImage>();
        _gearRawImage = _gearItemRawImageObj.GetComponent<RawImage>();
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SelectingItemID.Subscribe(_ => UpdateShowItemDetail());
        _gearItemRawImageObj.SetActive(false);
    }

    /// <summary>
    /// 何もアイテムを装備していない状態なら，アイテムの画像と説明文を削除．選択状態なら表示
    /// </summary>
    private void UpdateShowItemDetail()
    {
        if (SelectingItemID.Value < 0)      
        {
            _itemRawImageObj.SetActive(false);
            _gearItemRawImageObj.SetActive(false);
            _explainText.text = "";
            return;
        }
        _itemRawImageObj.SetActive(true);
        if (!_showGotItem.ItemPanel.activeSelf) { _gearItemRawImageObj.SetActive(true); }
        _itemRawImage.texture = _itemDatabaseCopy[SelectingItemID.Value].renderTexture;
        _gearItemRawImage.texture = _itemDatabaseCopy[SelectingItemID.Value].renderTexture;
        _explainText.text = _itemDatabaseCopy[SelectingItemID.Value].itemExplain;
    }

    public void UseItem(int usedItemID)
    {
        _showGotItem.RemoveItem(usedItemID);
    }
}
