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
    [SerializeField] private int _initialCameraPosId = 0;
    private int _diretionIndex = 0;

    void Awake()
    {
        Application.targetFrameRate = 60;
        _cameraPositionDatabaseCopy = _cameraPositionDatabase.cameraDataSheet;
    }
    
    void Start()
    {
        _mainCamera.position = new Vector3(_cameraPositionDatabaseCopy[_initialCameraPosId].posX, _cameraPositionDatabaseCopy[_initialCameraPosId].posY, _cameraPositionDatabaseCopy[_initialCameraPosId].posZ);  // 初期位置
        _colliderObject[0].SetActive(true);
        _mainCamera.rotation = Quaternion.Euler(new Vector3(_cameraPositionDatabaseCopy[_initialCameraPosId].rotX, _cameraPositionDatabaseCopy[_initialCameraPosId].rotY, _cameraPositionDatabaseCopy[_initialCameraPosId].rotZ));
        _cameraMoveRecorder.FourInitialPosition();
    }

    public void MoveRight()
    {
        if (ShowTextMessage.IsShowText) { return; }
        
        _diretionIndex++;
        OffOtherDirectionCollider();
        if (_diretionIndex >= 4) { _diretionIndex = 0; }
        CameraLRMove();
    }
    
    public void MoveLeft()
    {
        if (ShowTextMessage.IsShowText) { return; }
        
        _diretionIndex--;
        OffOtherDirectionCollider();
        if (_diretionIndex <= -1) { _diretionIndex = 3; }
        CameraLRMove();
    }

    private void OffOtherDirectionCollider()
    {
        int i = 0;
        foreach (var collider in _colliderObject)
        {
            if (i == _diretionIndex)
            {
                collider.SetActive(true);
            }
            else
            {
                collider.SetActive(false);
            }

            i++;
        }
    }

    private void CameraLRMove()
    {
        if (ShowTextMessage.IsShowText) { return; }
        _cameraMoveRecorder.MovePosisionsHistory.Clear();
        _cameraMoveRecorder.MoveRotatesHistroy.Clear();
        _mainCamera.position = new Vector3(_cameraPositionDatabaseCopy[_fourCameraDirection[_diretionIndex]].posX, _cameraPositionDatabaseCopy[_fourCameraDirection[_diretionIndex]].posY, _cameraPositionDatabaseCopy[_fourCameraDirection[_diretionIndex]].posZ);  // 初期位置
        _mainCamera.rotation = Quaternion.Euler(new Vector3(_cameraPositionDatabaseCopy[_fourCameraDirection[_diretionIndex]].rotX, _cameraPositionDatabaseCopy[_fourCameraDirection[_diretionIndex]].rotY, _cameraPositionDatabaseCopy[_fourCameraDirection[_diretionIndex]].rotZ));
        _colliderObject[_diretionIndex].SetActive(true);
        _cameraMoveRecorder.PositionUpdate(_mainCamera.position, _mainCamera.rotation.eulerAngles);
    }

    public void MoveIDPosCamera(int cameraId)
    {
        if (ShowTextMessage.IsShowText) { return; }
        _cameraMoveRecorder.PositionUpdate(_mainCamera.position, _mainCamera.rotation.eulerAngles);
        // Excelからカメラポジションと角度を取得
        Vector3 movePosition = new Vector3(_cameraPositionDatabaseCopy[cameraId].posX, _cameraPositionDatabaseCopy[cameraId].posY, _cameraPositionDatabaseCopy[cameraId].posZ);
        Vector3 moveRotate = new Vector3(_cameraPositionDatabaseCopy[cameraId].rotX, _cameraPositionDatabaseCopy[cameraId].rotY, _cameraPositionDatabaseCopy[cameraId].rotZ);
        _mainCamera.position = movePosition;
        _mainCamera.rotation = Quaternion.Euler(moveRotate);
    }
}
