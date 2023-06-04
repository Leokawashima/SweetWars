using System.Collections.Generic;
using UnityEngine;

public class GoodOffsetScript : MonoBehaviour
{
    [SerializeField] SizeChangeScript[] menu;

    List<RectTransform> rect = new List<RectTransform>();
    List<float> posY = new List<float>();

    float sizeDiffrence = 0f;

    void Start()
    {
        for(int i = 0; i < menu.Length; ++i)
        {
            rect.Add(menu[i].gameObject.GetComponent<RectTransform>());
            posY.Add(rect[i].anchoredPosition.y);
        }

        sizeDiffrence = menu[0].Scale.y - menu[0].Scale.x;
    }

    public void ChangeOffset()
    {
        for (int i = 0; i < menu.Length; i++)
        {
            if(menu[i].isSelect)
            {
                if (i == 0)
                {
                    i++;
                    for (; i < menu.Length; i++)
                    {
                        rect[i].anchoredPosition = new Vector2(rect[i].anchoredPosition.x, rect[i].anchoredPosition.y - rect[i].rect.height / 2f * sizeDiffrence);
                    }
                }
                else
                {
                    rect[i].anchoredPosition = new Vector2(rect[i].anchoredPosition.x, rect[i].anchoredPosition.y - rect[i].rect.height / 2f * sizeDiffrence);
                    i++;
                    for (; i < menu.Length; i++)
                    {
                        rect[i].anchoredPosition = new Vector2(rect[i].anchoredPosition.x, rect[i].anchoredPosition.y - rect[i].rect.height * sizeDiffrence);
                    }
                }
            }
        }
    }

    public void ResetOffset()
    {
        for(int i = 0; i < menu.Length; i++)
        {
            rect[i].anchoredPosition = new Vector2(rect[i].anchoredPosition.x, posY[i]);
        }
    }
}
