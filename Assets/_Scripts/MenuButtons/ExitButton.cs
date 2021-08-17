using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour, IClickable
{
    private ISelectable selectionHandle;
    private void Awake()
    {
        selectionHandle = GetComponent<ISelectable>();
    }
    public void OnClick()
    {
        if (selectionHandle != null)
        {
            selectionHandle.OnSelect();
        }
        Application.Quit();
    }
    public void OnRelease()
    {
        if (selectionHandle != null)
        {
            selectionHandle.OnDeselect();
        }
    }
}
