using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
// Todo:テトリスをはめるときの音．コインが落ちる音．各オブジェクトのSetativeの保存
public class TETRISBoardGimick : MonoBehaviour, IMoveGimmick
{
    [SerializeField] private SelectingItem _selectingItem;
    [SerializeField] private GameObject _purpleTETRIS;
    [SerializeField] private GameObject _blueTETRIS;
    [SerializeField] private GameObject _coinObj;
    private ShowTextMessage[] _showTextMessages;
    private bool _isSolved = false;

    private void Awake()
    {
        _showTextMessages = this.GetComponents<ShowTextMessage>();
    }
    
    public void MoveGimmick()
    {
        if (_isSolved) { return; }
        if (_selectingItem.SelectingItemID.Value == 0)
        {
            _purpleTETRIS.SetActive(true);
            _selectingItem.UseItem(_selectingItem.SelectingItemID.Value);
        }
        else if (_selectingItem.SelectingItemID.Value == 7)
        {
            _blueTETRIS.SetActive(true);
            _selectingItem.UseItem(_selectingItem.SelectingItemID.Value);
        }
        else { _showTextMessages[0].ShowText().Forget(); }

        if (_purpleTETRIS.activeSelf && _blueTETRIS.activeSelf)
        {
            _coinObj.SetActive(true);
            _showTextMessages[1].ShowText().Forget();
            _isSolved = true;
        }
    }
}
