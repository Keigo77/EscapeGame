using UnityEngine;

public class UIManager : MonoBehaviour
{
    // UI
    [SerializeField] private GameObject _showItemButton;
    [SerializeField] private GameObject _settingButton;
    [SerializeField] private GameObject _rightButton;
    [SerializeField] private GameObject _leftButton;
    [SerializeField] private GameObject _undoButton;
    [SerializeField] private GameObject _gearItemRawImageObj;
    [SerializeField] private GameObject _text;
    [SerializeField] private Canvas _canvas;
    public bool IsShowUI { get; private set; } = true;

    // UI表示を決定するためのスクリプト
    [SerializeField] private SelectingItem _selectingItem;
    [SerializeField] private ShowGotItem _showGotItem;
    [SerializeField] private CameraMoveRecorder _cameraMoveRecorder;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U) && IsShowUI && !_showGotItem.ItemPanel.activeSelf)
        {
            IsShowUI = false;
            DontShowUI();
        } 
        else if (Input.GetKeyDown(KeyCode.U) && !IsShowUI && !_showGotItem.ItemPanel.activeSelf)
        {
            IsShowUI = true;
            ShowUI();
        }
    }

    public void ShowUI()
    {
        _showItemButton.SetActive(true);
        _settingButton.SetActive(true);
        _text.SetActive(true);
        _cameraMoveRecorder.IsShowUIButton();
        if (_selectingItem.SelectingItemID.Value >= 0) { _gearItemRawImageObj.SetActive(true); } 
    }

    public void DontShowUI()
    {
        _showItemButton.SetActive(false);
        _settingButton.SetActive(false);
        _rightButton.SetActive(false);
        _leftButton.SetActive(false);
        _undoButton.SetActive(false);
        _gearItemRawImageObj.SetActive(false);
        _text.SetActive(false);
    }
}
