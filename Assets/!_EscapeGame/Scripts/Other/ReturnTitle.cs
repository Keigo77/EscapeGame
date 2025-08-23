using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ReturnTitle : MonoBehaviour
{
    [SerializeField] private SceneTransition _sceneTransition;

    void Start()
    {
        ES3.Save("HaveGoaled", true);
        MoveTitle().Forget();
    }

    private async UniTask MoveTitle()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(2.0f));
        if (PrinterTouchPanel.IsTrueEnd)
        {
            _sceneTransition.SceneName = "End_True";
        }
        _sceneTransition.SwitchScene();
    }
}
