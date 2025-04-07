using UnityEngine;

public class MoveCameraCollider : MonoBehaviour, IMoveGimick
{
    [SerializeField] private MoveCamera _moveCamera;
    [SerializeField] private int _cameraPosID;

    public void MoveGimick()
    {
        _moveCamera.MoveIDPosCamera(_cameraPosID);
    }
}
