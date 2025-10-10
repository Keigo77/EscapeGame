using UnityEngine;

public class ObjSetActiveManager : MonoBehaviour
{
    [SerializeField] private GameObject[] objs;

    /// <summary>
    /// オブジェクトを削除する．アイテムを獲得する時などに使用する．
    /// </summary>
    public void ObjSetActiveOff(int itemID)
    {
        objs[itemID].SetActive(false);
    }
}
