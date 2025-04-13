using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

// ToDo:ライトをつけるカチッっていう音
public class ShowItemPrice : MonoBehaviour, IMoveGimmick
{
    [SerializeField] private GameObject _itemPriceUsedRight;
    [SerializeField] private SelectingItem _selectingItem;
    private ShowTextMessage[] _showTextMessages;
    private CancellationToken _token;

    void Awake()
    {
        _token = this.GetCancellationTokenOnDestroy();
        _showTextMessages = this.GetComponents<ShowTextMessage>();
    }

    public void MoveGimmick()
    {
        MoveGimmickAsync().Forget();
    }
    
    // クリックしたらライトを消去
    private async UniTask MoveGimmickAsync()
    {
        if (_itemPriceUsedRight.activeSelf) return;     // 値段が表示中なら処理しない．値段表示中にテキストが出てしまうため．
        if (_selectingItem.SelectingItemID.Value != 12) {
            _showTextMessages[0].ShowText().Forget();
            return;     // ItemID 12はライト
        }
        _itemPriceUsedRight.SetActive(true);
        await UniTask.Delay(TimeSpan.FromSeconds(0.2f), cancellationToken: _token);
        _showTextMessages[1].ShowText().Forget();
        await UniTask.WaitUntil(() => Input.GetMouseButtonUp(0), cancellationToken: _token);
        await UniTask.WaitUntil(() => Input.GetMouseButtonUp(0), cancellationToken: _token);
        _itemPriceUsedRight.SetActive(false);
    }
}
