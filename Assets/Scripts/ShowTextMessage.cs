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
    // staticでないと，あるオブジェクトのテキストが出ているときに他のオブジェクトを触ると，テキストが連続で表示されてしまう．
    public static bool IsShowText;
    private CancellationToken _token;

    void Awake()
    {
        _token = this.GetCancellationTokenOnDestroy();
    }

    public void ShowExplainText()
    {
        ShowText().Forget();
    }

    public async UniTask ShowText()
    {
        if (IsShowText) { return; } // すでに文章を表示しているならリターン
        IsShowText = true;
        _textPanel.SetActive(true);

        foreach (var listText in _textSentences)
        {
            _text.text = listText;
            await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0), cancellationToken: _token);
        }
        _textPanel.SetActive(false);
        await UniTask.WaitUntil(() => Input.GetMouseButtonUp(0), cancellationToken: _token);
        IsShowText = false;
    }
    
}
