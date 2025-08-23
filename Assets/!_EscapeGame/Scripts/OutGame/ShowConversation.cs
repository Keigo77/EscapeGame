using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowConversation : MonoBehaviour
{
    [SerializeField] private TextAsset _conversationTextFile;
    [SerializeField] private TextMeshProUGUI _conversationText;
    [SerializeField] SceneTransition _sceneTransition;
    [SerializeField] private AudioClip _clickSe;
    private const int MAXTEXTLINE = 12;
    private CancellationTokenSource _ctsToken;
    private CancellationToken _token;
    private string[] _fileTexts;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _token = this.GetCancellationTokenOnDestroy();
        _ctsToken = new CancellationTokenSource();
        _fileTexts = _conversationTextFile.text.Split(char.Parse("\n"));
        ShowText().Forget();
    }

    private async UniTask ShowText()
    {
        int textsIndex = 0;
        
        while (textsIndex < _fileTexts.Length)
        {
            //await UniTask.WaitUntil(() => !_ctsToken.IsCancellationRequested, cancellationToken: _token);
            for (int j = 0; j < _fileTexts[textsIndex].Length; j++)
            {
                if (j < _fileTexts[textsIndex].Length && _fileTexts[textsIndex][j] == '\\' &&
                    _fileTexts[textsIndex][j + 1] == 'n')
                {
                    _conversationText.text += "\n";
                    j++;
                    continue;
                }
                _conversationText.text += _fileTexts[textsIndex][j];
            }
            
            // クリックを待つ
            ShowTriangles().Forget();
            await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0), cancellationToken: _token);
            SEManager.PlaySe(_clickSe);
            _ctsToken.Cancel();
            
            _conversationText.text = _conversationText.text.Replace("▼", "");  // ▼だけ消す
            _conversationText.text += "\n";
            textsIndex++;

            if (_conversationText.textInfo.lineCount > MAXTEXTLINE) { _conversationText.text = ""; }
        }
        // シーン遷移
        _sceneTransition.SwitchScene();
    }

    private async UniTask ShowTriangles()
    {
        while (!_ctsToken.IsCancellationRequested)
        {
            _conversationText.text += '▼';
            await UniTask.Delay(TimeSpan.FromSeconds(0.3f), cancellationToken: _token);
            _conversationText.text = _conversationText.text.Replace("▼", "");  // ▼だけ消す
            await UniTask.Delay(TimeSpan.FromSeconds(0.3f), cancellationToken: _token);
        }
        _ctsToken = new CancellationTokenSource();
    }
}
