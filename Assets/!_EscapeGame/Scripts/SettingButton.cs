using UnityEngine;
using UnityEngine.UI;

public class SettingButton : MonoBehaviour
{
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private GameObject _settingPanel;
    private Button _settingButton;

    void Start()
    {
        _settingButton = this.GetComponent<Button>();
    }

    public void ShowSettingPanel()
    {
        if (_settingPanel.activeSelf)
        {
            _uiManager.ShowUI();
            _settingButton.gameObject.SetActive(true);
            _settingPanel.SetActive(false);
            _settingButton.image.color = new Color32(133, 133, 133, 255);
        }
        else
        {
            _uiManager.DontShowUI();
            _settingButton.gameObject.SetActive(true);
            _settingPanel.SetActive(true);
            _settingButton.image.color = new Color32(72, 72, 72, 255);
        }
    }
    
}
