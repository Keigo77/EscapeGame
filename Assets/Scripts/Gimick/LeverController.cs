using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;


public class LeverController : MonoBehaviour
{
    [SerializeField] private GameObject _leverObj;
    [SerializeField] private GameObject _boxRotateObj;
    private enum State
    {
        Up,
        Down,
        Right,
        Left
    }
    [SerializeField] private State[] _answers;
    private State _choose;
    private int _index;
    private ShowTextMessage _showTextMessage;
    private CancellationToken _downToken;
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
    
    public async void LeverMove()
    {
        if (_isCorrected) return;
        Vector3 beforeMousePos = Input.mousePosition;      // レバーの左右は縦：Y方向　横：Z方向
        try
        {
            await UniTask.WaitUntil(() => Input.GetMouseButtonUp(0), cancellationToken: _token);
        }
        catch (OperationCanceledException)
        {
            Debug.Log("UniTaskキャンセル");
        }
        Vector3 afterMousePos = Input.mousePosition;
        float distance = Mathf.Sqrt(Mathf.Pow((afterMousePos.x - beforeMousePos.x), 2) + Mathf.Pow((afterMousePos.y - beforeMousePos.y), 2));
        if (distance < 50f) return;     // スライド量が小さければ処理しない
        
        float angle = Mathf.Atan2(afterMousePos.y - beforeMousePos.y, afterMousePos.x - beforeMousePos.x) * Mathf.Rad2Deg;

        switch (angle)
        {
            case >= -45f and <= 45f:
                _choose = State.Right;
                _leverObj.transform.DORotate(new Vector3(0, -45f, 0), 0.2f)
                    .OnComplete(() => _leverObj.transform.DORotate(new Vector3(0, 0, 0), 0.2f));
                break;
            case >= 46f and <= 135f:
                _choose = State.Up;
                _leverObj.transform.DORotate(new Vector3(0, 0, 45f), 0.2f)
                    .OnComplete(() => _leverObj.transform.DORotate(new Vector3(0, 0, 0), 0.2f));
                break;
            case >= 136f or <= -135f:
                _choose = State.Left;
                _leverObj.transform.DORotate(new Vector3(0, 45f, 0), 0.2f)
                    .OnComplete(() => _leverObj.transform.DORotate(new Vector3(0, 0, 0), 0.2f));
                break;
            case >= -136f and <= -46f:
                _choose = State.Down;
                _leverObj.transform.DORotate(new Vector3(0, 0, -45f), 0.2f)
                    .OnComplete(() => _leverObj.transform.DORotate(new Vector3(0, 0, 0), 0.2f));
                break;
            default:
                break;
        }

        if (_answers[_index] == _choose)
        {
            _index++;
            if (_index == _answers.Length)
            {
                // 箱を動かす関数
                Correct();
                _isCorrected = true;
                /*
                 ES3.Save<bool>("LeverGimick", _isCorrected);
                 */
                Debug.Log("正解");
            }
        }
        else
        {
            _index = 0;
        }
    }

    private void Correct()
    {
        _boxRotateObj.transform.DORotate(new Vector3(0, -90, 0), 0.2f);
        _showTextMessage.ShowText();
    }
}
