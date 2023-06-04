using UnityEngine;

public class SizeChangeScript : MonoBehaviour
{
    float scaleDef = 1f;
    float scaleBig = 1.5f;
    public  Vector2 Scale => new Vector2 (scaleDef, scaleBig);

    RectTransform rt;
    BoxCollider2D col2D;

    Vector2 defPos = Vector2.zero;
    Vector2 bigPos = Vector2.zero;

    public bool isSelect { get; private set; }

    [SerializeField] GoodOffsetScript offset;

    void Start()
    {
        rt = GetComponent<RectTransform>();
        col2D = GetComponent<BoxCollider2D>();
        col2D.size = rt.rect.size;

        defPos = rt.anchoredPosition;
        bigPos = new Vector2(defPos.x - rt.rect.x * (scaleBig - scaleDef), defPos.y);
    }

    public void Forcus()
    {
        rt.anchoredPosition = bigPos;
        rt.localScale *= scaleBig;
        isSelect = true;
        offset.ChangeOffset();
        Debug.Log("カーソルが重なっている：" + gameObject.name);
    }

    public void UnForcus()
    {
        rt.anchoredPosition = defPos;
        rt.localScale = Vector3.one * scaleDef;
        isSelect = false;
        offset.ResetOffset();
        Debug.Log("カーソルが重なっていない：" + gameObject.name);
    }

    private void OnMouseEnter()
    {
        Forcus();
    }

    private void OnMouseExit()
    {
        UnForcus();
    }
}
