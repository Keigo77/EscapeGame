using System.Collections.Generic;
using Unity.Mathematics.Geometry;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UnityEngine.Serialization;

public class CameraMoveRecorder : MonoBehaviour
{
    [SerializeField] private Transform _cameraPosition;
    [SerializeField] private GameObject _undoButton;
    [SerializeField] private GameObject _leftButton;
    [SerializeField] private GameObject _rightButton;
    [SerializeField] private UIManager _uiManager;
    public Stack<Vector3> MovePosisionsHistory { get; } = new();
    public Stack<Vector3> MoveRotatesHistroy { get; } = new();

    public void PositionUpdate(Vector3 cameraPosition, Vector3 cameraRotate)  // カメラが動くときに，動く前の位置を保存する
    {
        MovePosisionsHistory.Push(cameraPosition);
        MoveRotatesHistroy.Push(cameraRotate);
        IsShowUIButton();
    }

    public void UndoCameraPosition()
    {
        if (ShowTextMessage.IsShowText) { return; }
        _cameraPosition.position = MovePosisionsHistory.Pop();
        _cameraPosition.rotation = Quaternion.Euler(MoveRotatesHistroy.Pop());
        if (MovePosisionsHistory.Count <= 0) { FourInitialPosition(); }
        IsShowUIButton();
    }

    public void FourInitialPosition()
    {
        MovePosisionsHistory.Push(_cameraPosition.position);
        MoveRotatesHistroy.Push(_cameraPosition.rotation.eulerAngles);
    }

    public void IsShowUIButton()
    {   
        if (!_uiManager.IsShowUI) { return; }
        if (MovePosisionsHistory.Count >= 2)
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
