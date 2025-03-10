using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private CameraPositionExcel _cameraPositionDatabase;
    private List<CameraPositionDatabase> _cameraPositionDatabaseCopy;
    [SerializeField] private Transform _cameraObject;
    [SerializeField] private int _cameraId;
    [SerializeField] private CameraMoveRecorder _cameraMoveRecorder;

    void Awake()
    {
        _cameraPositionDatabaseCopy = _cameraPositionDatabase.cameraDataSheet;
    }

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
        // Excelからカメラポジションと角度を取得
        Vector3 _movePosition = new Vector3(_cameraPositionDatabaseCopy[_cameraId].posX, _cameraPositionDatabaseCopy[_cameraId].posY, _cameraPositionDatabaseCopy[_cameraId].posZ);
        Vector3 _moveRotate = new Vector3(_cameraPositionDatabaseCopy[_cameraId].rotX, _cameraPositionDatabaseCopy[_cameraId].rotY, _cameraPositionDatabaseCopy[_cameraId].rotZ);
        _cameraObject.position = _movePosition;
        _cameraObject.rotation = Quaternion.Euler(_moveRotate);
    }
}
