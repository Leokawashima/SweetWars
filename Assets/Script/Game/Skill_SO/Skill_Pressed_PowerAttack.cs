using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Skill_P_PowerAttack")]
public class Skill_Pressed_PowerAttack : Skill_Pressed_Template
{
    public override void Started()
    {
#if UNITY_EDITOR
        ExDebug.Log(this.ToString() + "::" + CoolDownTime, Color.red);
#endif
        //Debug.Log("Skill_1");
        //Rb.AddForce(transform.forward * 100.0f, ForceMode.Impulse);
        //animator.SetFloat("ActionHash", 0.5f);
        //animator.CrossFade(anim_SO.Anims[1]);

        //Debug.Log("Skill_2");
        //animator.SetFloat("ActionHash", 1);
        //animator.CrossFade(anim_SO.Anims[1]);
    }
}
