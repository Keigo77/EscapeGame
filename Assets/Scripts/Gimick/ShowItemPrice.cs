using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
// ToDo:ライトをつけるカチッっていう音
public class ShowItemPrice : MonoBehaviour, IMoveGimick
{
    [SerializeField] private GameObject _ItemPriceUsedRight;
    [SerializeField] private SelectingItem _selectingItem;
    private ShowTextMessage _showTextMessage;
    private CancellationToken _token;

    void Awake()
    {
        _token = this.GetCancellationTokenOnDestroy();
        _showTextMessage = this.GetComponent<ShowTextMessage>();
    }
    
    // クリックしたらライトを消去
    public async void MoveGimick()
    {
        if (_selectingItem.SelectingItemID.Value != 12) {
            _showTextMessage.ShowText();
            return;     // ItemID 12はライト
        }
        _ItemPriceUsedRight.SetActive(true);
        try
        {
            await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0), cancellationToken: _token);
        }
        catch (OperationCanceledException)
        {
            Debug.Log("UniTaskキャンセル");
        }
        _ItemPriceUsedRight.SetActive(false);
    }
}
