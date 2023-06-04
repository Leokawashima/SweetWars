using UnityEngine;

public class TitleAnimatorScript : MonoBehaviour
{
    [Header("セレクトメニューのアニメ")]
    [SerializeField] Animator animator;

    public void SelectAnim(bool onoff)
    {
        animator.SetBool("SelectMenu", onoff);
    }
}
