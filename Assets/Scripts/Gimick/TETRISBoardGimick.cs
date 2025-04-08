using System;
using UnityEngine;
// Todo:テトリスをはめるときの音．コインが落ちる音．各オブジェクトのSetativeの保存
public class TETRISBoardGimick : MonoBehaviour, IMoveGimick
{
    [SerializeField] private SelectingItem _selectingItem;
    [SerializeField] private GameObject _purpleTETRIS;
    [SerializeField] private GameObject _blueTETRIS;
    [SerializeField] private GameObject _coinObj;
    private ShowTextMessage _showTextMessage;

    private void Awake()
    {
        _showTextMessage = this.GetComponent<ShowTextMessage>();
    }

    /// <summary>
    /// テトリス
    /// </summary>
    public void MoveGimick()
    {
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

        if (_purpleTETRIS.activeSelf && _blueTETRIS.activeSelf)
        {
            _coinObj.SetActive(true);
            _showTextMessage.ShowText();
        }
    }
}
