using System.Threading;
using TMPro;
using UnityEngine;
using System;
using Cysharp.Threading.Tasks;

public class ShowTextMessage : MonoBehaviour, IShowText
{
    [SerializeField] private GameObject _textPanel;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private string[] _textSentences;
    private bool _isShowing;
    private CancellationToken _token;

    void Awake()
    {
        _token = this.GetCancellationTokenOnDestroy();
    }

    public void ShowExplainText()
    {
        ShowText();
    }

    public async void ShowText()
    {
        if (_isShowing) return;     // すでに文章を表示しているならリターン
        _isShowing = true;
        _textPanel.SetActive(true);

        foreach (var listText in _textSentences)
        {
            _text.text = listText;
            await Wait();
        }
        _textPanel.SetActive(false);
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
