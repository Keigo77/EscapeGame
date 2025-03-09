using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private Transform _cameraObject;
    [SerializeField] private Vector3 _movePosition;
    [SerializeField] private Vector3 _moveRotate;
    
    [SerializeField] private CameraMoveRecorder _cameraMoveRecorder;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _cameraObject.position = new Vector3(-0.3f, 9.73f, 3.2f);   // 初期位置
        _cameraObject.rotation = Quaternion.Euler(0, -180, 0);
        _cameraMoveRecorder.FourInitialPosition();
    }

    public void OnPointerClicked()
    {
        _cameraMoveRecorder.PositionUpdate(_cameraObject.position, _cameraObject.rotation.eulerAngles);
        _cameraObject.position = _movePosition;
        _cameraObject.rotation = Quaternion.Euler(_moveRotate);
    }
}
