using TMPro;
using UniRx;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectingItem : MonoBehaviour
{
    [SerializeField] private ItemDatabase _itemDatabase;
    private List<ItemData> _itemDatabaseCopy;
    [SerializeField] private RawImage _rawImage;
    [SerializeField] private TextMeshProUGUI _explainText;
    public ReactiveProperty<int> selectingItemID = new ReactiveProperty<int>(-1);
    
    
    void Awake()
    {
        _itemDatabaseCopy = _itemDatabase.itemDatas;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        selectingItemID.Subscribe(_ => UpdateShowItemDetail());
        _rawImage.enabled = false;
    }

    private void UpdateShowItemDetail()
    {
        if(selectingItemID.Value < 0) return;
        _rawImage.enabled = true;
        _rawImage.texture = _itemDatabaseCopy[selectingItemID.Value].renderTexture;
        _explainText.text = _itemDatabaseCopy[selectingItemID.Value].itemExplain;
    }
}
