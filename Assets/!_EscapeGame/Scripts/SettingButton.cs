using UnityEngine;
using UnityEngine.UI;

public class SettingButton : MonoBehaviour
{
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private GameObject _settingPanel;
    [SerializeField] private AudioClip _showSettingPanelSe;
    [SerializeField] private AudioClip _deleteSettingPanelSe;
    private Button _settingButton;

    void Start()
    {
        _settingButton = this.GetComponent<Button>();
    }

    public void ShowSettingPanel()
    {
        if (_settingPanel.activeSelf)
        {
            SEManager.PlaySe(_deleteSettingPanelSe);
            _uiManager.ShowUI();
            _settingButton.gameObject.SetActive(true);
            _settingPanel.SetActive(false);
            _settingButton.image.color = new Color32(133, 133, 133, 255);
        }
        else
        {
            SEManager.PlaySe(_showSettingPanelSe);
            _uiManager.DontShowUI();
            _settingButton.gameObject.SetActive(true);
            _settingPanel.SetActive(true);
            _settingButton.image.color = new Color32(72, 72, 72, 255);
        }
    }
    
}
