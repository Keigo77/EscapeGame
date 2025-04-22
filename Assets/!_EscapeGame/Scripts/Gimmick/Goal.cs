using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Goal : MonoBehaviour, IMoveGimmick
{
    [SerializeField] private SceneTransition _sceneTransition;
    [SerializeField] private GameObject _alphaPanel;    // クリック操作を不能にする
    private CancellationToken _token;

    void Awake()
    {
        _token = this.GetCancellationTokenOnDestroy();
    }

    public void MoveGimmick()
    {
        SwitchResultScene().Forget();
    }
    
    private async UniTask SwitchResultScene()
    {
        await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0), cancellationToken: _token);  // テキストを進めてから
        _alphaPanel.SetActive(true);
        await UniTask.Delay(TimeSpan.FromSeconds(1.0f), cancellationToken: _token);
        _sceneTransition.SwitchScene();
    }
}
