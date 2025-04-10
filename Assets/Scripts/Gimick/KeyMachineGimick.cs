using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;


// ToDo:コイン投入の音．コインの枚数の保存．ロード時にコインの枚数をロードし，枚数分MoveGimickを作動させる
public class KeyMachineGimick : MonoBehaviour, IMoveGimick
{
    [SerializeField] private SelectingItem selectingItem;
    [SerializeField] private GameObject boxCoverPivot;
    [SerializeField] private GameObject glassCover;
    private int _coinCounter = 0;
    private ShowTextMessage[] _showTextMessages;
    private CancellationToken _token;

    void Awake()
    {
        _showTextMessages = this.GetComponents<ShowTextMessage>();
        _token = this.GetCancellationTokenOnDestroy();
    }

    /// <summary>
    /// コインを装備中にマシンをクリックしたら実行
    /// </summary>
    public void MoveGimick()
    {
        if (selectingItem.SelectingItemID.Value != 9) { return; }  // ID9のアイテムはコイン
        _coinCounter++;
        MoveMachine().Forget();
    }
    
    private async UniTask MoveMachine()
    {
        switch (_coinCounter)
        {
            case 1:
                boxCoverPivot.transform.DOLocalRotate(new Vector3(0, 0, 0), 1.0f);
                break;
            case 3:
                glassCover.transform.DOMoveY(-3.0f, 1.0f);
                break;
            default:
                break;
        }
        selectingItem.UseItem(selectingItem.SelectingItemID.Value);
        await UniTask.Delay(TimeSpan.FromSeconds(1.0f), cancellationToken: _token);
        _showTextMessages[_coinCounter].ShowText();
    }
}
