using UnityEngine;

[CreateAssetMenu(menuName = "Charactor_SO")]
public class CharactorState_Data_SO : ScriptableObject
{
    public string Name;
    public float Hp;
    public float Attack;
    public float Difence;
    public float Speed = 12.0f;
    public float Critical_Damage = 7.0f;
    public float Critical_Chance = 1.5f;
    public Attack_Base_SO Attack_SO;
    public Action_Base_SO[] Skill_SO;
    public Special_Base_SO Special_SO;
}