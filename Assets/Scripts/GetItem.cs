using UnityEngine;

public class GetItem : MonoBehaviour, IMoveGimick
{
    [SerializeField] ShowGotItem _showGotItem;
    [SerializeField] private int _itemID;
    
    public void MoveGimick()
    {
        _showGotItem.GetItem(_itemID);
    }
    
        //ToDo: オブジェクトの名前で，オブジェクトのSetActiveのbool値を保存
}
