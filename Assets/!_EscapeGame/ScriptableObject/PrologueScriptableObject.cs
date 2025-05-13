using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConversationContents", menuName = "Scriptable Objects/PrologueContents")]

public class PrologueScriptableObject : ScriptableObject
{
    public List<ConversationContents> ConversationContentsData = new ();
}

[System.Serializable]
public class ConversationContents
{
    public string TakerName;
    [TextArea] public string Text;
    public AudioClip Se;
    public Sprite BackImage;
    public bool isUseFade = false;
}
