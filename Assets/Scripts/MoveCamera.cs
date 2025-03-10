using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private CameraPositionExcel _cameraPositionDatabase;
    private List<CameraPositionDatabase> _cameraPositionDatabaseCopy;
    [SerializeField] private Transform _mainCamera;
    [SerializeField] private CameraMoveRecorder _cameraMoveRecorder;

    void Awake()
    {
        _cameraPositionDatabaseCopy = _cameraPositionDatabase.cameraDataSheet;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _mainCamera.position = new Vector3(_cameraPositionDatabaseCopy[0].posX, _cameraPositionDatabaseCopy[0].posY, _cameraPositionDatabaseCopy[0].posZ);  // 初期位置
        _mainCamera.rotation = Quaternion.Euler(new Vector3(_cameraPositionDatabaseCopy[0].rotX, _cameraPositionDatabaseCopy[0].rotY, _cameraPositionDatabaseCopy[0].rotZ));
        _cameraMoveRecorder.FourInitialPosition();
    }

    public void OnPointerClicked(int cameraId)
    {
        _cameraMoveRecorder.PositionUpdate(_mainCamera.position, _mainCamera.rotation.eulerAngles);
        // Excelからカメラポジションと角度を取得
        Vector3 _movePosition = new Vector3(_cameraPositionDatabaseCopy[cameraId].posX, _cameraPositionDatabaseCopy[cameraId].posY, _cameraPositionDatabaseCopy[cameraId].posZ);
        Vector3 _moveRotate = new Vector3(_cameraPositionDatabaseCopy[cameraId].rotX, _cameraPositionDatabaseCopy[cameraId].rotY, _cameraPositionDatabaseCopy[cameraId].rotZ);
        _mainCamera.position = _movePosition;
        _mainCamera.rotation = Quaternion.Euler(_moveRotate);
    }
}
