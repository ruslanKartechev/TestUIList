using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveJSON 
{
    public void SaveLists(List<CustomList> lists, string path);
}
