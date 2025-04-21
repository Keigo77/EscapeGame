using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

//ToDO:謎が解けたかbool値で保存．もし解けてたら各スライダーの位置と，ハートボックスの出現．あとスライダー動かす時の音，箱が出る時の音．
public class SliderGimmick : MonoBehaviour, IMoveGimmick
{
    [SerializeField] private int[] _answers = new int[6];
    [SerializeField] private UDMoveSlider[] _sliders = new UDMoveSlider[6];
    [SerializeField] private GameObject _decideButton;
    [SerializeField] private GameObject _heartBox;
    private ShowTextMessage _showTextMessage;
    private CancellationToken _token;
    private bool _isCorrected = false;

    void Awake()
    {
        _token = this.GetCancellationTokenOnDestroy();
        _showTextMessage = this.GetComponent<ShowTextMessage>();
    }
    
    public void MoveGimmick()
    {
        if (_isCorrected) { return; }
        _decideButton.transform.DOLocalMove(new Vector3(0, 0, -0.2f), 0.25f)
            .OnComplete(() => _decideButton.transform.DOLocalMove(new Vector3(0, 0, 0), 0.25f));
        
        for (int i = 0; i < _sliders.Length; i++)
        {
            if (_sliders[i].Height != _answers[i]) { return; }
        }
        Correct().Forget();
    }

    private async UniTask Correct()
    {
        _isCorrected = true;
        _heartBox.SetActive(true);
        _heartBox.transform.DOLocalMoveX(-6.0f, 1.0f);
        await UniTask.Delay(TimeSpan.FromSeconds(1.0f), cancellationToken: _token);
        _showTextMessage.ShowText().Forget();
    }
}
