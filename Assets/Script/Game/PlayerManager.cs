using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    #region PublicProperty
    public MyInputSystem MyInput { get; private set; }

    //今回は要素に順番な数値以外を設定する用途ではないので
    //最後にCountを用意してCountの大きさで初期化しても良かったが、
    //余計な要素を追加しなくても良いSystem.Enumクラスメソッドで入力確認用配列を設定
    public enum InputState
    {
        //入力しづつける可能性のある入力系を列挙しておき配列として設定して
        //コールバック関数に何が入力中かを記録しておく
        Move,
        Look,
        Sprint,
        Skill_First,
        Skill_Second
    }
    public bool[] InputStatus { get; private set; } = new bool[System.Enum.GetValues(typeof(InputState)).Length];

    public Vector2 Move { get; private set; }
    public Vector2 Look { get; private set; }

    public Vector3 MoveToVec3 { get { return new Vector3(Move.x, 0, Move.y); } }
    public Vector3 LookToVec3 { get { return new Vector3(Look.x, 0, Look.y); } }
    #endregion PublicProperty

    #region MyEvent
    public delegate void PlayerEvent();
    public event PlayerEvent Move_Started;
    public event PlayerEvent Move_Performed;
    public event PlayerEvent Move_Canceled;
    public event PlayerEvent Look_Started;
    public event PlayerEvent Look_Performed;
    public event PlayerEvent Look_Canceled;
    public event PlayerEvent Sprint_Started;
    public event PlayerEvent Sprint_Canceled;
    public event PlayerEvent Cmd_Esc;
    public event PlayerEvent Cmd_Menu;
    public event PlayerEvent Cmd_Forcus;
    public event PlayerEvent Cmd_Attack;
    public event PlayerEvent Cmd_Skill_First_Pressed;
    public event PlayerEvent Cmd_Skill_First_Performed;
    public event PlayerEvent Cmd_Skill_First_Canceled;
    public event PlayerEvent Cmd_Skill_Second_Pressed;
    public event PlayerEvent Cmd_Skill_Second_Performed;
    public event PlayerEvent Cmd_Skill_Second_Canceled;
    public event PlayerEvent Cmd_Special;
    #endregion MyEvent

    #region UnityEvent
    void Awake()
    {
        MyInput = new MyInputSystem();
    }
    void OnEnable()
    {
        var input = new MyInputSystem();
        var actions = input.Game;
        //Move
        actions.Move.started += OnMove_Started;
        actions.Move.performed += OnMove_Performed;
        actions.Move.canceled += OnMove_Canceled;
        //Look
        actions.Look.started += OnLook_Started;
        actions.Look.performed += OnLook_Performed;
        actions.Look.canceled += OnLook_Canceled;
        //Sprint
        actions.Sprint.started += OnSprint_Started;
        actions.Sprint.canceled += OnSprint_Canceled;
        //Esc
        actions.Esc.started += OnCmd_Esc;
        //Menu
        actions.Menu.started += OnCmd_Menu;
        //Forcus
        actions.Forcus.started += OnCmd_Forcus;
        //Attack
        actions.Attack.started += OnCmd_Attack;
        //Skill_First
        actions.Skill_First.started += OnCmd_Skill_First_Pressed;
        actions.Skill_First.performed += OnCmd_Skill_First_Performed;
        actions.Skill_First.canceled += OnCmd_Skill_First_Canceled;
        //Skill_Second
        actions.Skill_Second.started += OnCmd_Skill_Second_Pressed;
        actions.Skill_Second.performed += OnCmd_Skill_Second_Performed;
        actions.Skill_Second.canceled += OnCmd_Skill_Second_Canceled;
        //Special
        actions.Special.started += OnCmd_Special;

        input.Enable();
    }
    void OnDisable()
    {
        var input = new MyInputSystem();
        var actions = input.Game;
        //Move
        actions.Move.started -= OnMove_Started;
        actions.Move.performed -= OnMove_Performed;
        actions.Move.canceled -= OnMove_Canceled;
        //Look
        actions.Look.started -= OnLook_Started;
        actions.Look.performed -= OnLook_Performed;
        actions.Look.canceled -= OnLook_Canceled;
        //Sprint
        actions.Sprint.started -= OnSprint_Started;
        actions.Sprint.canceled -= OnSprint_Canceled;
        //Esc
        actions.Esc.started -= OnCmd_Esc;
        //Menu
        actions.Menu.started -= OnCmd_Menu;
        //Forcus
        actions.Forcus.started -= OnCmd_Forcus;
        //Attack
        actions.Attack.started -= OnCmd_Attack;
        //Skill_First
        actions.Skill_First.started -= OnCmd_Skill_First_Pressed;
        actions.Skill_First.performed -= OnCmd_Skill_First_Performed;
        actions.Skill_First.canceled -= OnCmd_Skill_First_Canceled;
        //Skill_Second
        actions.Skill_Second.started -= OnCmd_Skill_Second_Pressed;
        actions.Skill_Second.performed -= OnCmd_Skill_Second_Performed;
        actions.Skill_Second.canceled -= OnCmd_Skill_Second_Canceled;
        //Special
        actions.Special.started -= OnCmd_Special;

        input.Disable();
    }
    void FixedUpdate()
    {
        switch(ButtonStatus)
        {
            case ButtonState.Start:
                ButtonStart();
                ButtonStatus = ButtonState.Perform;
                break;
            case ButtonState.Perform:
                ButtonPerform();
                break;
            case ButtonState.Up:
                ButtonUp();
                ButtonStatus = ButtonState.Non;
                break;
        }
    }
    void ButtonStart()
    {
        Debug.Log("Start");
    }
    void ButtonPerform()
    {
        Debug.Log("Perform");
    }
    void ButtonUp()
    {
        Debug.Log("Up");
    }
    #endregion UnityEvent

    #region ActionEvent
    void OnMove_Started(InputAction.CallbackContext context)
    {
        Move = context.ReadValue<Vector2>();
        InputStatus[(int)InputState.Move] = true;
        Move_Started?.Invoke();
    }
    void OnMove_Performed(InputAction.CallbackContext context)
    {
        Move = context.ReadValue<Vector2>();
        Move_Performed?.Invoke();
    }
    void OnMove_Canceled(InputAction.CallbackContext context)
    {
        Move = Vector2.zero;
        InputStatus[(int)InputState.Move] = false;
        Move_Canceled?.Invoke();
    }
    void OnLook_Started(InputAction.CallbackContext context)
    {
        Look = context.ReadValue<Vector2>();
        InputStatus[(int)InputState.Look] = true;
        Look_Started?.Invoke();
    }
    void OnLook_Performed(InputAction.CallbackContext context)
    {
        Look = context.ReadValue<Vector2>();
        Look_Performed?.Invoke();
    }
    void OnLook_Canceled(InputAction.CallbackContext context)
    {
        Look = Vector3.zero;
        InputStatus[(int)InputState.Look] = false;
        Look_Canceled?.Invoke();
    }
    void OnSprint_Started(InputAction.CallbackContext context)
    {
        InputStatus[(int)InputState.Sprint] = true;
        Sprint_Started?.Invoke();
    }
    void OnSprint_Canceled(InputAction.CallbackContext context)
    {
        InputStatus[(int)InputState.Sprint] = false;
        Sprint_Canceled?.Invoke();
    }
    void OnCmd_Esc(InputAction.CallbackContext context)
    {
        Cmd_Esc?.Invoke();
    }
    void OnCmd_Menu(InputAction.CallbackContext context)
    {
        Cmd_Menu?.Invoke();
    }
    void OnCmd_Forcus(InputAction.CallbackContext context)
    {
        Cmd_Forcus?.Invoke();
    }
    void OnCmd_Attack(InputAction.CallbackContext context)
    {
        Cmd_Attack?.Invoke();
    }
    void OnCmd_Skill_First_Pressed(InputAction.CallbackContext context)
    {
        Cmd_Skill_First_Pressed?.Invoke();
    }
    void OnCmd_Skill_First_Performed(InputAction.CallbackContext context)
    {
        Cmd_Skill_First_Performed?.Invoke();
    }
    void OnCmd_Skill_First_Canceled(InputAction.CallbackContext context)
    {
        Cmd_Skill_First_Canceled?.Invoke();
    }
    void OnCmd_Skill_Second_Pressed(InputAction.CallbackContext context)
    {
        Cmd_Skill_Second_Pressed?.Invoke();
    }
    void OnCmd_Skill_Second_Performed(InputAction.CallbackContext context)
    {
        Cmd_Skill_Second_Performed?.Invoke();
    }
    void OnCmd_Skill_Second_Canceled(InputAction.CallbackContext context)
    {
        Cmd_Skill_Second_Canceled?.Invoke();
    }
    void OnCmd_Special(InputAction.CallbackContext context)
    {
        Cmd_Special?.Invoke();
    }
    #endregion ActionEvent

    public enum ButtonState
    {
        Non,
        Start,
        Perform,
        Up
    }
    public ButtonState ButtonStatus;
    public void OnButtonDown()
    {
        ButtonStatus = ButtonState.Start;
        //イベントを登録する
    }
    public void OnButtonUp()
    {
        ButtonStatus = ButtonState.Up;
        //この状態を感知したイベント内でイベントを解除する
    }
}