using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;


// ToDo:コイン投入の音．コインの枚数の保存 
public class KeyMachineGimick : MonoBehaviour, IMoveGimick
{
    [SerializeField] private SelectingItem selectingItem;
    [SerializeField] private GameObject boxCoverPivot;
    [SerializeField] private GameObject glassCover;
    private int _coinCounter = 0;

    /// <summary>
    /// コインを装備中にマシンをクリックしたら実行
    /// </summary>
    public void MoveGimick()
    {
        if (selectingItem.SelectingItemID.Value != 9) { return; }  // ID9のアイテムはコイン
        _coinCounter++;
        MoveMachine();
    }
    
    private void MoveMachine()
    {
        switch (_coinCounter)
        {
            case 1:
                boxCoverPivot.transform.DOLocalRotate(new Vector3(0, 0, 0), 0.5f);
                break;
            case 3:
                glassCover.transform.DOMoveY(-3.0f, 1.0f);
                break;
            default:
                break;
        }
        selectingItem.UseItem(selectingItem.SelectingItemID.Value);
    }

    public void Correct()
    {
        // コインの枚数をロード
        MoveMachine();
    }
}
