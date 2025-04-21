using UnityEngine;

public class HeartButtonPush : MonoBehaviour, IMoveGimmick
{
    [SerializeField] private HeartGimmick _heartGimmick;
    [SerializeField] private Faces _faceType;
    [SerializeField] private AudioClip _pushButtonSe;
    
    /// <summary>
    /// ハートのボタンにアタッチする．
    /// </summary>
    public void MoveGimmick()
    {
        _heartGimmick.PointerDownHeartButton(_faceType);
        SEManager.PlaySe(_pushButtonSe);
    }
}