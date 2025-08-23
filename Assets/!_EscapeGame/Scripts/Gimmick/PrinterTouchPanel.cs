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
    [SerializeField] private ShowTextMessage _showTrueEndTextMessage;
    [SerializeField] private GameObject _hintPaper;
    
    [SerializeField] private CameraMoveRecorder _cameraMoveRecorder;
    [SerializeField] private MoveCamera _moveCamera;
    private bool _isClear = true;
    private bool _isTreuEnd = true;
    public static bool IsTrueEnd = false;
    private int _panelCount = 0;
    private CancellationToken _token;
    [SerializeField] private AudioClip _buttonTouchSe;
    [SerializeField] private AudioClip _missSe;
    [SerializeField] private AudioClip _printSe;
    [SerializeField] private AudioClip _solveSe;
    
    void Awake()
    {
        _token = this.GetCancellationTokenOnDestroy();
    }

    public void MissButtonTouch()
    {
        if (!IsOperationPanel()) { return; }
        SEManager.PlaySe(_buttonTouchSe);
        _isClear = false;
        _isTreuEnd = false;
        ShowNextPanel();
        Debug.Log("ミス");
    }

    public void ClearButtonTouch()
    {
        if (!IsOperationPanel()) { return; }
        SEManager.PlaySe(_buttonTouchSe);
        _isTreuEnd = false;
        ShowNextPanel();
        Debug.Log("正解");
    }

    public void TrueEndButtonTouch()
    {
        if (!IsOperationPanel()) { return; }
        SEManager.PlaySe(_buttonTouchSe);
        ShowNextPanel();
        _isClear = false;
    }

    public void BadPanelButtonTouch()   // ハズレ笑ボタン
    {
        if (!IsOperationPanel()) { return; }
        SEManager.PlaySe(_missSe);
        _lifeManager.TakeDamage();
        _isClear = true;
        _isTreuEnd = true;
        _missPanel.SetActive(false);
        _panelCount = 0;
        _panels[_panelCount].SetActive(true);
        Debug.Log("初期化");
    }

    private void ShowNextPanel()
    {
        _panels[_panelCount].SetActive(false);
        _panelCount++;
        if (!_isClear && !_isTreuEnd &&  _panelCount >= _panels.Length) { _missPanel.SetActive(true); }
        else if (_isClear && _panelCount >= _panels.Length) { GimmickClear().Forget(); }
        else if (_isTreuEnd && _panelCount >= _panels.Length)
        {
            IsTrueEnd = true;
            GimmickClear().Forget();
        }
        else { _panels[_panelCount].SetActive(true); }
    }

    private async UniTask GimmickClear()
    {
        if (IsTrueEnd)
        {
            SEManager.PlaySe(_solveSe);
            _showTrueEndTextMessage.ShowText().Forget();
        }
        else
        {
            SEManager.PlaySe(_printSe);
            await UniTask.Delay(TimeSpan.FromSeconds(2.5f), cancellationToken: _token);
            SEManager.PlaySe(_solveSe);
            await UniTask.Delay(TimeSpan.FromSeconds(0.2f), cancellationToken: _token);
            _showTextMessage.ShowText().Forget();
            _hintPaper.SetActive(true);
        }
    }

    /// <summary>
    /// UIをクリックするとrayが飛ばず，タッチパネルを拡大していなくても(遠くからでも)ボタンが反応するため，
    /// しっかりタッチパネルが見えている状態でないとギミックが動作しないようにする．
    /// </summary>
    private bool IsOperationPanel()
    {
        if (_cameraMoveRecorder.MovePosisionsHistory.Count < 3)
        {
            _moveCamera.MoveIDPosCamera(5);
            return false;
        }  
        else { return true; }
    }
    
}
