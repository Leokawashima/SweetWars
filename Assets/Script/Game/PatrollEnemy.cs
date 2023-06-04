using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollEnemy : Enemys_Template
{
    [SerializeField] Rigidbody rb;
    [SerializeField] CharaState_SO so;
    [SerializeField] float distance = 10.0f;
    [SerializeField] float angle = 50.0f;

    void Start()
    {
        Set_CharaStatus(so, rb);
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
        if(other.gameObject == Target_Attack.gameObject)
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
