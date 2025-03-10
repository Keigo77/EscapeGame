using System.Threading;
using TMPro;
using UnityEngine;
using System;
using Cysharp.Threading.Tasks;

public class ShowTextMessage : MonoBehaviour
{
    [SerializeField] private GameObject _UIPanel;
    [SerializeField] private TextMeshProUGUI _textObject;
    [SerializeField] private string[] _textSentences;
    private bool _isShowing;
    private int _textMaxNumber;
    private int _textIndex = 0;
    private CancellationToken _token;

    void Awake()
    {
        _textMaxNumber = _textSentences.Length;
        _token = this.GetCancellationTokenOnDestroy();
    }

    public async void ShowText()
    {
        if (_isShowing) return;     // すでに文章を表示しているならリターン
        _isShowing = true;
        _textIndex = 0;
        _UIPanel.SetActive(true);
        while (_textIndex < _textMaxNumber)
        {
            _textObject.text = _textSentences[_textIndex];
            await Wait();
            _textIndex++;
        }
        _UIPanel.SetActive(false);
        await Wait();
        _isShowing = false;
    }

    private async UniTask Wait()
    {
        try
        {
            await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0), cancellationToken: _token);
        }
        catch (OperationCanceledException)
        {
            Debug.Log("cancelled");
        }
    }
    
}
