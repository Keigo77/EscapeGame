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
    private float[] _positions = {-2.0f, -1.6f, -1.2f, -0.8f, -0.4f, 0.0f};
    private Vector3 _beforeMousePos;
    private Vector3 _afterMousePos;
    private CancellationToken _token;

    void Awake()
    {
        _token = this.GetCancellationTokenOnDestroy();
    }
    
    public void MoveGimick()
    {
        MoveSlider();
    }

    private async void MoveSlider()
    {
        _beforeMousePos = Input.mousePosition;
        while (true)
        {
            if (Input.GetMouseButtonUp(0)) { break; }
            _afterMousePos = Input.mousePosition;
            float distance = _afterMousePos.y - _beforeMousePos.y;
            if (Math.Abs(distance) > 50) { CalucHeight(distance); }
            
            await UniTask.Yield(_token);
        }
    }

    private void CalucHeight(float distance)
    {
        _beforeMousePos = Input.mousePosition;
        Height += (int)(Math.Truncate(distance / 50));
        if (Height < 0) { Height = 0; }
        if (Height > _positions.Length - 1) { Height = _positions.Length - 1; }
        this.transform.localPosition = new Vector3(this.transform.localPosition.x, _positions[Height], this.transform.localPosition.z); 
    }
}
