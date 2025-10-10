using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class RedClockGimmick : MonoBehaviour, IMoveGimmick
{
    [SerializeField] private int _needItemId;
    [SerializeField] private SelectingItem _selectingItem;
    [SerializeField] private GameObject _redClockSprite;
    [SerializeField] private GameObject _undoButton;
    private ShowTextMessage[] _showTextMessages;
    private CancellationToken _token;

    void Awake()
    {
        _showTextMessages = this.GetComponents<ShowTextMessage>();
        _token = this.GetCancellationTokenOnDestroy();
    }

    /// <summary>
    /// 虫眼鏡を持っていなければ，専用テキストを表示して終了．持っていたら，虫眼鏡を使ったことがわかるテキストを表示．
    /// </summary>
    public void MoveGimmick()
    {
        if (_selectingItem.SelectingItemID.Value != _needItemId){
            _showTextMessages[0].ShowText().Forget();
            return;
        }
        _showTextMessages[1].ShowText().Forget();
        _undoButton.SetActive(false);
        MoveGimmickAsync().Forget();
    }
    

    /// <summary>
    /// 赤い時計を大きく表示．テキストボックスを消すためのクリックをした後，もう一回クリックしたら赤い時計を非表示にする
    /// </summary>
    private async UniTask MoveGimmickAsync()
    {
        _redClockSprite.SetActive(true);
        await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0), cancellationToken: _token);
        await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0), cancellationToken: _token);
        // Upを待たないと，赤い時計が消えると同時に青い時計のテキストが表示されてしまう．
        await UniTask.WaitUntil(() => Input.GetMouseButtonUp(0), cancellationToken: _token);
        _redClockSprite.SetActive(false);
        _undoButton.SetActive(true);
    }
}
