using UnityEngine;

public class ObjectRotate : MonoBehaviour
{
    [SerializeField] private GameObject[] _targetObjects;
    private Vector3 _objRotate = new Vector3(0f, 0f, 0f);
    private Vector3 lastMousePosition;
    
    private void Update()
    {
        if (SelectingItem.selectingItemID.Value < 0) return;
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            // マウスの移動量分カメラを回転させる.
            _objRotate.y += (Input.mousePosition.x - lastMousePosition.x) * 0.1f;
            _objRotate.x += (Input.mousePosition.y - lastMousePosition.y) * 0.1f;
            _targetObjects[SelectingItem.selectingItemID.Value].transform.localRotation = Quaternion.Euler(_objRotate);

            lastMousePosition = Input.mousePosition;
        }

    }
}
