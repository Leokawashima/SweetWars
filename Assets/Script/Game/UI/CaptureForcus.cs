using UnityEngine;

public class CaptureForcus : MonoBehaviour
{
    //DunamicObjectを継承したクラスを持つコンポーネントのみにする(後で)
    [SerializeField] GameObject target;
    [SerializeField] RectTransform rect;
    bool isCapture = false;

    void FixedUpdate()
    {
        Vector2 pos = Camera.main.WorldToViewportPoint(target.transform.position);
        isCapture = pos.x > 0 && pos.x < 1 && pos.y > 0 && pos.y < 1;
        rect.gameObject.SetActive(isCapture);
    }
    void Update()
    {
        if(!isCapture) return;

        Vector2 pos = Camera.main.WorldToScreenPoint(target.transform.position);
        rect.anchoredPosition = pos;
    }
}
