using System.Collections;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class UDMoveSlider : MonoBehaviour, IMoveGimick
{
    //下：-2　上：0
    public int height { get; set; } = 0;
    private float[] _positions = {0.0f, 0.33f, 0.66f, 0.99f, 1.33f, 1.66f, 2.0f};
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
            if (Input.GetMouseButtonUp(0)) break;
            _afterMousePos = Input.mousePosition;
            float distance = _afterMousePos.y - _beforeMousePos.y;
            Debug.Log(distance);
            await UniTask.Yield(_token);
        }
    }
    
}
