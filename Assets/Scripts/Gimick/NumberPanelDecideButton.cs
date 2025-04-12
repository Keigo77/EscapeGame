using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;

//ToDo:ギミックを解いたかをbool値で保存．箱が開く音を実装する

public class NumberPanelDecideButton : MonoBehaviour, IMoveGimick
{
    [Header("暗号の答えを4桁の数字で入力")]
    [SerializeField] private int _answer;
    [SerializeField] private GameObject _boxCoverPivot;
    [SerializeField] private GameObject _decideButton;
    [SerializeField] private TextMeshProUGUI _thousandText;
    [SerializeField] private TextMeshProUGUI _hundredText;
    [SerializeField] private TextMeshProUGUI _tenText;
    [SerializeField] private TextMeshProUGUI _oneText;
    private ShowTextMessage _showTextMessage;
    private bool _isCorrected = false;  // すでにこの謎を解いたか

    private void Awake()
    {
        _showTextMessage = this.GetComponent<ShowTextMessage>();
    }

    public void MoveGimick()
    {
        if (_isCorrected) { return; } // 解いた後なら，処理しない

        int playerInput = int.Parse(_thousandText.text) * 1000 +
                          int.Parse(_hundredText.text) * 100 +
                          int.Parse(_tenText.text) * 10 +
                          int.Parse(_oneText.text);

        _decideButton.transform.DOLocalMove(new Vector3(0, 0, -0.2f), 0.25f)
            .OnComplete(() => _decideButton.transform.DOLocalMove(new Vector3(0, 0, 0), 0.25f));
        if (playerInput == _answer)
        {
            Correct().Forget();
        }
    }

    private async UniTask Correct()
    {
        _isCorrected = true;
        _boxCoverPivot.transform.DOLocalRotate(new Vector3(0, -135, 0), 1.0f);
        await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
        _showTextMessage.ShowText().Forget();
    }
}
