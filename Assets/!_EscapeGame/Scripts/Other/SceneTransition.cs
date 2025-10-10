using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private float _fadeOutTime = 1.0f;
    [SerializeField] private float _fadeInTime = 1.0f;
    public string SceneName;
    private Fade _fade;

    private void Awake()
    {
        _fade = this.GetComponent<Fade>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //シーン開始時にフェードを掛ける
        _fade.FadeOut(_fadeOutTime);
    }

    //各ボタンを押した時の処理
    public void SwitchScene()
    {
        //フェードを掛けてからシーン遷移する
        _fade.FadeIn(_fadeInTime, () =>
        {
            SceneManager.LoadScene(SceneName);
        });
    }
}
