using DG.Tweening;
using TMPro;
using UnityEngine;

public class InputArrowButton : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _numText;

    public void UpButtonOnClickPointer()
    {
        int nextNum = int.Parse(_numText.text) + 1;     // 0〜9で繰り返し表示する

        this.transform.DOLocalMove(new Vector3(0, -0.2f, 0), 0.25f)
            .OnComplete(() => this.transform.DOLocalMove(new Vector3(0, 0f, 0), 0.25f));
        if (nextNum > 9) _numText.text = "0";
        else _numText.text = nextNum.ToString();
    }
    
    public void DownButtonOnClickPointer()
    {
        int nextNum = int.Parse(_numText.text) - 1;     // 0〜9で繰り返し表示する
        
        this.transform.DOLocalMove(new Vector3(0, -0.2f, 0), 0.25f)
            .OnComplete(() => this.transform.DOLocalMove(new Vector3(0, 0f, 0), 0.25f));
        if (nextNum < 0) _numText.text = "9";
        else _numText.text = nextNum.ToString();
    }
}
