using UnityEngine;

public class SelectBook : MonoBehaviour, IMoveGimick
{
    [SerializeField] private BookGimick _bookGimick;
    public enum BookColor
    {
        Pink = 0,
        Purple = 1,
        Green = 2,
        Yellow = 3,
        Red = 4,
        Blue = 5
    }

    [SerializeField] private BookColor _bookColor;
    public BookColor bookColor => _bookColor;   // 外部の読み取り専用
    
    public void MoveGimick()
    {
        _bookGimick.MoveBook((int)_bookColor, this.gameObject.transform.position);
    }

}
