using UnityEngine;

public class GetItem : MonoBehaviour, IMoveGimmick
{
    [SerializeField] ShowGotItem _showGotItem;
    [SerializeField] private int _itemID;
    
    public void MoveGimmick()
    {
        _showGotItem.GetItem(_itemID);
    }
    
        //ToDo: オブジェクトの名前で，オブジェクトのSetActiveのbool値を保存
}
