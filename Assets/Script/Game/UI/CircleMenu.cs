using UnityEngine;

/// <summary>
/// 円形テーブルメニュー
/// </summary>
/// 調べたところ無料のRadicalMenuFramwarkというアセットなどがあったが、
/// 更新が古かったのとInputSystemに対応してるわけではなさそうだったので、
/// 自分で実装を行った。
public class CircleMenu : MonoBehaviour
{
    [SerializeField] RectTransform[] buttons;
    [SerializeField] float dist = 100.0f;
    [SerializeField] float offset = 0;

    const float TwoPi = Mathf.PI * 2;

    void OnValidate()
    {
        if(buttons == null) return;
        var angleOffset = TwoPi / buttons.Length;
        var delay = offset * Mathf.Deg2Rad;
        for (int i = 0; i < buttons.Length; i++)
        {
            float   x = Mathf.Sin(delay + angleOffset * i) * dist,
                    y = Mathf.Cos(delay + angleOffset * i) * dist;
            buttons[i].anchoredPosition = new Vector2(x, y);
        }
    }
}
