using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class RedClockGimick : MonoBehaviour, IMoveGimick
{
    [SerializeField] private int _needItemId;
    [SerializeField] private SelectingItem _selectingItem;
    [SerializeField] private GameObject _redClockSprite;
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
    public void MoveGimick()
    {
        if (_selectingItem.SelectingItemID.Value != _needItemId){
            _showTextMessages[0].ShowText();
            return;
        }
        _showTextMessages[1].ShowText();
        MoveGimickAsync();
    }
    

    /// <summary>
    /// 赤い時計を大きく表示．テキストボックスを消すためのクリックをした後，もう一回クリックしたら赤い時計を非表示にする
    /// </summary>
    private async UniTask MoveGimickAsync()
    {
        _redClockSprite.SetActive(true);
        try
        {
            await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0));
            await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0));
        }
        catch (OperationCanceledException)
        {
            Debug.Log("UniTaskのキャンセル");
        }
        _redClockSprite.SetActive(false);
    }
}
