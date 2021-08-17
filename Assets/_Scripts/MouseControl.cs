using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class MouseControl : MonoBehaviour, IControl
{

    private Vector2 mousePostionRaw;
    public List<GraphicRaycaster> m_Raycasters = new List<GraphicRaycaster>(); // from main canvas
    public EventSystem m_EventSystem; // form event system
    private PointerEventData m_PointerEventData;
    private IClickable target = null;
    private IMoveableGrid targetMove = null;

    void Awake()
    {
        if (m_EventSystem == null) m_EventSystem = FindObjectOfType<EventSystem>();
        if (m_Raycasters.Count == 0) Debug.LogWarning("Graphical Raycaster not assigned");
    }
    public void SetPosition(Vector3 position)
    {
        mousePostionRaw = position;
    }

    public void Click()
    {
    //    Debug.Log( "Mouse click position: "+ mousePostionRaw);
        List<RaycastResult> raycastResults = DoRaycast(mousePostionRaw);
        foreach (RaycastResult hit in raycastResults)
        {
            IClickable targetClick = hit.gameObject.GetComponent<IClickable>();
            IMoveableGrid targetM = hit.gameObject.GetComponent<IMoveableGrid>();
            if (targetClick != null)
            {
               
                target = targetClick;
                target.OnClick();
                if (targetM == null) return;
            }
            if(targetM != null)
            {
                hit.gameObject.transform.SetAsLastSibling();
                targetMove = targetM;
                StartCoroutine("Drag");
                return;
            }
        }
    }
    IEnumerator Drag()
    {
        while (true)
        {
            targetMove.SetDragPosition(mousePostionRaw);
            yield return null;
        }
    }
    public void Release()
    {
        if (target != null)
        {
            target.OnRelease();
            target = null;
        }
        if(targetMove != null)
        {
            StopCoroutine("Drag");
            targetMove = null;
        }
    }

    private List<RaycastResult> DoRaycast(Vector2 position)
    {
        m_PointerEventData = new PointerEventData(m_EventSystem);
        m_PointerEventData.position = position;
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        foreach(GraphicRaycaster rayc in m_Raycasters)
        {
            rayc.Raycast(m_PointerEventData, raycastResults);
        }
        return raycastResults;
    }

}
