using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class HeartBoxGimmick : MonoBehaviour, IMoveGimmick
{
    [SerializeField] private int _needItemId;
    [SerializeField] private SelectingItem _selectingItem;
    [SerializeField] private GameObject _purpleHeart;
    [SerializeField] private GameObject _coin;
    private ShowTextMessage[] _showTextMessages; 
    private BoxCollider _boxCollider;
    private bool _isSolved = false;
    private CancellationToken _token;
    [SerializeField] private AudioClip _putHeartSe;
    [SerializeField] private AudioClip _coinSe;
    [SerializeField] private AudioClip _solveSe;

    void Awake()
    {
        _token = this.GetCancellationTokenOnDestroy();
        _showTextMessages = this.GetComponents<ShowTextMessage>();  // [0]に鍵ないとき用メッセ．[1]に鍵あるとき用メッセ．
        _boxCollider = this.GetComponent<BoxCollider>();
    }

    public void MoveGimmick()
    {
        MoveGimmickAsync().Forget();
    }
    
    private async UniTask MoveGimmickAsync()
    {
        if (_isSolved) { return; }
        if (_selectingItem.SelectingItemID.Value != _needItemId){
            _showTextMessages[0].ShowText().Forget();
            return;
        }
        _isSolved = true;
        _coin.SetActive(true);
        Correct();
        SEManager.PlaySe(_putHeartSe);
        _selectingItem.UseItem(_selectingItem.SelectingItemID.Value);
        SEManager.PlaySe(_coinSe);
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: _token);
        _showTextMessages[1].ShowText().Forget();
        SEManager.PlaySe(_solveSe);
    }

    private void Correct()
    {
        _boxCollider.enabled = false;
        _purpleHeart.SetActive(true);
    }
}
