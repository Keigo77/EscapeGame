using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowHint : MonoBehaviour
{
    [SerializeField] private Button _hintButton;
    [SerializeField] private MoveCamera _moveCamera;
    [SerializeField] private CameraPositionExcel _cameraPositionExcel;
    [SerializeField] private ShowGotItem _showGotItem;
    [SerializeField] private TextMeshProUGUI _hintCountText;
    [SerializeField] private GameObject _hintTutorial;
    private ShowTextMessage _showTextMessage;
    private int _showHintCount = 0;
    private int _beforeCameraId = -100;

    private void Start()
    {
        _showTextMessage = this.GetComponent<ShowTextMessage>();
        
        if (!ES3.KeyExists("IsShowedHintTutorial"))
        {
            _hintTutorial.SetActive(true);
            ES3.Save<bool>("IsShowedHintTutorial", true);
        }
    }

    public void HintButtonClicked()
    {
        int nowCameraId = _moveCamera.CameraId.Value;
        
        if (_beforeCameraId != nowCameraId)
        {
            _showHintCount = 0;
        }
        
        if (_showHintCount == 0)
        {
            _showTextMessage.SetHintText(_cameraPositionExcel.cameraDataSheet[nowCameraId].FirstHint);
        } 
        else if (_showHintCount >= 1)
        {
            _showTextMessage.SetHintText(_cameraPositionExcel.cameraDataSheet[nowCameraId].SecondHint);
        }

        _showHintCount++;
        _hintCountText.text = Math.Min(_showHintCount + 1, 2).ToString();
        _beforeCameraId = nowCameraId;
        _showTextMessage.ShowExplainText();
        CheckHasShowHintItem();
    }

    /// <summary>
    /// 今のカメラ視点に，ヒントを表示できるギミックが映っているか判定．映っているなら，ヒントボタンを表示する．
    /// </summary>
    public void CheckShowHintButton()
    {
        int nowCameraId = _moveCamera.CameraId.Value;
        
        _showHintCount = 0;
        _hintCountText.text = "1";
        CheckHasShowHintItem();
        
        if (_cameraPositionExcel.cameraDataSheet[nowCameraId].FirstHint.Length > 0)
        {
            _hintButton.gameObject.SetActive(true);
        }
        else
        {
            _hintButton.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// ヒント表示のために必要なアイテムを所持しているか判定．所持していなければ，interactableをfalseにする．
    /// </summary>
    private void CheckHasShowHintItem()
    {
        int nowCameraId = _moveCamera.CameraId.Value;
        
        if (_showHintCount == 0 &&
            (_showGotItem.GetContainItemId(_cameraPositionExcel.cameraDataSheet[nowCameraId].ShowFirstHintNeedItemId) 
            || _cameraPositionExcel.cameraDataSheet[nowCameraId].ShowFirstHintNeedItemId == -100))
        {
            _hintButton.interactable = true;
        } 
        else if (_showHintCount >= 1 &&
                   (_showGotItem.GetContainItemId(_cameraPositionExcel.cameraDataSheet[nowCameraId].ShowSecondHintNeedItemId) 
                    || _cameraPositionExcel.cameraDataSheet[nowCameraId].ShowSecondHintNeedItemId == -100))
        {
            _hintButton.interactable = true;
        } 
        else
        {
            _hintButton.interactable = false;
        }
    }
}
