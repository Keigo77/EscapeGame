using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class InputArrowButton : MonoBehaviour, IMoveGimmick
{
    [SerializeField] private TextMeshProUGUI _numText;

    private enum Direction
    {
        Up,
        Down
    }
    [SerializeField] private Direction _direction;

    public void MoveGimmick()
    {
        int nextNum;
        this.transform.DOLocalMove(new Vector3(0, -0.2f, 0), 0.25f)
            .OnComplete(() => this.transform.DOLocalMove(new Vector3(0, 0f, 0), 0.25f));
        
        switch (_direction)
        {
            case Direction.Up:
                nextNum = int.Parse(_numText.text) + 1;     // 0〜9で繰り返し表示する
                _numText.text =  (nextNum > 9) ? "0" : nextNum.ToString();
                break;
            case Direction.Down:
                nextNum = int.Parse(_numText.text) - 1;     // 0〜9で繰り返し表示する
                _numText.text = (nextNum < 0) ? "9" : nextNum.ToString();
                break;
        }
    }
}
