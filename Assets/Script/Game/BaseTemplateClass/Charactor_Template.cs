using UnityEngine;

/// <summary>
/// キャラクタ抽象定義クラス
/// </summary>
public abstract class Charactor_Template : DynamicObject_Base
{
    public float Attack { get; private set; }
    public float Difence { get; private set; }
    public float Critical_Damage { get; private set; }
    public float Critical_Chance_Def { get; private set; }
    public float Buff_Attack { get; private set; }
    public float Buff_Difence { get; private set; }
    public float Buff_Speed { get; private set; }
    public float Buff_Critical_Damage { get; private set; }
    public float Buff_Critical_Chance { get; private set; }

    public DynamicObject_Base Target_Attack { get; protected set; }
    public Charactor_Template Target_Surpport { get; protected set; }

    public const float RotSpeed = 25;
    public static int Ray_Chara_Layer { get { return 1 << 11; } }
    public float Attack_Pow { get { return Attack * Buff_Attack; } }
    public float Difence_Pow { get { return Difence * Buff_Difence;} }
    public float Speed_Pow { get { return Speed * Buff_Speed; } }
    public float Critical_Pow { get { return Critical_Damage * Buff_Critical_Damage;} }
    public float Critical_Chance { get { return Critical_Chance_Def * Buff_Critical_Chance;} }

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

    public void Set_CharaStatus(CharaState_SO chara_so_, Rigidbody rb_)
    {
        this.Name = chara_so_.Name;
        this.Rb = rb_;
        this.Hp = chara_so_.Hp;
        this.Hp_Max = chara_so_.Hp;
        this.Attack = chara_so_.Attack;
        this.Difence = chara_so_.Difence;
        this.Speed = chara_so_.Speed;
        this.Critical_Damage = chara_so_.Critical_Damage;
        this.Critical_Chance_Def = chara_so_.Critical_Chance;
        //ボスの第二形態等を想定してデータセットしたときはバフをリセットさせる
        this.Buff_Reset();
    }

    //オーバーロードしないで関数名で明示する
    //完全個人製作で誰も見ないソースならオーバーロードするかも
    public void Target_Hp_Damage(float pow_, DynamicObject_Base target_)
    {
        target_.Hp_Damage(pow_);
    }

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

    public void Rb_Move(float x_, float z_)
    {
        Rb.velocity = new Vector3(x_ * Speed, Rb.velocity.y, z_ * Speed);
    }
    public void Rb_Move(float x_, float z_, float mulSpeed_)
    {
        Rb.velocity = new Vector3(x_ * Speed * mulSpeed_, Rb.velocity.y, z_ * Speed * mulSpeed_);
    }
    public void Rb_Move(Vector2 vec2_)
    {
        Rb.velocity = new Vector3(vec2_.x * Speed, Rb.velocity.y, vec2_.y * Speed);
    }
    public void Rb_Move(Vector2 vec2_, float mulSpeed_)
    {
        Rb.velocity = new Vector3(vec2_.x * Speed * mulSpeed_, Rb.velocity.y, vec2_.y * Speed * mulSpeed_);
    }
    public void Rb_Move(Vector3 vec3_)
    {
        Rb.velocity = new Vector3(vec3_.x * Speed, Rb.velocity.y, vec3_.z * Speed);
    }
    public void Rb_Move(Vector3 vec3_, float mulSpeed_)
    {
        Rb.velocity = new Vector3(vec3_.x * Speed * mulSpeed_, Rb.velocity.y, vec3_.z * Speed * mulSpeed_);
    }

    public void Rb_Move_Orizin(float x_, float z_)
    {
        Rb.velocity = new Vector3(x_, Rb.velocity.y, z_);
    }
    public void Rb_Move_Orizin(float x_, float z_, float speed_)
    {
        Rb.velocity = new Vector3(x_ * speed_, Rb.velocity.y, z_ * speed_);
    }
    public void Rb_Move_Orizin(Vector2 vec2_)
    {
        Rb.velocity = new Vector3(vec2_.x, Rb.velocity.y, vec2_.y);
    }
    public void Rb_Move_Orizin(Vector2 vec2_, float speed_)
    {
        Rb.velocity = new Vector3(vec2_.x * speed_, Rb.velocity.y, vec2_.y * speed_);
    }
    public void Rb_Move_Orizin(Vector3 vec3_)
    {
        Rb.velocity = new Vector3(vec3_.x, Rb.velocity.y, vec3_.z);
    }
    public void Rb_Move_Orizin(Vector3 vec3_, float speed_)
    {
        Rb.velocity = new Vector3(vec3_.x * speed_, Rb.velocity.y, vec3_.z * speed_);
    }

    public void Rb_Move_3Dim(float x_, float y_, float z_)
    {
        Rb.velocity = new Vector3(x_ * Speed, y_ * Speed, z_ * Speed);
    }
    public void Rb_Move_3Dim(float x_, float y_, float z_, float mulSpeed_)
    {
        Rb.velocity = new Vector3(x_ * Speed * mulSpeed_, y_ * Speed * mulSpeed_, z_ * Speed * mulSpeed_);
    }
    public void Rb_Move_3Dim(Vector3 vec3_)
    {
        Rb.velocity = vec3_ * Speed;
    }
    public void Rb_Move_3Dim(Vector3 vec3_, float mulSpeed_)
    {
        Rb.velocity = vec3_ * Speed * mulSpeed_;
    }

    public void Rb_Move_3Dim_Orizin(float x_, float y_, float z_)
    {
        Rb.velocity = new Vector3(x_, y_, z_);
    }
    public void Rb_Move_3Dim_Orizin(float x_, float y_, float z_, float speed_)
    {
        Rb.velocity = new Vector3(x_ * speed_, y_ * speed_, z_ * speed_);
    }
    public void Rb_Move_3Dim_Orizin(Vector3 vec3_)
    {
        Rb.velocity = vec3_;
    }
    public void Rb_Move_3Dim_Orizin(Vector3 vec3_, float speed_)
    {
        Rb.velocity = vec3_ * speed_;
    }

    public void Look_Dir_AllReady(Vector3 dir_)
    {
        transform.rotation = Quaternion.LookRotation(dir_, Vector3.up);
    }
    public void Look_Dir_MoveOnly(Vector3 dir_, Vector3 moveVec3_)
    {
        if (moveVec3_.magnitude != 0)
        {
            transform.rotation = Quaternion.LookRotation(dir_, Vector3.up);
        }
    }
    public void Look_Target_AllReady()
    {
        transform.rotation = Quaternion.LookRotation(Target_Angle(Target_Attack), Vector3.up);
    }
    public void Look_Target_MoveOnly(Vector3 moveVec3_)
    {
        if (moveVec3_.magnitude != 0)
        {
            this.transform.rotation = Quaternion.LookRotation(Target_Angle(Target_Attack), Vector3.up);
        }
    }

    public void Look_Target_Range(float distance_)
    {
        if(Target_Distance(Target_Attack) <= distance_)
        {
            transform.rotation = Quaternion.LookRotation(Target_Angle(Target_Attack), Vector3.up);
        }
    }
    public void Look_Target_Range_Angle(float distance_, float angle_)
    {
        if (Target_Distance(Target_Attack) <= distance_)
        {
            if (Vector3.Angle(this.transform.forward, Target_Angle(Target_Attack)) < angle_)
            {
                transform.rotation = Quaternion.LookRotation(Target_Angle(Target_Attack), Vector3.up);
            }
        }
    }
}