using UnityEngine;

public class ObjectRotate : MonoBehaviour
{
    [SerializeField] private GameObject[] _targetObjects;
    [SerializeField] private SelectingItem _selectingItem;
    private Vector3 _objRotate = new Vector3(0f, 0f, 0f);
    private Vector3 lastMousePosition;
    
    private void Update()
    {
        if (_selectingItem.selectingItemID.Value < 0) return;
        if (Input.GetMouseButtonDown(0))
        {
            _objRotate.y = 0f;
            _objRotate.x = 0f;
            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            // マウスの移動量分カメラを回転させる.
            _objRotate.y += (Input.mousePosition.x - lastMousePosition.x) * 0.08f;
            _objRotate.x += (Input.mousePosition.y - lastMousePosition.y) * 0.08f;
            _targetObjects[_selectingItem.selectingItemID.Value].transform.localRotation = Quaternion.Euler(_objRotate);
            lastMousePosition = Input.mousePosition;
        }

    }

    public void ResetRotate()
    {
        foreach (var obj in _targetObjects)
        {
            obj.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
    }
}
