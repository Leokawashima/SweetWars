using UnityEngine;
using UnityEngine.InputSystem;

public class TitleManagerScript : MonoBehaviour
{
    public enum TitleState { Stand_by, Select }
    [Header("タイトル画面ステート")]
    public TitleState State = TitleState.Stand_by;
    [Header("待機状態に戻るまでの時間")]
    [SerializeField] float resetStartTime = 20f;
    [Header("入力時効果音")]
    [SerializeField] AudioSource audio_SE;
    [Header("パーティクル")]
    [SerializeField] ParticleSystem particle;
    [Header("アニメ")]
    [SerializeField] TitleAnimatorScript anim;

    float elapseTime = 0f;

    private void OnEnable()
    {
        var input = new MyInputSystem();

        input.Title.AnyInput.started += OnAnyInput;

        input.Enable();
    }
    private void OnDisable()
    {
        var input = new MyInputSystem();

        input.Title.AnyInput.started -= OnAnyInput;

        input.Enable();
    }

    void OnAnyInput(InputAction.CallbackContext context)
    {
        if(State == TitleState.Stand_by)
            GoSelect();
    }

    void Update()
    {
        elapseTime += Time.deltaTime;

        if(elapseTime > resetStartTime)
        {
            BackSelect();
        }
    }

    void GoSelect()
    {
        State = TitleState.Select;
        elapseTime = 0f;
        audio_SE.Play();
        anim.SelectAnim(true);
        particle.Play();
    }

    void BackSelect()
    {
        State = TitleState.Stand_by;
        anim.SelectAnim(false);
    }
}
