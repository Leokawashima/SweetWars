using UnityEngine;

/// <summary>
/// Attackの抽象基底SO
/// </summary>
public abstract class Attack_Base_SO : ScriptableObject
{
    public enum AttackTypeState
    {
        Normal,
        Triple,
        LongRange
    }
    public AttackTypeState AttackType;

    public virtual void Action()
    {
        ExDebug.Log(nameof(this.name) + "のActionが上書きされてないよ", Color.red);
    }
}

/// <summary>
/// Skillの抽象基底SO
/// </summary>
public abstract class Skill_Base_SO : ScriptableObject
{
    public enum SkillTypeState
    {
        Pressed,
        Hold,
        Charge,
    }
    public SkillTypeState EventType;

    public virtual void Started()
    {
        ExDebug.Log(nameof(this.name) + "のPressedが上書きされてないよ", Color.green);
    }
    public virtual void Performed()
    {
        ExDebug.Log(nameof(this.name) + "のPerformedが上書きされてないよ", Color.green);
    }
    public virtual void Canceled()
    {
        ExDebug.Log(nameof(this.name) + "のCanceledが上書きされてないよ", Color.green);
    }
}

/// <summary>
/// Specialの抽象基底SO
/// </summary>
public abstract class SPecial_Base_SO : ScriptableObject
{
    public virtual void Action()
    {
        ExDebug.Log(nameof(this.name) + "のActionが上書きされてないよ", Color.blue);
    }
}