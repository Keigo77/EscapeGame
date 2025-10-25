using UnityEngine;

public class SkipStory : MonoBehaviour
{
    [SerializeField] private SceneTransition _sceneTransition;
    
    public void SkipStoryButtonPushed()
    {
        _sceneTransition.SceneName = "EscapeScene";
        _sceneTransition.SwitchScene();
    }
}
