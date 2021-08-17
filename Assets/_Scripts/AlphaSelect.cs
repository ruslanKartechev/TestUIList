using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasRenderer))]
public class AlphaSelect : MonoBehaviour, ISelectable
{
    public float selectAlpha = 0.7f;
    private float defaultAlpha;
    private CanvasRenderer rend;
    void Awake()
    {
        rend = GetComponent<CanvasRenderer>();
        defaultAlpha = rend.GetColor().a;
    }
    public void OnSelect()
    {
        Color current = rend.GetColor();
        current.a = selectAlpha;
        rend.SetColor(current);
    }
    public void OnDeselect()
    {
        Color current = rend.GetColor();
        current.a = defaultAlpha;
        rend.SetColor(current);
    }
}
