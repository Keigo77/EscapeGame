using UnityEngine;

public class LifeManager : MonoBehaviour
{
    [SerializeField] private int _maxHp = 10;

    public void TakeDamage()
    {
        _maxHp--;
        Debug.Log("ダメージ！");
    }
}
