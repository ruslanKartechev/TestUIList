using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ListElement : MonoBehaviour, IClickable, IMoveableGrid, IGridOutput
{
    private Canvas myCanvas;
    private TextMeshProUGUI myText;
    private RectTransform myRect;
    private float defaultHeight;
    private float defaultWidth;
    private float distanceGrid = 10;
    private float upperLimit;
    private float lowerLimit;
    private ISelectable selectableHandle;
    private List<GameObject> gridObjs = new List<GameObject>();
    private List<Vector2> gridPositions = new List<Vector2>();
    private bool beingDragged = false;

    private List<GraphicRaycaster> m_Raycasters = new List<GraphicRaycaster>(); // from main canvas
    private EventSystem m_EventSystem; // form event system
    private PointerEventData m_PointerEventData;
    private void Awake()
    {
        selectableHandle = GetComponent<ISelectable>();
        myCanvas = GetComponentInParent<Canvas>();
        myText = GetComponentInChildren<TextMeshProUGUI>();
        myRect = GetComponent<RectTransform>();
        defaultHeight = myRect.rect.height;
        defaultWidth = myRect.rect.width;
        if (m_EventSystem == null) m_EventSystem = FindObjectOfType<EventSystem>();
    }
    private void Start()
    {
        SetLocalScale(Vector3.one);
        if (myCanvas == null)
            myCanvas = GetComponentInParent<Canvas>();
        m_Raycasters.Add(myCanvas.GetComponent<GraphicRaycaster>());
    }
    public void OnClick()
    {
        if (selectableHandle != null)
        {
            selectableHandle.OnSelect();
        }
    }
    public void OnRelease()
    {
        if (selectableHandle != null)
            selectableHandle.OnDeselect();
        beingDragged = false;
        SnapToPosition(FindClosedAvailablePosition());
       
    }


    // IGridOutput
    public void SetText(string text)
    {
        myText.text = text;
        CheckOverflow(myText);
    }

    public Vector2 GetDimentions()
    {
        RectTransform rect = GetComponent<RectTransform>();
        Vector2 dimentions = new Vector2(rect.rect.width, rect.rect.height);
        return dimentions;
    }
    public void SetTextColor(Color color)
    {
        myText.color = color;
    }

    public void SetFontSize(int size)
    {
        myText.fontSize = size;
    }
    public void SetElementColor(Color color)
    {
        gameObject.GetComponent<CanvasRenderer>().SetColor(color);
    }

    public void SetSprite(Sprite sprite)
    {
    }
    public void SetDragPosition(Vector2 position)
    {
        beingDragged = true;
        Vector2 targetPos = new Vector2();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, position, myCanvas.worldCamera, out targetPos);
        myRect.anchoredPosition = targetPos;
    }
    public void SetGridElements(List<GameObject> gridObjects)
    {
        gridObjs = gridObjects;
        foreach (GameObject temp in gridObjs)
        {
            Vector2 localPos = temp.transform.localPosition;
            gridPositions.Add(localPos);
        }
    }
    public void SetGridDistance(float distance)
    {
        distanceGrid = distance;
    }
    public void SetGridBounds(float upperBound, float lowerBounds)
    {
        upperLimit = upperBound;
        lowerLimit = lowerBounds;
    }

    // IMoveableGrid
    public bool IsBeingDragged()
    {
        return beingDragged;
    }
    public void SnapToPosition(Vector2 targetPosition)
    {
        transform.localPosition = targetPosition;
        CheckOverlap();
    }
    public void ReshafleElement()
    {
        Vector2 mypos = new Vector2(transform.localPosition.x, transform.localPosition.y);
        Vector2 pos = FindReshufflePosition(mypos);
        SnapToPosition(pos);
    }

    // private methods
    private Vector2 FindReshufflePosition(Vector2 mypos)
    {
        Vector2 targetPos = new Vector2();
        targetPos = EmptyPositionAbove(mypos);
        if(targetPos == mypos)
        {
            targetPos = PositionBelow(mypos);
        }
        return targetPos;
    }
    private Vector2 EmptyPositionAbove(Vector2 mypos)
    {
        Vector2 targetPos = mypos;
        Vector2 abovePosition = new Vector2(mypos.x, mypos.y + GetDimentions().y + distanceGrid);
        List<Vector2> abovePositions = new List<Vector2>();
        Vector2 temp = mypos;
        while (temp.y < upperLimit)
        {
            temp.y += GetDimentions().y + distanceGrid;
            abovePositions.Add(temp);
        }
        foreach(Vector2 v in abovePositions)
        {
            List<RaycastResult> res = DoRaycast(v);
            if(res.Count == 0)
            {
                if (gridPositions.Contains(abovePosition) == false)
                    gridPositions.Add(abovePosition);
                targetPos = abovePosition;
            }
        }
        return targetPos;
    }
    private Vector2 PositionBelow(Vector2 mypos)
    {
        Vector2 belowPosition = new Vector2(mypos.x, mypos.y - GetDimentions().y - distanceGrid);
        if(gridPositions.Contains(belowPosition) == false)
            gridPositions.Add(belowPosition);
        return belowPosition;
    }
    private List<RaycastResult> DoRaycast(Vector2 position)
    {
        m_PointerEventData = new PointerEventData(m_EventSystem);
        m_PointerEventData.position = CanvasPositionToScreenPosition(position);
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        foreach (GraphicRaycaster rayc in m_Raycasters)
        {
             rayc.Raycast(m_PointerEventData, raycastResults);
        }
        return raycastResults;
    }
    private Vector2 CanvasPositionToScreenPosition(Vector2 inp)
    {
        Vector2 res = new Vector2(inp.x * myCanvas.scaleFactor + myCanvas.pixelRect.width/2, 
                                inp.y * myCanvas.scaleFactor + myCanvas.pixelRect.height / 2);
        return res;
    }
    private Vector2 FindClosedAvailablePosition()
    {
        Vector2 closest = new Vector2(Mathf.Infinity, Mathf.Infinity);
        Vector2 mypos = new Vector2(transform.localPosition.x, transform.localPosition.y);
        foreach (Vector2 temp in gridPositions)
        {
            Vector2 distance = mypos - temp;
            if (distance.sqrMagnitude < (mypos - closest).sqrMagnitude)
            {
                closest = temp;
            }
        }
        //  if(closest.y <= lowerLimit)
        return closest;
    }
    private bool IsOverlappint(RectTransform rect1, RectTransform rect2)
    {
        Rect r1 = new Rect(rect1.localPosition.x, rect1.localPosition.y, rect1.rect.width, rect1.rect.height);
        Rect r2 = new Rect(rect2.localPosition.x, rect2.localPosition.y, rect2.rect.width, rect2.rect.height);

        return r1.Overlaps(r2);
    }
    private void CheckOverlap()
    {
        RectTransform rect1 = GetComponent<RectTransform>();
        foreach (GameObject temp in gridObjs)
        {
            if (temp != this.gameObject)
            {
                RectTransform rect2 = temp.GetComponent<RectTransform>();
                if (IsOverlappint(rect1, rect2))
                {
                    temp.GetComponent<IMoveableGrid>().ReshafleElement();
                }
            }
        }
    }
    private void SetLocalScale(Vector3 scale)
    {
        transform.localScale = scale;
    }

    private void Resize()
    {
        float height = myRect.sizeDelta.y + defaultHeight;
        myRect.sizeDelta = new Vector2(myRect.sizeDelta.x, height);
    }
    private void CheckOverflow(TextMeshProUGUI text)
    {
        Canvas.ForceUpdateCanvases();
        while (text.firstOverflowCharacterIndex > 0)
        {
            Resize();
            Canvas.ForceUpdateCanvases();
        }
    }

}
