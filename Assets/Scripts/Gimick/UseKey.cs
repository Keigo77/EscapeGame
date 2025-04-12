using System;
using System.Threading;
using Cysharp.Threading.Tasks;
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
    private CancellationToken _token;

    void Awake()
    {
        _token = this.GetCancellationTokenOnDestroy();
        _showTextMessages = this.GetComponents<ShowTextMessage>();  // [0]に鍵ないとき用メッセ．[1]に鍵あるとき用メッセ．
    }

    public void MoveGimick()
    {
        MoveGimickAsync().Forget();
    }
    
    /// <summary>
    /// 鍵を使われるオブジェクト自体にアタッチ．指定のアイテムを使うと，指定した分回転する．
    /// </summary>
    private async UniTask MoveGimickAsync()
    {
        if (_selectingItem.SelectingItemID.Value != _needItemId)
        {
            _showTextMessages[0].ShowText().Forget();
            return;
        }
        MoveObj();
        _selectingItem.UseItem(_selectingItem.SelectingItemID.Value);
        await UniTask.Delay(TimeSpan.FromSeconds(1.0f));
        _showTextMessages[1].ShowText().Forget();
    }

    private void MoveObj()
    {
        _rotateObjPivot.transform.DORotate(_rotate, 1.0f);
        _collider.enabled = false;
    }
}
