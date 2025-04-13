using UnityEngine;
using UnityEngine.Serialization;

public class HeartButtonPush : MonoBehaviour, IMoveGimmick
{
    [SerializeField] private HeartGimmick _heartGimmick;
    [SerializeField] private Faces _faceType;
    
    /// <summary>
    /// ハートのボタンにアタッチする．
    /// </summary>
    public void MoveGimmick()
    {
        _heartGimmick.PointerDownHeartButton(_faceType);
    }
}