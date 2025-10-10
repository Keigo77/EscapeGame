using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class SliderGimmick : MonoBehaviour, IMoveGimmick
{
    [SerializeField] private int[] _answers = new int[6];
    [SerializeField] private UDMoveSlider[] _sliders = new UDMoveSlider[6];
    [SerializeField] private GameObject _decideButton;
    [SerializeField] private GameObject _heartBox;
    private ShowTextMessage _showTextMessage;
    private CancellationToken _token;
    private bool _isCorrected = false;

    [SerializeField] private AudioClip _pushDecideButtonSe;
    [SerializeField] private AudioClip _solveSe;
    [SerializeField] private AudioClip _missSe;

    void Awake()
    {
        _token = this.GetCancellationTokenOnDestroy();
        _showTextMessage = this.GetComponent<ShowTextMessage>();
    }
    
    public void MoveGimmick()
    {
        MoveGimmickAsync().Forget();
    }

    private async UniTask MoveGimmickAsync()
    {
        if (_isCorrected) { return; }
        SEManager.PlaySe(_pushDecideButtonSe);
        _decideButton.transform.DOLocalMove(new Vector3(0, 0, -0.2f), 0.25f)
            .OnComplete(() => _decideButton.transform.DOLocalMove(new Vector3(0, 0, 0), 0.25f));
        
        for (int i = 0; i < _sliders.Length; i++)
        {
            if (_sliders[i].Height != _answers[i])
            {
                SEManager.PlaySe(_missSe);
                return;
            }
        }
        await Correct();
        _showTextMessage.ShowText().Forget();
        SEManager.PlaySe(_solveSe);
    }

    private async UniTask Correct()
    {
        _isCorrected = true;
        _heartBox.SetActive(true);
        await _heartBox.transform.DOLocalMoveX(-6.0f, 1.0f).AsyncWaitForCompletion();
    }
}
