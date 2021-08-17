using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ListOutput : MonoBehaviour
{
    [SerializeField] private Color headColor;
    [SerializeField] private Color mainColor;
    private const float DISTANCE = 10f;
    private const float Y_OFFSET = 0.2f;
    public const float ColomnWidth = 400f;
    public Canvas outputCanvas;
    public GameObject elementPF;
    public ListManager listManagerHandle;
    private TextMeshProUGUI elementText;
    private List<GameObject> elementsOnScreen = new List<GameObject>();
    private List<Vector2> gridPositions = new List<Vector2>();
    void Awake()
    {
        elementText = elementPF.GetComponentInChildren<TextMeshProUGUI>();
        if(elementText == null) { Debug.LogWarning("TMpro component not found"); }
    }
    public void OutputAll()
    {
        ClearScreen();
        OutputMultiple(listManagerHandle.stringLists);
    }
    public void ClearScreen()
    {

        foreach(GameObject obj in elementsOnScreen)
        {
            Destroy(obj);
        }
        elementsOnScreen.Clear();
    }
    public void OutputSingle(CustomList list)
    {
        Transform headTrans = OutputHead(list.name, list.size);
        OutputList(list, headTrans);
    }
    public void OutputMultiple(List<CustomList> lists)
    {
        List<Vector2> headPositions = FindHeadPositions(lists.Count);
        for (int i = 0; i < lists.Count; i++)
        {
            Transform head = OutputHead(lists[i].name, lists[i].size);
            head.localPosition = headPositions[i];
            OutputList(lists[i], head);
        }
    }
    private Transform OutputHead(string listName, int listSize)
    {
        GameObject temp =  OutputElement(listName +": "+ listSize.ToString());
        IGridOutput handle = temp.GetComponent<IGridOutput>();
        Vector2 dimentions = handle.GetDimentions();
        handle.SetElementColor(headColor);
        temp.transform.localPosition = FindHeadPosition();
        return temp.transform;
    }
    private void OutputList(CustomList list, Transform headTrans)
    {
        Transform prevElement = headTrans;
        for (int i = 0; i < list.size; i++)
        {
            string text = list.list[i];
            GameObject element = OutputElement(text);
            Vector2 dimentions = element.GetComponent<IGridOutput>().GetDimentions();
            Vector2 prevElementDimentions = prevElement.GetComponent<IGridOutput>().GetDimentions();
            element.transform.localPosition = ElementPosition(prevElement.localPosition, prevElementDimentions, dimentions);
            prevElement = element.transform;
        }
        SetGrid();
    }
     GameObject OutputElement(string text)
    {
        GameObject temp = Instantiate(elementPF, Vector2.zero, Quaternion.identity);
        elementsOnScreen.Add(temp);
        IGridOutput handle = temp.GetComponent<IGridOutput>();
        temp.transform.SetParent(outputCanvas.transform);
        handle.SetText(text);
        handle.SetElementColor(mainColor);
        handle.SetGridDistance(DISTANCE);
        handle.SetGridBounds(GridBounds().x, GridBounds().y);
        return temp;
    }
    private Vector2 GridBounds()
    {
        float height = outputCanvas.pixelRect.height / 2;
        Vector2 bounds = new Vector2(height*(1-Y_OFFSET), -1*height * (1 - Y_OFFSET));
        return bounds;
    }
    private Vector2 FindHeadPosition()
    {
        Vector2 position = new Vector2(0f, GridBounds().x);
        return position; 
    }
    public Vector2 ElementPosition(Vector2 prevElement, Vector2 prevElementDimentions, Vector2 elementDimentions) 
    {
        Vector2 position = new Vector2(prevElement.x, prevElement.y - prevElementDimentions.y/2 - elementDimentions.y/2 - DISTANCE  ) ;
        return position;
    }

    private List<Vector2> FindHeadPositions(int num)
    {
        List<Vector2> headPositions = new List<Vector2>();
        float height = outputCanvas.pixelRect.height / 2;
        for (int i=0; i < num; i++)
        {
            if (num % 2 == 0)
            {
                float x = 0f + ColomnWidth / 2 - (num) / 2 * ColomnWidth + i * ColomnWidth;
                float y = height * (1 - Y_OFFSET);
                Vector2 temp = new Vector2(x, y);
                headPositions.Add(temp);
            } else if (num % 2 != 0)
            {
                float x = 0 - (num - 1) / 2 * ColomnWidth + i * ColomnWidth;
                float y = height * (1 - Y_OFFSET);
                Vector2 temp = new Vector2(x, y);
                headPositions.Add(temp);
            }
        }

        return headPositions;
    }
    private void SetGrid()
    {
        foreach(GameObject temp in elementsOnScreen)
        {
            temp.GetComponent<IGridOutput>().SetGridElements(elementsOnScreen);
        }
    }

}
