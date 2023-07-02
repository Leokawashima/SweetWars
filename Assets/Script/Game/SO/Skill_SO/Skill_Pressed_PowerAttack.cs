using UnityEngine;

[CreateAssetMenu(menuName = "Action/Skill/Pressed/PowerAttack")]
public class Skill_Pressed_PowerAttack : Skill_Pressed_Template
{
    public override void Started(Charactor_Template chara_)
    {
        chara_.Rb_Move_Impulse(chara_.transform.forward, 100);
        chara_.CharaAnim.SetFloat("ActionHash", 0.5f);
        chara_.CharaAnim.CrossFade(chara_.Anim_SO.Anims[1]);
    }
}
