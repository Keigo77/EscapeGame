using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class HeartGimick : MonoBehaviour
{
    [SerializeField] private GameObject smileButton;
    [SerializeField] private GameObject normalButton;
    [SerializeField] private GameObject anglyButton;
    [SerializeField] private GameObject boxPivotObj;
    [SerializeField] private Faces[] answers = new Faces[9];
    private readonly Faces[] _inputs = new Faces[9];
    private int _index = 0;
    private float _beforePushPosZ;        // ボタンを押す前の座標
    private float _pushedPosZ;    // ボタンを押した時の座標
    private ShowTextMessage _showTextMessage;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _showTextMessage = this.GetComponent<ShowTextMessage>();
        _beforePushPosZ = smileButton.GetComponent<Transform>().localPosition.z;
    }

    void Start()
    {
        _pushedPosZ = _beforePushPosZ + 0.1f;
    }

    /// <summary>
    /// ハートのボタンを押すと実行．
    /// </summary>
    /// <param name="face">押されたボタンの役割</param>
    public void PointerDownHeartButton(Faces face)
    {
        _inputs[_index] = face;
        switch (face)
        {
            case Faces.Smile:
                MoveButton(smileButton);
                break;
            case Faces.Normal:
                MoveButton(normalButton);
                break;
            case Faces.Angly:
                MoveButton(anglyButton);
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
        if (_inputs[_index] == answers[_index])
        {
            if (_index == answers.Length - 1) { Correct(); }
            _index++;
        }
        else { _index = 0; }
        
    }

    public void Correct()
    {
        boxPivotObj.transform.DORotate(new Vector3(0, 90, 0), 0.2f);
        _showTextMessage.ShowText();
    }
}
