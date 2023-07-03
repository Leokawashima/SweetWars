using UnityEngine;

[CreateAssetMenu(menuName = "Action/Skill/Pressed/Stamp")]
public class Skill_Pressed_Stamp : Skill_Pressed_Template
{
    [SerializeField] float upVec = 0.2f;
    private float time = 0;

    public override void Started(Charactor_Template chara_)
    {
        chara_.Rb.useGravity = false;
        var forward = chara_.transform.forward;
        chara_.Rb_Move_3Dim(new Vector3(forward.x, upVec, forward.z));
        chara_.Set_CharaUpdate(StampAction);
    }

    void StampAction(Charactor_Template chara_)
    {
        time += Time.deltaTime;
        if (time > 5)
        {
            chara_.Set_MoveUpdate();
            time = 0;
        }
    }
}