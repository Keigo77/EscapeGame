using System.Linq;
using System.Collections.Generic;
using UniRx;
using UnityEngine;


public class MoveCamera : MonoBehaviour
{
    [SerializeField] private CameraPositionExcel _cameraPositionDatabase;
    [SerializeField] private ShowHint _showHint;
    [SerializeField] private Transform _mainCamera;
    [SerializeField] private CameraMoveRecorder _cameraMoveRecorder;
    [SerializeField] private int[] _fourDirectionCameraId;
    [SerializeField] private GameObject[] _colliderObject;
    [SerializeField] private int _initialCameraPosId = 0;
    private int _diretionIndex = 0;
    public ReactiveProperty<int> CameraId { get; private set; } = new ReactiveProperty<int>(0);

    void Awake()
    {
        Application.targetFrameRate = 60;
    }
    
    void Start()
    {
        CameraId.Subscribe(_ => _showHint.CheckShowHintButton());
        CameraId.Subscribe(MoveIDPosCamera);

        var cameraPosData = _cameraPositionDatabase.cameraDataSheet;
        _mainCamera.position = new Vector3(cameraPosData[_initialCameraPosId].posX, cameraPosData[_initialCameraPosId].posY, cameraPosData[_initialCameraPosId].posZ);  // 初期位置
        _colliderObject[0].SetActive(true);
        _mainCamera.rotation = Quaternion.Euler(new Vector3(cameraPosData[_initialCameraPosId].rotX, cameraPosData[_initialCameraPosId].rotY, cameraPosData[_initialCameraPosId].rotZ));
    }

    public void MoveRight()
    {
        if (ShowTextMessage.IsShowText) { return; }
        
        _diretionIndex++;
        if (_diretionIndex >= 4) { _diretionIndex = 0; }
        OffOtherDirectionCollider();
        CameraLRMove();
    }
    
    public void MoveLeft()
    {
        if (ShowTextMessage.IsShowText) { return; }
        
        _diretionIndex--;
        if (_diretionIndex <= -1) { _diretionIndex = 3; }
        OffOtherDirectionCollider();
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
        CameraId.Value = _fourDirectionCameraId[_diretionIndex];
    }

    public void MoveIDPosCamera(int cameraId)
    {
        if (ShowTextMessage.IsShowText) { return; }

        if (_fourDirectionCameraId.Contains(cameraId))
        {
            _cameraMoveRecorder.CameraIdStack.Clear();
            _cameraMoveRecorder.CameraIdStack.Push(cameraId);
        }
        
        // Excelからカメラポジションと角度を取得
        var cameraPosData = _cameraPositionDatabase.cameraDataSheet;
        Vector3 movePosition = new Vector3(cameraPosData[cameraId].posX, cameraPosData[cameraId].posY, cameraPosData[cameraId].posZ);
        Vector3 moveRotate = new Vector3(cameraPosData[cameraId].rotX, cameraPosData[cameraId].rotY, cameraPosData[cameraId].rotZ);
        _mainCamera.position = movePosition;
        _mainCamera.rotation = Quaternion.Euler(moveRotate);
    }
}
