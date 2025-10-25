using System.Collections.Generic;
using Unity.Mathematics.Geometry;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UnityEngine.Serialization;

public class CameraMoveRecorder : MonoBehaviour
{
    [SerializeField] private CameraPositionExcel _cameraPositionExcel;
    [SerializeField] private MoveCamera _moveCamera;
    [SerializeField] private Transform _cameraPosition;
    [SerializeField] private GameObject _undoButton;
    [SerializeField] private GameObject _leftButton;
    [SerializeField] private GameObject _rightButton;
    [SerializeField] private UIManager _uiManager;
    public Stack<int> CameraIdStack = new Stack<int>();

    public void RecordePosition(int cameraId)  // カメラが動くときに，動く前の位置を保存する
    {
        CameraIdStack.Push(cameraId);
        IsShowUIButton();
    }

    public void UndoCameraPosition()
    {
        if (ShowTextMessage.IsShowText) { return; }
        
        CameraIdStack.Pop();
        int cameraId = CameraIdStack.Peek();
        _moveCamera.CameraId.Value = cameraId;
        IsShowUIButton();
    }

    public void IsShowUIButton()
    {   
        if (!_uiManager.IsShowUI) { return; }
        if (CameraIdStack.Count >= 2)
        {
            _undoButton.SetActive(true);
            _rightButton.SetActive(false);
            _leftButton.SetActive(false);
        }
        else
        {
            _undoButton.SetActive(false);
            _rightButton.SetActive(true);
            _leftButton.SetActive(true);
        }
        
    }
}
