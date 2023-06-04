using UnityEngine;

/// <summary>
/// アニメーション遷移配列定義SO
/// </summary>
[CreateAssetMenu(menuName = "AnimState_SO")]
public class AnimState_SO : ScriptableObject
{
    public AnimState[] Anims;
}

/// <summary>
/// アニメーション遷移基底クラス
/// </summary>
[System.Serializable]
public class AnimState
{
    public string StateName;
    public float Transition_Duration;
    [Range(0, 1.0f)] public float Transition_Offset;
    public int layerNumber = 0;
}

/// <summary>
/// アニメーション遷移効率化クラス
/// </summary>
public static class AnimatorExtender
{
    public static void CrossFade(this Animator animator, AnimState state)
    {
        animator.CrossFade(state.StateName, state.Transition_Duration, state.layerNumber, state.Transition_Offset); ;
    }
}