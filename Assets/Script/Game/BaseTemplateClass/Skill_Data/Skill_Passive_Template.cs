using UnityEngine;

/// <summary>
/// PassiveSkillの抽象定義クラス
/// </summary>
/// パッシブスキルとして発動条件、効果等があまりにも広義すぎるので、
/// Baseクラスとしてキャラクタの各イベント毎に追加したいものをTemplateとして随時追加して作っていきたい。
/// 例えば、死んでも一回復活できるとか、死んだら爆弾を落とすとか。
/// 攻撃したら火属性になるとか、受けるダメージを半減するとか。
/// 全体的にいろんなものをイベント化してそこに登録したいイベントに登録する用の関数を各自実装する感じで。
public abstract class PassiveSkill_Base_SO: ScriptableObject
{
    public enum IventTypeState
    {
        state,
        Dead,
    }
    public IventTypeState IventType;
}
