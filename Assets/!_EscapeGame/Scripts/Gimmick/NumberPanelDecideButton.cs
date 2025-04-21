using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using DG.Tweening;

//ToDo:ギミックを解いたかをbool値で保存．箱が開く音を実装する

public class NumberPanelDecideButton : MonoBehaviour, IMoveGimmick
{
    [Header("暗号の答えを4桁の数字で入力")]
    [SerializeField] private int _answer;
    [SerializeField] private GameObject _boxCoverPivot;
    [SerializeField] private GameObject _decideButton;
    [SerializeField] private TextMeshProUGUI _thousandText;
    [SerializeField] private TextMeshProUGUI _hundredText;
    [SerializeField] private TextMeshProUGUI _tenText;
    [SerializeField] private TextMeshProUGUI _oneText;
    [SerializeField] BoxCollider _boxCollider;
    private ShowTextMessage _showTextMessage;
    private CancellationToken _token;
    private bool _isCorrected = false;  // すでにこの謎を解いたか
    [SerializeField] private AudioClip _pushDecideButtonSe;
    [SerializeField] private AudioClip _solveSe;
    [SerializeField] private AudioClip _missSe;

    private void Awake()
    {
        _showTextMessage = this.GetComponent<ShowTextMessage>();
        _token = this.GetCancellationTokenOnDestroy();
    }
    
    public void MoveGimmick()
    {
        MoveGimmickAsync().Forget();
    }

    private async UniTask MoveGimmickAsync()
    {
        if (_isCorrected) { return; } // 解いた後なら，処理しない
        SEManager.PlaySe(_pushDecideButtonSe);
        int playerInput = int.Parse(_thousandText.text) * 1000 +
                          int.Parse(_hundredText.text) * 100 +
                          int.Parse(_tenText.text) * 10 +
                          int.Parse(_oneText.text);

        _decideButton.transform.DOLocalMove(new Vector3(0, 0, -0.2f), 0.25f)
            .OnComplete(() => _decideButton.transform.DOLocalMove(new Vector3(0, 0, 0), 0.25f));
        if (playerInput == _answer)
        {
            await Correct();
            SEManager.PlaySe(_solveSe);
            await UniTask.Delay(TimeSpan.FromSeconds(0.2f), cancellationToken: _token);
            _showTextMessage.ShowText().Forget();
        }
        else { SEManager.PlaySe(_missSe); }
    }

    private async UniTask Correct()
    {
        _isCorrected = true;
        _boxCollider.enabled = false;
        await _boxCoverPivot.transform.DOLocalRotate(new Vector3(0, -135, 0), 0.5f).AsyncWaitForCompletion();
    }
}
