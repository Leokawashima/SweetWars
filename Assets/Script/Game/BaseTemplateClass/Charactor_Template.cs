using UnityEngine;

/// <summary>
/// キャラクタ抽象定義クラス
/// </summary>
public abstract class Charactor_Template : DynamicObject_Base
{
    #region キャラデータ変数
    public CharactorState_Data_SO CharaState { get; private set; }
    public float Buff_Attack { get; private set; }
    public float Buff_Difence { get; private set; }
    public float Buff_Speed { get; private set; }
    public float Buff_Critical_Damage { get; private set; }
    public float Buff_Critical_Chance { get; private set; }
    #endregion

    #region ターゲット
    public DynamicObject_Base Target_Attack { get; protected set; }
    public Charactor_Template Target_Surpport { get; protected set; }
    #endregion

    #region アニメーション
    public Animator CharaAnim { get; private set; }
    public AnimState_SO Anim_SO { get; private set; }
    #endregion

    #region キャラアップデート
    public delegate void CharactorEvent(Charactor_Template chara_);
    public CharactorEvent CharaUpdate { get; private set; }
    public CharactorEvent MoveUpdate { get; private set; }
    public CharactorEvent MoveStopCallBack { get; private set; }
    #endregion

    #region ゲッター＆定数
    public const float RotSpeed = 25;
    public static int Ray_Chara_Layer { get { return 1 << 11; } }
    public float Attack { get { return CharaState.Attack * Buff_Attack; } }
    public float Difence { get { return CharaState.Difence * Buff_Difence;} }
    public float MoveSpeed { get { return CharaState.Speed * Buff_Speed; } }
    public float Critical_Damage { get { return CharaState.Critical_Damage * Buff_Critical_Damage;} }
    public float Critical_Chance { get { return CharaState.Critical_Chance * Buff_Critical_Chance;} }
    #endregion

    #region 戻り値関数
    public Vector3 Target_Angle(DynamicObject_Base target_)
    {
        return new Vector3(target_.Position.x - Position.x, 0, target_.Position.z - Position.z);
    }
    public float Target_Distance(DynamicObject_Base target_)
    {
        return Target_Angle(target_).magnitude;
    }
    public Vector3 Target_Nomalized(DynamicObject_Base target_)
    {
        return Target_Angle(target_).normalized;
    }
    #endregion

    #region 設定関数
    //Charactor_Dataクラスに変えてデータをセットする
    public void Set_CharaStatus(
        CharactorState_Data_SO chara_so_, Rigidbody rb_, Animator anim_, AnimState_SO anim_SO_,
        CharactorEvent moveUpdate_, CharactorEvent callback_)
    {
        this.CharaState = chara_so_;
        this.Name = chara_so_.Name;
        this.Rb = rb_;
        this.Hp = chara_so_.Hp;
        this.Hp_Max = chara_so_.Hp;
        this.CharaAnim = anim_;
        this.Anim_SO = anim_SO_;
        this.CharaUpdate = moveUpdate_;
        this.MoveUpdate = moveUpdate_;
        this.MoveStopCallBack = callback_;
        //ボスの第二形態等を想定してデータセットしたときはバフをリセットさせる
        this.Buff_Reset();
    }
    public void Set_CharaUpdate(CharactorEvent update_)
    {
        MoveStopCallBack?.Invoke(this);
        CharaUpdate = update_;
    }
    public void Set_MoveUpdate()
    {
        CharaUpdate = MoveUpdate;
    }
    public void Add_CharaUpdate(CharactorEvent update_)
    {
        CharaUpdate += update_;
    }
    public void RemoveUpdate(CharactorEvent update_)
    {
        CharaUpdate -= update_;
    }
    #endregion

    #region ダメージ関数
    //オーバーロードしないで関数名で明示する
    //完全個人製作で誰も見ないソースならオーバーロードするかも
    public void Target_Hp_Damage(float pow_, DynamicObject_Base target_)
    {
        target_.Hp_Damage(pow_);
    }
    #endregion

    #region バフ関数
    public void Buff_Reset()
    {
        this.Buff_Attack = 1.0f;
        this.Buff_Difence = 1.0f;
        this.Buff_Critical_Damage = 1.0f;
    }
    public void Buff_Set_Attack(float buff_)
    {
        this.Buff_Attack = buff_;
    }
    public void Buff_Set_Difence(float buff_)
    {
        this.Buff_Difence = buff_;
    }
    public void Buff_Set_Critical_Damage(float buff_)
    {
        this.Buff_Critical_Chance = buff_;
    }
    public void Buff_Set_Critical_Chance(float buff_)
    {
        this.Buff_Critical_Chance = buff_;
    }
    public void Buff_Mul_Attack(float buff_)
    {
        this.Buff_Attack *= buff_;
    }
    public void Buff_Mul_Difence(float buff_)
    {
        this.Buff_Difence *= buff_;
    }
    public void Buff_Mul_Critical_Damage(float buff_)
    {
        this.Buff_Critical_Damage *= buff_;
    }
    public void Buff_mul_Critical_Chance(float buff_)
    {
        this.Buff_Critical_Chance *= buff_;
    }
    #endregion

    #region 物理関数
    public void Rb_Stop()
    {
        Rb.velocity = Vector3.zero;
    }
    public void Rb_Move(Vector2 vec2_)
    {
        Rb.velocity = new Vector3(vec2_.x * CharaState.Speed, Rb.velocity.y, vec2_.y * CharaState.Speed);
    }
    public void Rb_Move(Vector3 vec3_)
    {
        Rb.velocity = new Vector3(vec3_.x * CharaState.Speed, Rb.velocity.y, vec3_.z * CharaState.Speed);
    }
    public void Rb_Move_3Dim(Vector3 vec3_)
    {

    }
    public void Rb_Move_Impulse(Vector3 vec3_, float pow_)
    {
        Rb.AddForce(vec3_ * pow_, ForceMode.Impulse);
    }
    #endregion

    #region 視点関数
    public void Look_Dir_AllReady(Vector3 dir_)
    {
        transform.rotation = Quaternion.LookRotation(dir_, Vector3.up);
    }
    public void Look_Dir_MoveOnly(Vector3 dir_, Vector3 move_)
    {
        if (move_.magnitude != 0)
        {
            transform.rotation = Quaternion.LookRotation(dir_, Vector3.up);
        }
    }
    public void Look_Target_AllReady()
    {
        transform.rotation = Quaternion.LookRotation(Target_Angle(Target_Attack), Vector3.up);
    }
    public void Look_Target_MoveOnly(Vector3 move_)
    {
        if (move_.magnitude != 0)
        {
            this.transform.rotation = Quaternion.LookRotation(Target_Angle(Target_Attack), Vector3.up);
        }
    }

    public void Look_Target_Range(float dist_)
    {
        if(Target_Distance(Target_Attack) <= dist_)
        {
            transform.rotation = Quaternion.LookRotation(Target_Angle(Target_Attack), Vector3.up);
        }
    }
    public void Look_Target_Range_Angle(float dist_, float angle_)
    {
        if (Target_Distance(Target_Attack) <= dist_)
        {
            if (Vector3.Angle(this.transform.forward, Target_Angle(Target_Attack)) < angle_)
            {
                transform.rotation = Quaternion.LookRotation(Target_Angle(Target_Attack), Vector3.up);
            }
        }
    }
    #endregion
    public bool Check_Look_Angle(float angle_)
    {
        return (Vector3.Angle(this.transform.forward, Target_Angle(Target_Attack)) < angle_);
    }
}