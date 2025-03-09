using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    /// <summary>
    /// オブジェクトを削除する．アイテムを獲得する時などに使用する．
    /// </summary>
    public void DestroyGameObject(GameObject targetGameObject)
    {
        Destroy(targetGameObject);
    }
}
