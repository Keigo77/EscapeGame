using UnityEngine;

public class HeartButtonPush : MonoBehaviour, IMoveGimick
{
    [SerializeField] private HeartGimick _heartGimick;
    [SerializeField] private int _faceType;
    
    /// <summary>
    /// ハートのボタンにアタッチする．
    /// </summary>
    public void MoveGimick()
    {
        _heartGimick.PointerDownHeartButton(_faceType);
    }
}