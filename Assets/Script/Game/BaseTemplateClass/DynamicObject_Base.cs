using UnityEngine;

/// <summary>
/// 動的オブジェクト抽象基底クラス
/// </summary>
/// /// interface化しない理由は当然ながら継承先で多大な変数宣言を行いたくないためと、
/// 同時多重継承はヒューマンエラーが起きやすいため。
/// Hpなどしか持たない壁等にもターゲットをかけたかったのと、
/// キャラクタクラスと別で壁クラスや無機物Objクラスを作って管理したくなかったため。
public abstract class DynamicObject_Base : MonoBehaviour
{
    public Rigidbody Rb { get; protected set; }
    public string Name { get; protected set; }
    public float Hp { get; protected set; }
    public float Hp_Max { get; protected set; }
    public float Speed { get; protected set; }

    public Vector3 Position { get { return transform.position; } }

    //ディフェンスを持たせるか正直迷った
    //無機物系といっても車だったりスライムみたいな通常攻撃は効かないものに対する対策が悩ましい

    public abstract void Hp_NoLife();

    public void Hp_Check_Over()
    {
        if(this.Hp > this.Hp_Max)
        {
            this.Hp = this.Hp_Max;
        }
    }
    public void Hp_Check_Death()
    {
        if(this.Hp <= 0)
        {
            this.Hp_NoLife();
        }
    }

    public void Hp_Heal(float pow_)
    {
        this.Hp += pow_;
        this.Hp_Check_Over();
    }
    public void Hp_Damage(float pow_)
    {
        this.Hp -= pow_;
        this.Hp_Check_Death();
    }
}
