using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IControl 
{
    public void SetPosition(Vector3 position);
    
    public void Click();
    public void Release();
}
