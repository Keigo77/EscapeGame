using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;


// ToDo:コイン投入の音．コインの枚数の保存．ロード時にコインの枚数をロードし，枚数分MoveGimickを作動させる
public class KeyMachineGimmick : MonoBehaviour, IMoveGimmick
{
    [SerializeField] private SelectingItem _selectingItem;
    [SerializeField] private GameObject _boxCoverPivot;
    [SerializeField] private BoxCollider _boxBoxCollider;
    [SerializeField] private GameObject _glassCover;
    private BoxCollider _coinHoleCollider;
    private int _coinCounter = 0;
    private ShowTextMessage[] _showTextMessages;
    private CancellationToken _token;

    void Awake()
    {
        _coinHoleCollider = this.GetComponent<BoxCollider>();
        _showTextMessages = this.GetComponents<ShowTextMessage>();
        _token = this.GetCancellationTokenOnDestroy();
    }

    /// <summary>
    /// コインを装備中にマシンをクリックしたら実行
    /// </summary>
    public void MoveGimmick()
    {
        // コインのアイテムIDは9, 13, 14
        if (_selectingItem.SelectingItemID.Value != 9 && _selectingItem.SelectingItemID.Value != 13 &&
            _selectingItem.SelectingItemID.Value != 14)
        {
            if (_coinCounter == 0) { _showTextMessages[3].ShowText().Forget(); }
            else { _showTextMessages[4].ShowText().Forget(); }
            return;
        }
        _coinCounter++;
        MoveMachine().Forget();
    }
    
    private async UniTask MoveMachine()
    {
        switch (_coinCounter)
        {
            case 1:
                _boxCoverPivot.transform.DOLocalRotate(new Vector3(0, 0, 0), 1.0f);
                _boxBoxCollider.enabled = false;
                break;
            case 3:
                _glassCover.transform.DOMoveY(-3.0f, 1.0f);
                _coinHoleCollider.enabled = false;
                break;
        }
        _selectingItem.UseItem(_selectingItem.SelectingItemID.Value);
        await UniTask.Delay(TimeSpan.FromSeconds(0.2f), cancellationToken: _token);
        _showTextMessages[_coinCounter - 1].ShowText().Forget();
    }
}
