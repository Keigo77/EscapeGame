using UnityEngine;
using UnityEngine.Serialization;

public class SelectBook : MonoBehaviour, IMoveGimick
{
    [SerializeField] private BookGimick _bookGimick;
    public enum BookColors
    {
        Pink = 0,
        Purple = 1,
        Green = 2,
        Yellow = 3,
        Red = 4,
        Blue = 5
    }

    [FormerlySerializedAs("_bookColor")] public BookColors BookColor;
    
    public void MoveGimick()
    {
        _bookGimick.MoveBook((int)BookColor, this.gameObject.transform.position);
    }

}
