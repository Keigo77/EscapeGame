using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ShowConversation : MonoBehaviour
{
    [SerializeField] private TextAsset _conversationTextFile;
    [SerializeField] private TextMeshProUGUI _conversationText;
    [SerializeField] private int _maxTextLine;
    public static float AddTextSpan = 0.05f;     // ページ送りの速度を設置
    private CancellationToken _token;
    private string[] _fileTexts;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _fileTexts = _conversationTextFile.text.Split(char.Parse("\n"));
        ShowText().Forget();
    }

    private async UniTask ShowText()
    {
        int textsIndex = 0;
        
        while (textsIndex < _fileTexts.Length)
        {
            for (int j = 0; j < _fileTexts[textsIndex].Length; j++)
            {
                _conversationText.text += _fileTexts[textsIndex][j];
                await UniTask.Delay(TimeSpan.FromSeconds(AddTextSpan), cancellationToken: _token);
            }
            _conversationText.text += "\n";
            // クリックを待つ
            await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0), cancellationToken: _token);
            textsIndex++;

            if (_conversationText.textInfo.lineCount > _maxTextLine)
            {
                _conversationText.text = "";
            }
        }
        // シーン遷移
    }
}
