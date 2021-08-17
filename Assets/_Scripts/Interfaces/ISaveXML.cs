using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveXML 
{
    public void SaveLists(List<CustomList> lists, string path);
}
