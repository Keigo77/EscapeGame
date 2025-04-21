using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class PrinterTouchPanel : MonoBehaviour
{
    [SerializeField] private GameObject[] _panels;
    [SerializeField] private GameObject _missPanel;
    [SerializeField] private LifeManager _lifeManager;
    [SerializeField] private ShowTextMessage _showTextMessage;
    [SerializeField] private GameObject _hintPaper;
    
    [SerializeField] private CameraMoveRecorder _cameraMoveRecorder;
    [SerializeField] private MoveCamera _moveCamera;
    private bool _isClear = true;
    private int _panelCount = 0;
    private CancellationToken _token;
    
    void Awake()
    {
        _token = this.GetCancellationTokenOnDestroy();
    }

    public void MissButtonTouch()
    {
        // UIをクリックするとrayが飛ばず，タッチパネルを拡大していなくても(遠くからでも)ボタンが反応するため，しっかりタッチパネルが見えている状態でないとギミックが動作しないようにする．
        if (_cameraMoveRecorder.MovePosisionsHistory.Count < 3)
        {
            _moveCamera.MoveIDPosCamera(6);
            return;
        }  
        _isClear = false;
        ShowNextPanel();
        Debug.Log("ミス");
    }

    public void ClearButtonTouch()
    {
        if (_cameraMoveRecorder.MovePosisionsHistory.Count < 3)
        {
            _moveCamera.MoveIDPosCamera(6);
            return;
        }  
        ShowNextPanel();
        Debug.Log("正解");
    }

    public void BadPanelButtonTouch()   // 笑ボタン
    {
        _lifeManager.TakeDamage();
        _isClear = true;
        _missPanel.SetActive(false);
        _panelCount = 0;
        _panels[_panelCount].SetActive(true);
        Debug.Log("初期化");
    }

    private void ShowNextPanel()
    {
        _panels[_panelCount].SetActive(false);
        _panelCount++;
        if (!_isClear && _panelCount >= _panels.Length) { _missPanel.SetActive(true); }
        else if(_isClear && _panelCount >= _panels.Length) { GimmickClear().Forget(); }
        else { _panels[_panelCount].SetActive(true); }
    }

    private async UniTask GimmickClear()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(0.2f), cancellationToken: _token);
        _showTextMessage.ShowText().Forget();
        _hintPaper.SetActive(true);
        
        // 少し待ってから(印刷音を出してから)テキスト表示する？
    }
    
}
