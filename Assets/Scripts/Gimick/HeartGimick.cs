using DG.Tweening;
using UnityEngine;

public class HeartGimick : MonoBehaviour
{
    public enum Face
    {
        Smile = 0,
        Normal = 1,
        Angly = 2
    }

    [SerializeField] private GameObject _smileButton;
    [SerializeField] private GameObject _normalButton;
    [SerializeField] private GameObject _anglyButton;
    [SerializeField] private GameObject _boxPivotObj;
    [SerializeField] private Face[] _answer = new Face[9];
    private Face[] _input = new Face[9];
    private int index = 0;
    private Vector3 _buttonPosition;        // ボタンを押す前の座標
    private Vector3 _pushButtonPosition;    // ボタンを押した時の座標
    private ShowTextMessage _showTextMessage;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _showTextMessage = this.GetComponent<ShowTextMessage>();
        _buttonPosition = _smileButton.GetComponent<Transform>().localPosition;
    }

    void Start()
    {
        _pushButtonPosition = new Vector3(_buttonPosition.x, _buttonPosition.y, _buttonPosition.z + 0.1f);
    }

    public void PointerDownHeartButton(int face)
    {
        switch ((Face)face)
        {
            case Face.Smile:
                _input[index] = Face.Smile;
                _smileButton.transform.DOLocalMove(_pushButtonPosition, 0.25f)
                    .OnComplete(() => _smileButton.transform.DOLocalMove(_buttonPosition, 0.25f));
                break;
            case Face.Normal:
                _input[index] = Face.Normal;
                _normalButton.transform.DOLocalMove(_pushButtonPosition, 0.25f)
                    .OnComplete(() => _normalButton.transform.DOLocalMove(_buttonPosition, 0.25f));
                break;
            case Face.Angly:
                _input[index] = Face.Angly;
                _anglyButton.transform.DOLocalMove(_pushButtonPosition, 0.25f)
                    .OnComplete(() => _anglyButton.transform.DOLocalMove(_buttonPosition, 0.25f));
                break;
            default:
                break;
        }
        InputCheck();
    }

    private void InputCheck()
    {
        if (_input[index] == _answer[index])
        {
            if (index == _answer.Length - 1) Correct();
            index++;
        }
        else
        {
            index = 0;
        }
    }

    private void Correct()
    {
        _boxPivotObj.transform.DORotate(new Vector3(0, 90, 0), 0.2f);
        _showTextMessage.ShowText();
    }
}
