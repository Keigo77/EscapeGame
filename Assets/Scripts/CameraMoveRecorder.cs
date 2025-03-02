using Unity.Mathematics.Geometry;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class CameraMoveRecorder : MonoBehaviour
{
    [SerializeField] private Transform _cameraPosition;
    [SerializeField] private Button _undoButton;
    private Vector3[] _cameraPositionsHistroy = new Vector3[3];
    private Vector3[] _cameraRotateHistroy = new Vector3[3];
    private ReactiveProperty<int> _historyIndex = new ReactiveProperty<int>(0);

    void Start()
    {
        _historyIndex.Subscribe(_ => isShowUndoButton());
    }

    public void PositionUpdate(Vector3 cameraPosition, Vector3 cameraRotate)  // カメラが動くときに，動く前の位置を保存する
    {
        _historyIndex.Value++;
        _cameraPositionsHistroy[_historyIndex.Value] = cameraPosition;
        _cameraRotateHistroy[_historyIndex.Value] = cameraRotate;
    }

    public void UndoCameraPosition()
    {
        _historyIndex.Value--;
        _cameraPosition.position = _cameraPositionsHistroy[Mathf.Max(0, _historyIndex.Value)];
        _cameraPosition.rotation = Quaternion.Euler(_cameraRotateHistroy[Mathf.Max(0, _historyIndex.Value)]);
    }

    public void FourInitialPosition()
    {
        _cameraPositionsHistroy[0] = _cameraPosition.position;
        _cameraRotateHistroy[0] = _cameraPosition.rotation.eulerAngles;
    }

    private void isShowUndoButton()
    {
        if (_historyIndex.Value >= 1) _undoButton.gameObject.SetActive(true);
        else _undoButton.gameObject.SetActive(false);
    }
}
