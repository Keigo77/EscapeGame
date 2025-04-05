using DG.Tweening;
using UnityEngine;


// ToDo:コイン投入の音．コインの枚数の保存 
public class KeyMachineGimick : MonoBehaviour, IMoveGimick, ICorrect
{
    [SerializeField] private SelectingItem _selectingItem;
    [SerializeField] private GameObject _boxCoverPivot;
    [SerializeField] private GameObject _glassCover;
    private int _coinCounter = 0;

    /// <summary>
    /// コインを装備中にマシンをクリックしたら実行
    /// </summary>
    public void MoveGimick()
    {
        if (_selectingItem.selectingItemID.Value != 9) return;  // ID9のアイテムはコイン
        _coinCounter++;
        MoveMachine();
    }
    
    private void MoveMachine()
    {
        switch (_coinCounter)
        {
            case 1:
                _boxCoverPivot.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.5f);
                break;
            case 3:
                _glassCover.transform.DOMoveY(-3.0f, 1.0f);
                break;
            default:
                break;
        }
        _selectingItem.UseItem(_selectingItem.selectingItemID.Value);
    }

    public void Correct()
    {
        // コインの枚数をロード
        MoveMachine();
    }
}
