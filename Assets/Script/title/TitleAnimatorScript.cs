using UnityEngine;

public class TitleAnimatorScript : MonoBehaviour
{
    [Header("�Z���N�g���j���[�̃A�j��")]
    [SerializeField] Animator animator;

    public void SelectAnim(bool onoff)
    {
        animator.SetBool("SelectMenu", onoff);
    }
}
