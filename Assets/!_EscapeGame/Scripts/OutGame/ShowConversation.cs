using TMPro;
using UnityEngine;

public class ShowConversation : MonoBehaviour
{
    [SerializeField] private ConversationSO _conversationSo;
    [SerializeField] private TextMeshProUGUI _speakerText;
    [SerializeField] private TextMeshProUGUI _conversationText;
    [SerializeField] private AudioSource _audioSource;
    private int _index = 0;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UpdateConversationText();
        }
    }

    private void UpdateConversationText()
    {
        var data = _conversationSo.ConversationDatas[_index];
        _speakerText.text = data.Speaker.ToString();
        _conversationText.text = data.Text;
        if (data.Se != null)
        {
            _audioSource.PlayOneShot(data.Se);
        }
        _index++;
    }
}
