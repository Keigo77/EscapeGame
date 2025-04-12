using System;
using System.Collections;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
//ToDo:　スライダー動かすごとに音を出す

public class UDMoveSlider : MonoBehaviour, IMoveGimick
{
    //下：-2　上：0
    public int Height { get; private set; } = 0;
    private readonly float[] _positions = {-2.0f, -1.6f, -1.2f, -0.8f, -0.4f, 0.0f};
    private Vector3 _beforeMousePos;
    private Vector3 _afterMousePos;
    private CancellationToken _token;

    void Awake()
    {
        _token = this.GetCancellationTokenOnDestroy();
    }
    
    public void MoveGimick()
    {
        MoveSlider().Forget();
    }

    private async UniTask MoveSlider()
    {
        _beforeMousePos = Input.mousePosition;
        while (true)
        {
            if (Input.GetMouseButtonUp(0)) { break; }
            _afterMousePos = Input.mousePosition;
            var distance = _afterMousePos.y - _beforeMousePos.y;
            if (Math.Abs(distance) > 50) { CalcHeight(distance); }
            
            await UniTask.Yield(_token);
        }
    }

    private void CalcHeight(float distance)
    {
        if (distance > 0 && Height + 1 < _positions.Length) { Height++; }
        if (distance < 0 && Height - 1 >= 0) { Height--; }
        this.transform.localPosition = new Vector3(this.transform.localPosition.x, _positions[Height], this.transform.localPosition.z);
        _beforeMousePos = Input.mousePosition;
    }
}
