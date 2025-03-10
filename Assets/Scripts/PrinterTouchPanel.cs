using UnityEngine;

public class PrinterTouchPanel : MonoBehaviour
{
    [SerializeField] private GameObject[] _panels;
    [SerializeField] private GameObject _missPanel;
    [SerializeField] private LifeManager _lifeManager;
    [SerializeField] private ShowTextMessage _showTextMessage;
    [SerializeField] private GameObject _hintPaper;
    private bool _isClear = true;
    private int _panelCount = 0;

    public void MissButtonTouch()
    {
        _isClear = false;
        ShowNextPanel();
        Debug.Log("ミス");
    }

    public void ClearButtonTouch()
    {
        ShowNextPanel();
        Debug.Log("正解");
    }

    public void BadPanelButtonTouch()
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
        if (!_isClear && _panelCount >= _panels.Length) _missPanel.SetActive(true);
        else if(_isClear && _panelCount >= _panels.Length) GimmickClear();
        else _panels[_panelCount].SetActive(true);
    }

    private void GimmickClear()
    {
        Debug.Log("クリア");
        _showTextMessage.ShowText();
        _hintPaper.SetActive(true);
        
        // 少し待ってから(印刷音を出してから)テキスト表示する？
    }
    
}
