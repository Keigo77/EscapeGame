using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ReturnTitle : MonoBehaviour
{
    [SerializeField] private SceneTransition _sceneTransition;

    void Start()
    {
        MoveTitle().Forget();
    }

    private async UniTask MoveTitle()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(2.0f));
        _sceneTransition.SwitchScene();
    }
}
