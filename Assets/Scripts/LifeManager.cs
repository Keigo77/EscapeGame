using UnityEngine;

public class LifeManager : MonoBehaviour
{
    [SerializeField] private int _maxHP = 10;

    public void TakeDamage()
    {
        _maxHP--;
        Debug.Log("ダメージ！");
    }
}
