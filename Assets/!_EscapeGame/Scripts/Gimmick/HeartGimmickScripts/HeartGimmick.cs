using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class HeartGimmick : MonoBehaviour
{
    [SerializeField] private GameObject _smileButton;
    [SerializeField] private GameObject _normalButton;
    [SerializeField] private GameObject _angryButton;
    [SerializeField] private GameObject _boxPivotObj;
    [SerializeField] private BoxCollider _boxCollider;
    [SerializeField] private Faces[] _answers = new Faces[9];
    private readonly Faces[] _inputs = new Faces[9];
    private bool _isSolved = false;
    private int _index = 0;
    private float _beforePushPosZ;        // ボタンを押す前の座標
    private float _pushedPosZ;    // ボタンを押した時の座標
    private ShowTextMessage _showTextMessage;
    private CancellationToken _token;
    [SerializeField] private AudioClip _pushDecideButtonSe;
    [SerializeField] private AudioClip _openBoxSe;
    [SerializeField] private AudioClip _solveSe;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _token = this.GetCancellationTokenOnDestroy();
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
    public void PointerDownHeartButton(Faces face)
    {
        if (_isSolved) { return; }
        SEManager.PlaySe(_pushDecideButtonSe);
        _inputs[_index] = face;
        switch (face)
        {
            case Faces.Smile:
                MoveButton(_smileButton);
                break;
            case Faces.Normal:
                MoveButton(_normalButton);
                break;
            case Faces.Angly:
                MoveButton(_angryButton);
                break;
        }
        InputCheck().Forget();
    }

    private void MoveButton(GameObject button)
    {
        button.transform.DOLocalMoveZ(_pushedPosZ, 0.25f)
            .OnComplete(() => button.transform.DOLocalMoveZ(_beforePushPosZ, 0.25f));
    }
    
    private async UniTask InputCheck()
    {
        if (_inputs[_index] == _answers[_index])
        {
            if (_index == _answers.Length - 1)
            {
                SEManager.PlaySe(_openBoxSe);
                await Correct();
                await UniTask.Delay(TimeSpan.FromSeconds(0.2f), cancellationToken: _token);
                _showTextMessage.ShowText().Forget();
                SEManager.PlaySe(_solveSe);
            }
            _index++;
        }
        else { _index = 0; }
    }

    private async UniTask Correct()
    {
        await _boxPivotObj.transform.DORotate(new Vector3(0, 90, 0), 0.5f).AsyncWaitForCompletion();
        _boxCollider.enabled = false;
        _isSolved = true;
    }
}
