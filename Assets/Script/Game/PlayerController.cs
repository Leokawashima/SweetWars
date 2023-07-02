using UnityEngine;

public class PlayerController : Charactor_Template
{
    #region Valiables
    [Header("アタッチ欄")]
    [SerializeField] PlayerManager playerManager;
    [SerializeField] Rigidbody rb;
    [Header("SO")]
    [SerializeField] CharactorState_Data_SO chara_so;
    [SerializeField] AnimState_SO anim_SO;
    [SerializeField] Animator animator;
    [Header("UI")]
    [SerializeField] GameObject esc_UI;
    [SerializeField] GameObject menu_UI;
    [SerializeField] GameObject forcus_UI;
    [Header("速度")]
    const float dashSpeed = 1.8f;

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

    void OnCmd_Attack()
    {
        chara_so.Attack_SO.Action(this);
    }
    void OnCmd_Skill_First_Pressed()
    {
        chara_so.Skill_SO[0].Started(this);
    }
    void OnCmd_Skill_First_Performed()
    {
        chara_so.Skill_SO[0].Performed(this);
    }
    void OnCmd_Skill_First_Canceled()
    {
        chara_so.Skill_SO[0].Canceled(this);
    }
    void OnCmd_Skill_Second_Pressed()
    {
        chara_so.Skill_SO[1].Started(this);
    }
    void OnCmd_Skill_Second_Performed()
    {
        chara_so.Skill_SO[1].Performed(this);
    }
    void OnCmd_Skill_Second_Canceled()
    {
        chara_so.Skill_SO[1].Canceled(this);
    }
    void OnCmd_Special()
    {
        chara_so.Special_SO.Action(this);
    }
    #endregion InputSystems

    #region Function
    void AnimSet_Move()
    {
        //アニメーションを割り当てるための仮実装
        //後々アニメーションのハッシュを取得して綺麗に整える
        float hash = 0;
        float speed = 0;
        if(playerManager.InputStatus[(int)PlayerManager.InputState.Move])
        {
            hash = 1;
            if(playerManager.InputStatus[(int)PlayerManager.InputState.Sprint])
            {
                speed = 2;
            }
            else
            {
                speed = 1;
            }
        }
        animator.SetFloat("Move", hash);
        animator.SetFloat("Speed", speed);
    }
    void Move()
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
    public override void Hp_NoLife()
    {
        Destroy(gameObject);
    }
    #endregion Function

    #region ComponentIvent
    void Start()
    {
        Set_CharaStatus(chara_so, rb, animator, anim_SO, Move);
        cameraAngle = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);
    }
    void Update()
    {
        CharaUpdate();
    }
    #endregion
}