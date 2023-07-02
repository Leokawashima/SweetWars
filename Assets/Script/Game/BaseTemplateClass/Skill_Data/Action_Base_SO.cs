using UnityEngine;

/// <summary>
/// Attackの抽象基底SO
/// </summary>
[System.Serializable]
public abstract class Attack_Base_SO : ScriptableObject
{
    public virtual void Action(Charactor_Template chara)
    {
#if UNITY_EDITOR
        ExDebug.Log(this.name.ToString() + "のActionが上書きされてないよ", Color.red);
#endif
    }
}

/// <summary>
/// Skillの抽象基底SO
/// </summary>
[System.Serializable]
public abstract class Action_Base_SO : ScriptableObject
{
    public virtual void Started(Charactor_Template chara_)
    {
#if UNITY_EDITOR
        ExDebug.Log(this.name.ToString() + "のPressedが上書きされてないよ", Color.green);
#endif
    }
    public virtual void Performed(Charactor_Template chara_)
    {
#if UNITY_EDITOR
        ExDebug.Log(this.name.ToString() + "のPerformedが上書きされてないよ", Color.green);
#endif
    }
    public virtual void Canceled(Charactor_Template chara_)
    {
#if UNITY_EDITOR
        ExDebug.Log(this.name.ToString() + "のCanceledが上書きされてないよ", Color.green);
#endif
    }
}

/// <summary>
/// Specialの抽象基底SO
/// </summary>
[System.Serializable]
public abstract class Special_Base_SO : ScriptableObject
{
    public virtual void Action(Charactor_Template chara_)
    {
#if UNITY_EDITOR
        ExDebug.Log(this.name.ToString() + "のActionが上書きされてないよ", Color.blue);
#endif
    }
}