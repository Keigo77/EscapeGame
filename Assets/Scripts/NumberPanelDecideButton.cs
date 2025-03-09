using System;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class NumberPanelDecideButton : MonoBehaviour
{
    [Header("暗号の答えを4桁の数字で入力")]
    [SerializeField] private int _answer;
    [SerializeField] private GameObject _boxCoverPivot;
    [SerializeField] private TextMeshProUGUI _southandText;
    [SerializeField] private TextMeshProUGUI _hundredText;
    [SerializeField] private TextMeshProUGUI _tenText;
    [SerializeField] private TextMeshProUGUI _oneText;
    private ShowTextMessage _showTextMessage;
    private bool _isCorrected = false;  // すでにこの謎を解いたか

    private void Awake()
    {
        _showTextMessage = this.GetComponent<ShowTextMessage>();
        /*　セーブ部分
        if (!ES3.KeyExists("PriceGimmick")) return;
        _isCorrected = ES3.Load<bool>("PriceGimmick");
        if (_isCorrected) CorrectAnswer();
        */
    }

    public void ClickOnDecideButton()
    {
        if (_isCorrected) return;       // 解いた後なら，処理しない
        int playerInput = int.Parse(_southandText.text) * 1000 +
                          int.Parse(_hundredText.text) * 100 +
                          int.Parse(_tenText.text) * 10 +
                          int.Parse(_oneText.text);
        
        this.transform.DOLocalMove(new Vector3(0, -3f, 0), 0.5f);
        if (playerInput == _answer)
        {
            CorrectAnswer();
        }
        else
        {
            // 間違えていた時の処理
        }
        this.transform.DOLocalMove(new Vector3(0, 0, 0), 0.5f);
    }

    private void CorrectAnswer()
    {
        _isCorrected = true;
        ES3.Save<bool>("PriceGimmick", _isCorrected);
        _boxCoverPivot.transform.DOLocalRotate(new Vector3(0, -90, 0), 1.0f);
        _showTextMessage.ShowText();
    }
}
