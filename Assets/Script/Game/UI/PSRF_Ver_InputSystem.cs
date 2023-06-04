using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// SpriteのPhysicsShapeの内部のみ当たり判定を有効にするRaycastFilter
/// </summary>
/// ほぼまんま流用させていただいた
/// https://qiita.com/sune2/items/cf9ef9d197b47b2d7a10
/// https://qiita.com/MATU0055/items/2e1a306496d76bf8bfbe
/// 一つ目の方のリンクをそのまま使用したら実行はできるが必ずエラーが出てしまうので、
/// 改良しやすくするためにとりあえず別名でスクリプトを作ってコピペしてからすこしいじっている
[RequireComponent(typeof(Image))]
public class PSRF_Ver_InputSystem : MonoBehaviour, ICanvasRaycastFilter
{
    private readonly List<Vector2> _verts = new List<Vector2>();
    private Image _image;

    void Awake()
    {
        _image = GetComponent<Image>();
    }

    public bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
    {
        var rectTransform = transform as RectTransform;
        Vector2 local;
        //中でデバッグログを呼んでも帰ってこないのに画面座標を得るためにこれがないと上手く作用しない
        //関数の中を見る感じ画面座標にレイを飛ばしてなにも当たらなかったらfalseが帰ってくるっぽいので
        //正直もっとInputSystemに対応した軽量で綺麗な書き方ができそう
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, screenPoint, eventCamera, out local))
        {
            //ExDebug.Log("IsRaycast:false", Color.red);
            return false;
        }

        var rect = rectTransform.rect;
        var pivot = rectTransform.pivot;
        var sprite = _image.sprite;
        var x = (local.x / rect.width + pivot.x - 0.5f) * sprite.rect.width / sprite.pixelsPerUnit;
        var y = (local.y / rect.height + pivot.y - 0.5f) * sprite.rect.height / sprite.pixelsPerUnit;
        var p = new Vector2(x, y);

        var physicsShapeCount = sprite.GetPhysicsShapeCount();
        for(var i = 0; i < physicsShapeCount; i++)
        {
            sprite.GetPhysicsShape(i, _verts);
            if(IsInPolygon(_verts, p))
            {
                // どれかの多角形の内部にあればtrueを返す
                //ExDebug.Log("IsRaycast:true", Color.green);
                return true;
            }
        }

        //ExDebug.Log("IsRaycast:false", Color.red);
        return false;
    }

    /// <summary>
    /// 非凸多角形の内部に点が存在するかどうか
    /// </summary>
    private static bool IsInPolygon(List<Vector2> polygon, Vector2 p)
    {
        // pからx軸の正方向への無限な半直線を考えて、多角形との交差回数によって判定する
        var n = polygon.Count;
        var isIn = false;
        for(var i = 0; i < n; i++)
        {
            var nxt = (i + 1);
            if(nxt >= n) nxt = 0;
            var a = polygon[i] - p;
            var b = polygon[nxt] - p;
            if(a.y > b.y)
            {
                // swap
                var t = a;
                a = b;
                b = t;
            }

            if(a.y <= 0 && 0 < b.y && CrossProduct(a, b) > 0)
            {
                isIn = !isIn;
            }
        }

        return isIn;
    }

    /// <summary>
    /// 外積
    /// </summary>
    private static float CrossProduct(Vector2 u, Vector2 v)
    {
        return u.x * v.y - u.y * v.x;
    }
}
