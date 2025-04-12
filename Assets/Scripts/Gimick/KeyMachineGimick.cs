using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;


// ToDo:コイン投入の音．コインの枚数の保存．ロード時にコインの枚数をロードし，枚数分MoveGimickを作動させる
public class KeyMachineGimick : MonoBehaviour, IMoveGimick
{
    [SerializeField] private SelectingItem _selectingItem;
    [SerializeField] private GameObject _boxCoverPivot;
    [SerializeField] private GameObject _glassCover;
    private int _coinCounter = 0;
    private ShowTextMessage[] _showTextMessages;
    private CancellationToken _token;

    void Awake()
    {
        _showTextMessages = this.GetComponents<ShowTextMessage>();
        _token = this.GetCancellationTokenOnDestroy();
    }

    /// <summary>
    /// コインを装備中にマシンをクリックしたら実行
    /// </summary>
    public void MoveGimick()
    {
        if (_selectingItem.SelectingItemID.Value != 9 && _selectingItem.SelectingItemID.Value != 13 && _selectingItem.SelectingItemID.Value != 14) { return; }  // ID9のアイテムはコイン
        _coinCounter++;
        MoveMachine().Forget();
    }
    
    private async UniTask MoveMachine()
    {
        switch (_coinCounter)
        {
            case 1:
                _boxCoverPivot.transform.DOLocalRotate(new Vector3(0, 0, 0), 1.0f);
                break;
            case 3:
                _glassCover.transform.DOMoveY(-3.0f, 1.0f);
                break;
        }
        _selectingItem.UseItem(_selectingItem.SelectingItemID.Value);
        await UniTask.Delay(TimeSpan.FromSeconds(0.2f), cancellationToken: _token);
        _showTextMessages[_coinCounter - 1].ShowText().Forget();
    }
}
