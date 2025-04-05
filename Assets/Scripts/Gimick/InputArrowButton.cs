using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class InputArrowButton : MonoBehaviour, IMoveGimick
{
    [SerializeField] private TextMeshProUGUI _numText;

    private enum direction
    {
        Up,
        Down
    }
    [SerializeField] private direction _direction;

    public void MoveGimick()
    {
        int nextNum;
        this.transform.DOLocalMove(new Vector3(0, -0.2f, 0), 0.25f)
            .OnComplete(() => this.transform.DOLocalMove(new Vector3(0, 0f, 0), 0.25f));
        
        switch (_direction)
        {
            case direction.Up:
                nextNum = int.Parse(_numText.text) + 1;     // 0〜9で繰り返し表示する
                if (nextNum > 9) _numText.text = "0";
                else _numText.text = nextNum.ToString();
                break;
            case direction.Down:
                nextNum = int.Parse(_numText.text) - 1;     // 0〜9で繰り返し表示する
                if (nextNum < 0) _numText.text = "9";
                else _numText.text = nextNum.ToString();
                break;
            default:
                Debug.LogError("ボタンの向きが設定されていません．");
                break;
        }
    }
}
