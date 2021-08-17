
using UnityEngine;
public interface IMoveableGrid 
{
    public void SetDragPosition(Vector2 position);
    public bool IsBeingDragged();
    public void ReshafleElement();
    public void SnapToPosition(Vector2 targetPosition);

}
