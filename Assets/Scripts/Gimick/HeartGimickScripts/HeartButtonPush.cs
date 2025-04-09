using UnityEngine;
using UnityEngine.Serialization;

public class HeartButtonPush : MonoBehaviour, IMoveGimick
{
    [SerializeField] private HeartGimick heartGimick;
    [SerializeField] private Faces faceType;
    
    /// <summary>
    /// ハートのボタンにアタッチする．
    /// </summary>
    public void MoveGimick()
    {
        heartGimick.PointerDownHeartButton(faceType);
    }
}