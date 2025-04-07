using System.Collections;
using UnityEngine;


public interface IMoveGimick
{
    /// <summary>
    /// オブジェクトがクリックされたときの処理．
    /// </summary>
    public void MoveGimick();
}

public interface ICorrect
{
    /// <summary>
    /// 謎を解いたときに実行．
    /// </summary>
    public void Correct();
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

