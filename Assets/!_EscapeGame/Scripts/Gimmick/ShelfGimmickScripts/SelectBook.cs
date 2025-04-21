using UnityEngine;
using UnityEngine.Serialization;

public class SelectBook : MonoBehaviour, IMoveGimmick
{
    [SerializeField] private BookGimmick _bookGimmick;
    [SerializeField] private BookColors _bookColor;
    public BookColors BookColor => _bookColor;
    
    public void MoveGimmick()
    {
        _bookGimmick.MoveBook(BookColor);
    }

}
