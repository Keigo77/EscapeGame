using UnityEngine;
using UnityEngine.Serialization;

public class SelectBook : MonoBehaviour, IMoveGimick
{
    [SerializeField] private BookGimick bookGimick;
    [SerializeField] private BookColors bookColor;
    public BookColors BookColor => bookColor;
    
    public void MoveGimick()
    {
        bookGimick.MoveBook(BookColor);
    }

}
