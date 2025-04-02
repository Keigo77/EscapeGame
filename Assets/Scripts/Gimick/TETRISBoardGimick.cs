using System;
using UnityEngine;

public class TETRISBoardGimick : MonoBehaviour
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

    public void SetTETRIS()
    {
        if (_selectingItem.selectingItemID.Value == 0) _purpleTETRIS.SetActive(true);
        else if (_selectingItem.selectingItemID.Value == 7) _blueTETRIS.SetActive(true);

        if (_purpleTETRIS.activeSelf && _blueTETRIS.activeSelf)
        {
            _coinObj.SetActive(true);
            _showTextMessage.ShowText();
        }
    }
}
