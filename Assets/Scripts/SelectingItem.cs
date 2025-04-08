using TMPro;
using UniRx;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

// Todo: 装備中のアイテムのハーフ画像を画面に表示したい
public class SelectingItem : MonoBehaviour
{
    [SerializeField] private ItemDatabase _itemDatabase;
    private List<ItemData> _itemDatabaseCopy;
    [SerializeField] private RawImage _rawImage;
    [SerializeField] private TextMeshProUGUI _explainText;
    public ReactiveProperty<int> SelectingItemID = new (-1);
    
    [SerializeField] private ShowGotItem _showGotItem;
    
    void Awake()
    {
        _itemDatabaseCopy = _itemDatabase.itemDatas;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SelectingItemID.Subscribe(_ => UpdateShowItemDetail());
        _rawImage.enabled = false;
    }

    /// <summary>
    /// 何もアイテムを装備していない状態なら，アイテムの画像と説明文を削除．選択状態なら表示
    /// </summary>
    private void UpdateShowItemDetail()
    {
        if (SelectingItemID.Value < 0)      
        {
            _rawImage.enabled = false;
            _explainText.text = "";
            return;
        }
        _rawImage.enabled = true;
        _rawImage.texture = _itemDatabaseCopy[SelectingItemID.Value].renderTexture;
        _explainText.text = _itemDatabaseCopy[SelectingItemID.Value].itemExplain;
    }

    public void UseItem(int usedItemID)
    {
        _showGotItem.RemoveItem(usedItemID);
    }
}
