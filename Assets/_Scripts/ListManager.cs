using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CustomList
{
    public string name;
    public List<string> list;
    public int size;
    public CustomList()
    {
        name = null;
        list = new List<string>();
        size = 0;
    }
    public CustomList(string nam)
    {
        name = nam;
        list = new List<string>();
        size = 0;
    }
}


public class ListManager : MonoBehaviour
{
    //[SerializeField] private List<CustomList> stringLists = new List<CustomList>();
    public List<CustomList> stringLists { get; private set; }
    private static ListManager S;
    public void Awake()
    {
        if (S == null) S = this;
        S.stringLists = new List<CustomList>();
        SetSizes();
    }

    private void SetSizes()
    {
        foreach(CustomList ls in stringLists)
        {
            ls.size = ls.list.Count;
        }
    }
    public static void CreateList(List<string> inp, string listName)
    {
        CustomList newList = new CustomList(listName);
        S.stringLists.Add(newList);
        foreach (string s in inp)
        {
            S.AddElement<string>(listName, s);
        }
    }

    public static void DeleteList<T>(string listName)
    {
        CustomList target = S.ReturnList<T>(listName);
        S.stringLists.Remove(target);
    }
    public static void ClearAll()
    {
        S.stringLists.Clear();
    }
    private void AddElement<T>(string listName, string targetElement)
    {
        CustomList target = ReturnList<T>(listName);
        if (target == null)
        {
            Debug.LogWarning("the list Not found");
        }
        else
        {
            target.list.Add(targetElement);
            target.size += 1;
        }
    }

    public CustomList ReturnList<T>(string name)
    {
        CustomList foundList = null;
        foreach(CustomList sl  in stringLists)
        {
            if(sl.name == name)
            {
                foundList = sl;
            }
        }
        return foundList;
    }


}
