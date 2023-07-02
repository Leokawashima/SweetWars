using UnityEngine;

[CreateAssetMenu(menuName = "Action/Attack/NormalAttack")]
public class Attack_NormalAttack : Attack_Base_SO
{
    [SerializeField] float range = 5.0f;

    public override void Action(Charactor_Template chara_)
    {
        chara_.CharaAnim.SetFloat("ActionHash", 0);
        chara_.CharaAnim.CrossFade(chara_.Anim_SO.Anims[1]);

        Ray ray = new Ray(chara_.Position, chara_.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * range, Color.red);
        if(Physics.Raycast(ray, out RaycastHit hit, range, Charactor_Template.Ray_Chara_Layer))
        {
            chara_.Target_Hp_Damage(chara_.Attack, hit.collider.GetComponent<Charactor_Template>());
#if UNITY_EDITOR
            ExDebug.Log("与ダメ：" + chara_.Attack, Color.cyan);
#endif
        }
    }
}
