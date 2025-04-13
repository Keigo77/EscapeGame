using System.Collections;
using UnityEngine;


public interface IMoveGimmick
{
    /// <summary>
    /// オブジェクトがクリックされたときの処理．
    /// </summary>
    public void MoveGimmick();
}

public interface IShowText
{
    /// <summary>
    /// オブジェクトをクリックした時，そのオブジェクトの説明を表示
    /// </summary>
    public void ShowExplainText();
}

public interface ISaveLoad
{
    void Save();
    void Load();
}

