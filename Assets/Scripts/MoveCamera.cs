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
    [SerializeField] private int[] _fourCameraDirection;
    [SerializeField] private GameObject[] _colliderObject;
    private int _diretionIndex = 0;

    void Awake()
    {
        _cameraPositionDatabaseCopy = _cameraPositionDatabase.cameraDataSheet;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _mainCamera.position = new Vector3(_cameraPositionDatabaseCopy[0].posX, _cameraPositionDatabaseCopy[0].posY, _cameraPositionDatabaseCopy[0].posZ);  // 初期位置
        _colliderObject[0].SetActive(true);
        _mainCamera.rotation = Quaternion.Euler(new Vector3(_cameraPositionDatabaseCopy[0].rotX, _cameraPositionDatabaseCopy[0].rotY, _cameraPositionDatabaseCopy[0].rotZ));
        _cameraMoveRecorder.FourInitialPosition();
    }

    public void MoveRight()
    {
        _colliderObject[_diretionIndex].SetActive(false);
        _diretionIndex++;
        if (_diretionIndex >= 4) _diretionIndex = 0;
        CameraLRMove();
    }
    
    public void MoveLeft()
    {
        _colliderObject[_diretionIndex].SetActive(false);
        _diretionIndex--;
        if (_diretionIndex <= -1) _diretionIndex = 3;
        CameraLRMove();
    }

    private void CameraLRMove()
    {
        _cameraMoveRecorder._movePosisionsHistory.Clear();
        _cameraMoveRecorder._moveRotatesHistroy.Clear();
        _mainCamera.position = new Vector3(_cameraPositionDatabaseCopy[_fourCameraDirection[_diretionIndex]].posX, _cameraPositionDatabaseCopy[_fourCameraDirection[_diretionIndex]].posY, _cameraPositionDatabaseCopy[_fourCameraDirection[_diretionIndex]].posZ);  // 初期位置
        _mainCamera.rotation = Quaternion.Euler(new Vector3(_cameraPositionDatabaseCopy[_fourCameraDirection[_diretionIndex]].rotX, _cameraPositionDatabaseCopy[_fourCameraDirection[_diretionIndex]].rotY, _cameraPositionDatabaseCopy[_fourCameraDirection[_diretionIndex]].rotZ));
        _colliderObject[_diretionIndex].SetActive(true);
        _cameraMoveRecorder.PositionUpdate(_mainCamera.position, _mainCamera.rotation.eulerAngles);
    }

    public void MoveIDPosCamera(int cameraId)
    {
        _cameraMoveRecorder.PositionUpdate(_mainCamera.position, _mainCamera.rotation.eulerAngles);
        // Excelからカメラポジションと角度を取得
        Vector3 _movePosition = new Vector3(_cameraPositionDatabaseCopy[cameraId].posX, _cameraPositionDatabaseCopy[cameraId].posY, _cameraPositionDatabaseCopy[cameraId].posZ);
        Vector3 _moveRotate = new Vector3(_cameraPositionDatabaseCopy[cameraId].rotX, _cameraPositionDatabaseCopy[cameraId].rotY, _cameraPositionDatabaseCopy[cameraId].rotZ);
        _mainCamera.position = _movePosition;
        _mainCamera.rotation = Quaternion.Euler(_moveRotate);
    }
}
