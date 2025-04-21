using UnityEngine;

public class MoveCameraCollider : MonoBehaviour, IMoveGimmick
{
    [SerializeField] private MoveCamera _moveCamera;
    [SerializeField] private int _cameraPosID;

    public void MoveGimmick()
    {
        _moveCamera.MoveIDPosCamera(_cameraPosID);
    }
}
