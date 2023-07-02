using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollEnemy : Enemys_Template
{
    [SerializeField] Rigidbody rb;
    [SerializeField] CharactorState_Data_SO so;
    [SerializeField] Animator animator;
    [SerializeField] AnimState_SO anim_so;
    [SerializeField] float distance = 10.0f;
    [SerializeField] float angle = 50.0f;

    void Start()
    {
        Set_CharaStatus(so, rb, animator, anim_so);
    }

    void Update()
    {
        if(Target_Attack)
        {
            Look_Target_Range_Angle(distance, angle);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var target = other.gameObject.GetComponent<PlayerController>();
        if (target != null)
            Target_Attack = target;
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<PlayerController>() == Target_Attack)
            Target_Attack = null;
    }

    public override void Hp_NoLife()
    {
        Destroy(gameObject);
    }
    public override void Cmd_Attack()
    {

    }
    public override void Cmd_Skill()
    {

    }
    public override void Cmd_Special()
    {

    }
}
