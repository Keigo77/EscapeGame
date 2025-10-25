using System.Threading;
using TMPro;
using UnityEngine;
using System;
using Cysharp.Threading.Tasks;
using NUnit.Framework;

public class ShowTextMessage : MonoBehaviour, IShowText
{
    [SerializeField] private GameObject _textPanel;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private string[] _textSentences;
    // staticでないと，あるオブジェクトのテキストが出ているときに他のオブジェクトを触ると，テキストが連続で表示されてしまう．
    public static bool IsShowText = false;
    private CancellationToken _token;

    void Awake()
    {
        _token = this.GetCancellationTokenOnDestroy();
    }

    public void ShowExplainText()
    {
        ShowText().Forget();
    }

    // ヒント文代入に使用
    public void SetHintText(string text)
    {
        _textSentences[0] = text;
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
        // IsShowTextがfalseとなると同時にボタンが押されると，テキスト表示後すぐにカメラ移動してしまうため，0.2秒待ってからカメラが動くようにする
        await UniTask.Delay(TimeSpan.FromSeconds(0.2f), cancellationToken: _token);
        IsShowText = false;
    }
    
}
