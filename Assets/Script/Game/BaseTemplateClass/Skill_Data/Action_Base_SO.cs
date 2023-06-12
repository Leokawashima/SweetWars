using UnityEngine;

/// <summary>
/// Attackの抽象基底SO
/// </summary>
[System.Serializable]
public abstract class Attack_Base_SO : ScriptableObject
{
    public enum AttackTypeState
    {
        Normal,
        Triple,
        LongRange
    }
    public AttackTypeState AttackType;

    public virtual void Action(Charactor_Template chara)
    {
#if UNITY_EDITOR
        ExDebug.Log(nameof(this.name) + "のActionが上書きされてないよ", Color.red);
#endif
    }
}

/// <summary>
/// Skillの抽象基底SO
/// </summary>
[System.Serializable]
public abstract class Action_Base_SO : ScriptableObject
{
    public enum SkillTypeState
    {
        Pressed,
        Hold,
        Charge,
    }
    public SkillTypeState EventType;

    public virtual void Started(Charactor_Template chara_)
    {
#if UNITY_EDITOR
        ExDebug.Log(nameof(this.name) + "のPressedが上書きされてないよ", Color.green);
#endif
    }
    public virtual void Performed(Charactor_Template chara_)
    {
#if UNITY_EDITOR
        ExDebug.Log(nameof(this.name) + "のPerformedが上書きされてないよ", Color.green);
#endif
    }
    public virtual void Canceled(Charactor_Template chara_)
    {
#if UNITY_EDITOR
        ExDebug.Log(nameof(this.name) + "のCanceledが上書きされてないよ", Color.green);
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
        ExDebug.Log(nameof(this.name) + "のActionが上書きされてないよ", Color.blue);
#endif
    }
}