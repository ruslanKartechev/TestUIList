using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputButton : MonoBehaviour, IClickable
{
    public ListOutput outputHandle;
    private ISelectable selectionHandle;
    void Awake()
    {
        if (outputHandle == null) outputHandle = FindObjectOfType<ListOutput>();
        selectionHandle = GetComponent<ISelectable>();
    }

    public void OnClick()
    {
        if(selectionHandle!=null)
        {
            selectionHandle.OnSelect();
        }
        outputHandle.OutputAll();
    }
    public void OnRelease()
    {
        if (selectionHandle != null)
        {
            selectionHandle.OnDeselect();
        }
    }
    
}
