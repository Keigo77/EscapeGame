using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

//ToDo:　レバーを動かす音．レバーギミックを解いたかのbool値の保存

public class LeverController : MonoBehaviour, IMoveGimick
{
    [SerializeField] private GameObject leverObj;
    [SerializeField] private GameObject boxCoverPivot;

    [SerializeField] private MoveDirection[] answers;
    private MoveDirection _choose;
    private int _index;
    private ShowTextMessage _showTextMessage;
    [SerializeField] private BoxCollider _boxCollider;
    private CancellationToken _token;
    private bool _isCorrected = false;

    void Awake()
    {
        _token = this.GetCancellationTokenOnDestroy();
        _showTextMessage = this.GetComponent<ShowTextMessage>();
        /*　セーブ部分
        if (!ES3.KeyExists("LeverGimick")) return;
        _isCorrected = ES3.Load<bool>("LeverGimick");
        if (_isCorrected) CorrectAnswer();
        */
    }

    public void MoveGimick()
    {
        MoveGimickAsync().Forget();
    }
    
    /// <summary>
    /// レバーをスライドした時に実行．(レバーをPointerDownで実行)
    /// </summary>
    private async UniTask MoveGimickAsync()
    {
        if (_isCorrected) { return; }
        Vector3 beforeMousePos = Input.mousePosition;      // レバーの左右は縦：Y方向　横：Z方向
        await UniTask.WaitUntil(() => Input.GetMouseButtonUp(0), cancellationToken: _token);
        Vector3 afterMousePos = Input.mousePosition;
        float distance = Mathf.Sqrt(Mathf.Pow((afterMousePos.x - beforeMousePos.x), 2) + Mathf.Pow((afterMousePos.y - beforeMousePos.y), 2));
        if (distance < 50f) { return; } // スライド量が小さければ処理しない


        float angle = Mathf.Atan2(afterMousePos.y - beforeMousePos.y, afterMousePos.x - beforeMousePos.x) * Mathf.Rad2Deg;

        switch (angle)
        {
            case >= -45f and <= 45f:
                _choose = MoveDirection.Right;
                leverObj.transform.DORotate(new Vector3(0, -45f, 0), 0.2f)
                    .OnComplete(() => leverObj.transform.DORotate(new Vector3(0, 0, 0), 0.2f));
                break;
            case >= 46f and <= 135f:
                _choose = MoveDirection.Up;
                leverObj.transform.DORotate(new Vector3(0, 0, 45f), 0.2f)
                    .OnComplete(() => leverObj.transform.DORotate(new Vector3(0, 0, 0), 0.2f));
                break;
            case >= 136f or <= -135f:
                _choose = MoveDirection.Left;
                leverObj.transform.DORotate(new Vector3(0, 45f, 0), 0.2f)
                    .OnComplete(() => leverObj.transform.DORotate(new Vector3(0, 0, 0), 0.2f));
                break;
            case >= -136f and <= -46f:
                _choose = MoveDirection.Down;
                leverObj.transform.DORotate(new Vector3(0, 0, -45f), 0.2f)
                    .OnComplete(() => leverObj.transform.DORotate(new Vector3(0, 0, 0), 0.2f));
                break;
        }

        if (answers[_index] == _choose)
        {
            _index++;
            Debug.Log(_index);
            if (_index == answers.Length)
            {
                // 箱を動かす関数
                Correct();
                _isCorrected = true;
                Debug.Log("正解");
            }
        }
        else { _index = 0; }
    }

    private void Correct()
    {
        boxCoverPivot.transform.DORotate(new Vector3(0, 315, 0), 0.5f);
        _boxCollider.enabled = false;
        _showTextMessage.ShowText().Forget();
    }
}
