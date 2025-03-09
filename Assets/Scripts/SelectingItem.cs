using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class SelectingItem : MonoBehaviour
{
    [SerializeField] private ItemDatabase _itemDatasScriptableObject;
    [SerializeField] private RawImage _rawImage;
    [SerializeField] private TextMeshProUGUI _explainText;
    public static ReactiveProperty<int> selectingItemID = new ReactiveProperty<int>(-1);
    
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
        _rawImage.texture = _itemDatasScriptableObject.itemDatas[selectingItemID.Value].renderTexture;
        _explainText.text = _itemDatasScriptableObject.itemDatas[selectingItemID.Value].itemExplain;
    }
}
