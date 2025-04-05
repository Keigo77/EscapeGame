using DG.Tweening;
using UnityEngine;
//ToDo:鍵を使う音．開けたかどうかをbool値で保存
public class UseKey : MonoBehaviour, IMoveGimick
{
    [SerializeField] private int _needItemId;
    [SerializeField] private Vector3 _rotate;
    [SerializeField] private SelectingItem _selectingItem;
    [SerializeField] private GameObject _rotateObjPivot;
    [SerializeField] private BoxCollider _collider;
    private ShowTextMessage[] _showTextMessages;    

    void Awake()
    {
        _showTextMessages = this.GetComponents<ShowTextMessage>();  // [0]に鍵ないとき用メッセ．[1]に鍵あるとき用メッセ．
    }
    
    /// <summary>
    /// 鍵を使われるオブジェクト自体にアタッチ．指定のアイテムを使うと，指定した分回転する．
    /// </summary>
    public void MoveGimick()
    {
        if (_selectingItem.selectingItemID.Value != _needItemId)
        {
            _showTextMessages[0].ShowText();
            return;
        }
        _rotateObjPivot.transform.DORotate(_rotate, 1.0f);
        _selectingItem.UseItem(_selectingItem.selectingItemID.Value);
        _collider.enabled = false;
        _showTextMessages[1].ShowText();
    }
}
