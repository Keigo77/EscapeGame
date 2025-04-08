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
    private float _beforePushPosZ;        // ボタンを押す前の座標
    private float _pushedPosZ;    // ボタンを押した時の座標
    private ShowTextMessage _showTextMessage;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _showTextMessage = this.GetComponent<ShowTextMessage>();
        _beforePushPosZ = _smileButton.GetComponent<Transform>().localPosition.z;
    }

    void Start()
    {
        _pushedPosZ = _beforePushPosZ + 0.1f;
    }

    /// <summary>
    /// ハートのボタンを押すと実行．
    /// </summary>
    /// <param name="face">押されたボタンの役割</param>
    public void PointerDownHeartButton(int face)
    {
        switch ((Face)face)
        {
            case Face.Smile:
                _input[index] = Face.Smile;
                MoveButton(_smileButton);
                break;
            case Face.Normal:
                _input[index] = Face.Normal;
                MoveButton(_normalButton);
                break;
            case Face.Angly:
                _input[index] = Face.Angly;
                MoveButton(_anglyButton);
                break;
            default:
                break;
        }
        InputCheck();
    }

    private void MoveButton(GameObject button)
    {
        button.transform.DOLocalMoveZ(_pushedPosZ, 0.25f)
            .OnComplete(() => button.transform.DOLocalMoveZ(_beforePushPosZ, 0.25f));
    }
    
    private void InputCheck()
    {
        if (_input[index] == _answer[index])
        {
            if (index == _answer.Length - 1) {Correct();}
            index++;
        }
        else {index = 0;}
        
    }

    public void Correct()
    {
        _boxPivotObj.transform.DORotate(new Vector3(0, 90, 0), 0.2f);
        _showTextMessage.ShowText();
    }
}
