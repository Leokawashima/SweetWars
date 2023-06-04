using UnityEngine;

[CreateAssetMenu(menuName = "Charactor_SO")]
public class CharaState_SO : ScriptableObject
{
    public string Name;
    public float Hp = 100;
    public float Attack = 10;
    public float Difence = 20;
    public float Speed = 12.0f;
    public float Critical_Damage = 7.0f;
    public float Critical_Chance = 1.5f;
}