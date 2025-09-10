using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class TETRISBoardGimick : MonoBehaviour, IMoveGimmick
{
    [SerializeField] private SelectingItem _selectingItem;
    [SerializeField] private GameObject _purpleTETRIS;
    [SerializeField] private GameObject _blueTETRIS;
    [SerializeField] private GameObject _coinObj;
    private ShowTextMessage[] _showTextMessages;
    private bool _isSolved = false;
    private CancellationToken _token;
    [SerializeField] private AudioClip _coinSe;
    [SerializeField] private AudioClip _putTETRISSe;
    [SerializeField] private AudioClip _solveSe;

    private void Awake()
    {
        _token = this.GetCancellationTokenOnDestroy();
        _showTextMessages = this.GetComponents<ShowTextMessage>();
    }

    public void MoveGimmick()
    {
        MoveGimmickAsync().Forget();
    }
    
    public async UniTask MoveGimmickAsync()
    {
        if (_isSolved) { return; }
        if (_selectingItem.SelectingItemID.Value == 0)
        {
            SEManager.PlaySe(_putTETRISSe);
            _purpleTETRIS.SetActive(true);
            _selectingItem.UseItem(_selectingItem.SelectingItemID.Value);
        }
        else if (_selectingItem.SelectingItemID.Value == 7)
        {
            SEManager.PlaySe(_putTETRISSe);
            _blueTETRIS.SetActive(true);
            _selectingItem.UseItem(_selectingItem.SelectingItemID.Value);
        }
        else { _showTextMessages[0].ShowText().Forget(); }

        if (_purpleTETRIS.activeSelf && _blueTETRIS.activeSelf)
        {
            SEManager.PlaySe(_coinSe);
            _coinObj.SetActive(true);
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: _token); // コインの音を少し待つ
            _showTextMessages[1].ShowText().Forget();
            _isSolved = true;
            SEManager.PlaySe(_solveSe);
        }
    }
}
