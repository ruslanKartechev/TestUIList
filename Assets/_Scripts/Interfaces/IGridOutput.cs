using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGridOutput
{
    public void SetGridElements(List<GameObject> gridObjects);
    public void SetGridDistance(float distance);
    public void SetText(string text);
    public void SetGridBounds(float upperBound, float lowerBounds);

    public Vector2 GetDimentions();
    public void SetTextColor(Color color);
    public void SetFontSize(int size);
    public void SetElementColor(Color color);
    public void SetSprite(Sprite sprite);

}
