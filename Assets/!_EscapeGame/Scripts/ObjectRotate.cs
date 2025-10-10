using UnityEngine;
using UnityEngine.Serialization;

public class ObjectRotate : MonoBehaviour
{
    [SerializeField] private GameObject[] targetObjects;
    [SerializeField] private SelectingItem selectingItem;
    private Vector3 _objRotate = new Vector3(0f, 0f, 0f);
    private Vector3 _lastMousePosition;
    private bool _isMouseDowned = false;
    
    private void Update()
    {
        if (selectingItem.SelectingItemID.Value < 0) { return; }
        if (Input.GetMouseButtonDown(0) && !_isMouseDowned)
        {
            _isMouseDowned = true;
            _objRotate = targetObjects[selectingItem.SelectingItemID.Value].transform.localRotation.eulerAngles;
            _lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0) && _isMouseDowned)
        {
            // マウスの移動量分，アイテムを回転させる.
            _objRotate.y += (Input.mousePosition.x - _lastMousePosition.x) * 0.08f;
            _objRotate.x += (Input.mousePosition.y - _lastMousePosition.y) * 0.08f;
            targetObjects[selectingItem.SelectingItemID.Value].transform.localRotation =  Quaternion.Euler(_objRotate);
            _lastMousePosition = Input.mousePosition;
        }
        else if(_isMouseDowned && !Input.GetMouseButton(0)) { _isMouseDowned = false; }
    }

    /// <summary>
    /// 
    /// </summary>
    public void ResetRotate()
    {
        foreach (var obj in targetObjects)
        {
            obj.transform.localRotation = Quaternion.Euler(Vector3.zero);
            _objRotate = Vector3.zero;
            _isMouseDowned = false;
        }
    }
}
