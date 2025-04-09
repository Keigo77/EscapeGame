using UnityEngine;
using UnityEngine.Serialization;

public class ObjectRotate : MonoBehaviour
{
    [SerializeField] private GameObject[] targetObjects;
    [SerializeField] private SelectingItem selectingItem;
    private Vector3 _objRotate = new Vector3(0f, 0f, 0f);
    private Vector3 _lastMousePosition;
    
    private void Update()
    {
        if (selectingItem.SelectingItemID.Value < 0) { return; }
        if (Input.GetMouseButtonDown(0))
        {
            _objRotate.y = 0f;
            _objRotate.x = 0f;
            _lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            // マウスの移動量分カメラを回転させる.
            _objRotate.y += (Input.mousePosition.x - _lastMousePosition.x) * 0.08f;
            _objRotate.x += (Input.mousePosition.y - _lastMousePosition.y) * 0.08f;
            targetObjects[selectingItem.SelectingItemID.Value].transform.localRotation = Quaternion.Euler(_objRotate);
            _lastMousePosition = Input.mousePosition;
        }

    }

    public void ResetRotate()
    {
        foreach (var obj in targetObjects)
        {
            obj.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
    }
}
