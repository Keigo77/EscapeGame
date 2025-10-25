using UnityEngine;

public class MoveCameraCollider : MonoBehaviour, IMoveGimmick
{
    [SerializeField] private CameraMoveRecorder _cameraMoveRecorder;
    [SerializeField] private MoveCamera _moveCamera;
    [SerializeField] private int _cameraPosID;

    public void MoveGimmick()
    {
        _cameraMoveRecorder.RecordePosition(_cameraPosID);
        _moveCamera.CameraId.Value = _cameraPosID;
    }
}
