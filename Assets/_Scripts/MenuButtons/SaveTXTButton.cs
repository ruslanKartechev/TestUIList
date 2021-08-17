using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTXTButton : MonoBehaviour, IClickable
{
    public ListSaver saverHandle;
    private ISelectable selectionHandle;
    private void Awake()
    {
        if (saverHandle == null) saverHandle = FindObjectOfType<ListSaver>();
        selectionHandle = GetComponent<ISelectable>();
    }
    public void OnClick()
    {
        if (selectionHandle != null)
        {
            selectionHandle.OnSelect();
        }
        saverHandle.SaveTXT();
    }

    public void OnRelease()
    {
        if (selectionHandle != null)
        {
            selectionHandle.OnDeselect();
        }
    }

}
