using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class LeverGimmick : MonoBehaviour, IMoveGimmick
{
    [SerializeField] private GameObject _leverObj;
    [SerializeField] private GameObject _boxCoverPivot;
    [SerializeField] private MoveDirection[] _answers;
    private MoveDirection _choose;
    private int _index;
    private ShowTextMessage _showTextMessage;
    [SerializeField] private BoxCollider _boxCollider;
    private CancellationToken _token;
    private bool _isCorrected = false;
    [SerializeField] private AudioClip _openBoxSe;
    [SerializeField] private AudioClip _leverSe;
    [SerializeField] private AudioClip _solveSe;

    void Awake()
    {
        _token = this.GetCancellationTokenOnDestroy();
        _showTextMessage = this.GetComponent<ShowTextMessage>();
    }

    public void MoveGimmick()
    {
        MoveGimmickAsync().Forget();
    }
    
    /// <summary>
    /// レバーをスライドした時に実行．(レバーをPointerDownで実行)
    /// </summary>
    private async UniTask MoveGimmickAsync()
    {
        if (_isCorrected) { return; }
        Vector3 beforeMousePos = Input.mousePosition;      // レバーの左右は縦：Y方向　横：Z方向
        await UniTask.WaitUntil(() => Input.GetMouseButtonUp(0), cancellationToken: _token);
        Vector3 afterMousePos = Input.mousePosition;
        float distance = Mathf.Sqrt(Mathf.Pow((afterMousePos.x - beforeMousePos.x), 2) + Mathf.Pow((afterMousePos.y - beforeMousePos.y), 2));
        if (distance < 50f) { return; } // スライド量が小さければ処理しない
        
        float angle = Mathf.Atan2(afterMousePos.y - beforeMousePos.y, afterMousePos.x - beforeMousePos.x) * Mathf.Rad2Deg;
        Vector3 moveDirection = Vector3.zero;
        switch (angle)
        {
            case >= -45f and <= 45f:
                _choose = MoveDirection.Right;
                moveDirection = new Vector3(0, -45f, 0);
                break;
            case >= 46f and <= 135f:
                _choose = MoveDirection.Up;
                moveDirection = new Vector3(0, 0, 45f);
                break;
            case >= 136f or <= -135f:
                _choose = MoveDirection.Left;
                moveDirection = new Vector3(0, 45f, 0);
                break;
            case >= -136f and <= -46f:
                _choose = MoveDirection.Down;
                moveDirection = new Vector3(0, 0, -45f);
                break;
        }
        SEManager.PlaySe(_leverSe);
        _leverObj.transform.DORotate(moveDirection, 0.2f)
            .OnComplete(() => _leverObj.transform.DORotate(new Vector3(0, 0, 0), 0.2f));

        if (_answers[_index] == _choose)
        {
            if (_index == _answers.Length - 1)
            {
                SEManager.PlaySe(_openBoxSe);
                await Correct();
                _showTextMessage.ShowText().Forget();
                SEManager.PlaySe(_solveSe);
                _isCorrected = true;
            }
            
            _index++;
        }
        else if (_choose == _answers[0])
        {
            _index = 1;
        }
        else
        {
            _index = 0;
        }
    }

    private async UniTask Correct()
    {
        await _boxCoverPivot.transform.DORotate(new Vector3(0, 315, 0), 0.5f).AsyncWaitForCompletion();
        _boxCollider.enabled = false;
    }
}
