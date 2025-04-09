using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

//ToDO:謎が解けたかbool値で保存．もし解けてたら各スライダーの位置と，ハートボックスの出現．あとスライダー動かす時の音，箱が出る時の音．
public class SliderGimick : MonoBehaviour, IMoveGimick
{
    [SerializeField] private int[] answers = new int[6];
    [SerializeField] private UDMoveSlider[] sliders = new UDMoveSlider[6];
    [SerializeField] private GameObject decideButton;
    [SerializeField] private GameObject heartBox;
    private ShowTextMessage _showTextMessage;
    private CancellationToken _token;
    private bool _isCorrected = false;

    void Awake()
    {
        _token = this.GetCancellationTokenOnDestroy();
        _showTextMessage = this.GetComponent<ShowTextMessage>();
    }
    
    public void MoveGimick()
    {
        if (_isCorrected) { return; }
        decideButton.transform.DOLocalMove(new Vector3(0, 0, -0.2f), 0.25f)
            .OnComplete(() => decideButton.transform.DOLocalMove(new Vector3(0, 0, 0), 0.25f));
        
        for (int i = 0; i < sliders.Length; i++)
        {
            if (sliders[i].Height != answers[i]) { return; }
        }
        Correct().Forget();
    }

    private async UniTask Correct()
    {
        _isCorrected = true;
        heartBox.SetActive(true);
        heartBox.transform.DOLocalMoveX(-6.0f, 1.0f);
        await UniTask.Delay(TimeSpan.FromSeconds(1.0f), cancellationToken: _token);
        _showTextMessage.ShowText();
    }
}
