using UnityEngine;
using TMPro;

public class IntervalFadeText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI fadeText;
    [SerializeField] float fadeStartTime = 2f;
    [SerializeField] float faderequireTime = 2f;
    [SerializeField] float fadeSceeChangeTime = 2f;
    float elapseTime = 0f;
    public bool IsInvisible = false;

    void Update()
    {
        var _delta = Time.deltaTime;
        elapseTime += _delta;
        
        if(!IsInvisible)
        {
            if(elapseTime > fadeStartTime)
            {
                if(elapseTime < fadeStartTime + faderequireTime / 2f)
                    fadeText.color = fadeText.color - new Color(0, 0, 0, _delta);
                else
                    fadeText.color = fadeText.color + new Color(0, 0, 0, _delta);

                if(elapseTime > fadeStartTime + faderequireTime)
                    elapseTime = 0f;
            }
        }
        else
        {
            fadeText.color = fadeText.color - new Color(0, 0, 0, _delta * fadeSceeChangeTime);
        }
    }
}
