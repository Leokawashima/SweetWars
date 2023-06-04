﻿using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Charactor_Template
{
    #region Valiables
    [Header("アタッチ欄")]
    [SerializeField] PlayerManager playerManager;
    [SerializeField] Rigidbody rb;
    [SerializeField] Animator animator;
    [Header("SO")]
    [SerializeField] CharaState_SO chara_so;
    [SerializeField] AnimState_SO anim_SO;
    [SerializeField] Skill_Base_SO skill_SO_1;
    [SerializeField] Skill_Base_SO skill_SO_2;
    [Header("UI")]
    [SerializeField] GameObject esc_UI;
    [SerializeField] GameObject menu_UI;
    [SerializeField] GameObject forcus_UI;
    [Header("速度")]
    private const float dashSpeed = 1.8f;

    Quaternion cameraAngle = Quaternion.identity;

    #endregion Valiables

    #region EventCallbacks
    void OnEnable()
    {
        playerManager.Move_Started += OnMove_Started;
        playerManager.Move_Performed += OnMove_Performed;
        playerManager.Move_Canceled += OnMove_Canceled;
        playerManager.Look_Started += OnLook_Started;
        playerManager.Look_Performed += OnLook_Performed;
        playerManager.Look_Canceled += OnLook_Canceled;
        playerManager.Sprint_Started += OnSprint_Started;
        playerManager.Sprint_Canceled += OnSprint_Canceled;
        playerManager.Cmd_Esc += OnCmd_Esc;
        playerManager.Cmd_Menu += OnCmd_Menu;
        playerManager.Cmd_Forcus += OnCmd_Forcus;
        playerManager.Cmd_Attack += OnCmd_Attack;
        playerManager.Cmd_Skill_First_Pressed += OnCmd_Skill_First_Pressed;
        playerManager.Cmd_Skill_First_Performed += OnCmd_Skill_First_Performed;
        playerManager.Cmd_Skill_First_Canceled += OnCmd_Skill_First_Canceled;
        playerManager.Cmd_Skill_Second_Pressed += OnCmd_Skill_Second_Pressed;
        playerManager.Cmd_Skill_Second_Performed += OnCmd_Skill_Second_Performed;
        playerManager.Cmd_Skill_Second_Canceled += OnCmd_Skill_Second_Canceled;
        playerManager.Cmd_Special += OnCmd_Special;
    }
    void OnDisable()
    {
        playerManager.Move_Started -= OnMove_Started;
        playerManager.Move_Performed -= OnMove_Performed;
        playerManager.Move_Canceled -= OnMove_Canceled;
        playerManager.Look_Started -= OnLook_Started;
        playerManager.Look_Performed -= OnLook_Performed;
        playerManager.Look_Canceled -= OnLook_Canceled;
        playerManager.Sprint_Started -= OnSprint_Started;
        playerManager.Sprint_Canceled -= OnSprint_Canceled;
        playerManager.Cmd_Esc -= OnCmd_Esc;
        playerManager.Cmd_Menu -= OnCmd_Menu;
        playerManager.Cmd_Forcus -= OnCmd_Forcus;
        playerManager.Cmd_Attack -= OnCmd_Attack;
        playerManager.Cmd_Skill_First_Pressed -= OnCmd_Skill_First_Pressed;
        playerManager.Cmd_Skill_First_Performed -= OnCmd_Skill_First_Performed;
        playerManager.Cmd_Skill_First_Canceled -= OnCmd_Skill_First_Canceled;
        playerManager.Cmd_Skill_Second_Pressed -= OnCmd_Skill_Second_Pressed;
        playerManager.Cmd_Skill_Second_Performed -= OnCmd_Skill_Second_Performed;
        playerManager.Cmd_Skill_Second_Canceled -= OnCmd_Skill_Second_Canceled;
        playerManager.Cmd_Special -= OnCmd_Special;
    }
    void OnMove_Started()
    {
        AnimSet_Move();
    }
    //↓中身ナシ
    void OnMove_Performed()
    {

    }
    void OnMove_Canceled()
    {
        AnimSet_Move();
    }
    //↓中身ナシ
    void OnLook_Started()
    {

    }
    //↓中身ナシ
    void OnLook_Performed()
    {

    }
    //↓中身ナシ
    void OnLook_Canceled()
    {

    }
    void OnSprint_Started()
    {
        AnimSet_Move();
    }
    void OnSprint_Canceled()
    {
        AnimSet_Move();
    }
    void OnCmd_Esc()
    {
        esc_UI.SetActive(!esc_UI.activeSelf);
    }
    void OnCmd_Menu()
    {
        menu_UI.SetActive(!menu_UI.activeSelf);
    }
    void OnCmd_Forcus()
    {
        forcus_UI.SetActive(!forcus_UI.activeSelf);
    }

    //ここら辺をCharaFormatで抽象定義して敵と共通化しなかった理由は、
    //設計上敵はボス含め複数のスキル、必殺技を持つ可能性があること。
    //プレイヤは基本的にゲーム開始時に持ち込むスキル等は固定されていて数が上限しないこと。
    //仮に複数必殺や複数スキルを持たせるのならボタンやキー配置の都合上かなり難儀になる可能性があり、
    //スキルをそこまで持てる場合キャラチョイスによる差を設けることが難しいこと。
    //以上が挙げられる。　基本的に技や必殺はアセット管理するので結局やろうと思えば大体全部親クラスに纏まるが、
    //現状のゲーム設計ではスキルは二個までSPは一個までを前提として設計。
    void OnCmd_Attack()
    {
        Debug.Log("Attack");
        animator.SetFloat("ActionHash", 0);
        animator.CrossFade(anim_SO.Anims[1]);

        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * 5.0f, Color.red);
        if(Physics.Raycast(ray, out RaycastHit hit, 5.0f, Ray_Chara_Layer))
        {
            Target_Hp_Damage(Attack_Pow, hit.collider.GetComponent<Charactor_Template>());
        }
    }
    void OnCmd_Skill_First_Pressed()
    {
        skill_SO_1.Started();
    }
    void OnCmd_Skill_First_Performed()
    {
        skill_SO_1.Performed();
    }
    void OnCmd_Skill_First_Canceled()
    {
        skill_SO_1.Canceled();
    }
    void OnCmd_Skill_Second_Pressed()
    {
        skill_SO_2.Started();
    }
    void OnCmd_Skill_Second_Performed()
    {
        skill_SO_2.Performed();
    }
    void OnCmd_Skill_Second_Canceled()
    {
        skill_SO_2.Canceled();
    }
    void OnCmd_Special()
    {
        Debug.Log("Special");
    }
    #endregion InputSystems

    #region Function
    void AnimSet_Move()
    {
        float hash = 0;
        if(playerManager.InputStatus[(int)PlayerManager.InputState.Move])
        {
            if(playerManager.InputStatus[(int)PlayerManager.InputState.Sprint])
            {
                hash = 1;
            }
            else
            {
                hash = 0.5f;
            }
        }
        animator.SetFloat("Move", hash);
    }
    public override void Hp_NoLife()
    {
        Destroy(gameObject);
    }
    #endregion Function

    #region ComponentIvent
    void Start()
    {
        Set_CharaStatus(chara_so, rb);
        cameraAngle = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);
    }
    void Update()
    {
        var velocity = cameraAngle * playerManager.MoveToVec3.normalized;
        var moveSpeed = playerManager.InputStatus[(int)PlayerManager.InputState.Sprint] ? dashSpeed : 1;
        var moveVec = new Vector2(velocity.x * moveSpeed, velocity.z * moveSpeed);

        Rb_Move(moveVec);

        if(velocity.magnitude > 0)
        {
            var mDisaire = Mathf.Atan2(playerManager.Move.x, playerManager.Move.y) * Mathf.Rad2Deg;
            var targetRot = Quaternion.Euler(0, mDisaire, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, RotSpeed * Time.deltaTime);
        }
    }
    #endregion
}