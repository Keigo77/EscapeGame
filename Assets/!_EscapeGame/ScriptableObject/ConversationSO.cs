using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ConversationContents", menuName = "Scriptable Objects/PrologueContents")]

public class ConversationSO : ScriptableObject
{
    public List<ConversationData> ConversationDatas = new ();
}

public enum Speaker
{
    Speaker1,
    Speaker2,
    Speaker3
}

[System.Serializable]
public class ConversationData
{
    public Speaker Speaker;
    [TextArea] public string Text;
    public AudioClip Se;
    public Sprite NextBackImage;
    public bool isUseFade = false;
}
