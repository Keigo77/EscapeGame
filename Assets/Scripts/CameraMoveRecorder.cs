using System.Collections.Generic;
using Unity.Mathematics.Geometry;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class CameraMoveRecorder : MonoBehaviour
{
    [SerializeField] private Transform _cameraPosition;
    [SerializeField] private Button _undoButton;
    Stack<Vector3> _movePosisionsHistory = new Stack<Vector3>();
    Stack<Vector3> _moveRotatesHistroy = new Stack<Vector3>();

    public void PositionUpdate(Vector3 cameraPosition, Vector3 cameraRotate)  // カメラが動くときに，動く前の位置を保存する
    {
        _movePosisionsHistory.Push(cameraPosition);
        _moveRotatesHistroy.Push(cameraRotate);
        isShowUndoButton();
    }

    public void UndoCameraPosition()
    {
        _cameraPosition.position = _movePosisionsHistory.Pop();
        _cameraPosition.rotation = Quaternion.Euler(_moveRotatesHistroy.Pop());
        if (_movePosisionsHistory.Count <= 0) FourInitialPosition();
    }

    public void FourInitialPosition()
    {
        _movePosisionsHistory.Push(_cameraPosition.position);
        _moveRotatesHistroy.Push(_cameraPosition.rotation.eulerAngles);
    }

    private void isShowUndoButton()
    {
        if (_movePosisionsHistory.Count >= 1) _undoButton.gameObject.SetActive(true);
        else _undoButton.gameObject.SetActive(false);
    }
}
