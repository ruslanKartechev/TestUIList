using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadXMLButton : MonoBehaviour, IClickable
{
    public ListLoader loaderhandle;
    private ISelectable selectionHandle;
    void Awake()
    {
        if (loaderhandle == null) loaderhandle = FindObjectOfType<ListLoader>();
        selectionHandle = GetComponent<ISelectable>();
    }
    public void OnClick()
    {
        if (selectionHandle != null)
        {
            selectionHandle.OnSelect();
        }
        loaderhandle.ReadXMLfile();
    }
    public void OnRelease()
    {
        if (selectionHandle != null)
        {
            selectionHandle.OnDeselect();
        }
    }
}
