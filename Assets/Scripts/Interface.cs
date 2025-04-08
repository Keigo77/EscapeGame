using System.Collections;
using UnityEngine;


public interface IMoveGimick
{
    /// <summary>
    /// オブジェクトがクリックされたときの処理．
    /// </summary>
    public void MoveGimick();
}

public interface IShowText
{
    public void ShowExplainText();
}

public interface ISaveLoad
{
    void Save();
    void Load();
}

