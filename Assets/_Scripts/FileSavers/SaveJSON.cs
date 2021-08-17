using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class SaveJSON : MonoBehaviour, ISaveJSON
{
    public void SaveLists(List<CustomList> lists, string path)
    {
        if (File.Exists(path))
        {
            People people = CreatePeopleFromList.CreatePeopleObjFromList(lists);
            string output = JsonUtility.ToJson(people);
            StreamWriter writer = new StreamWriter(path, false);
            writer.Write(output);
            writer.Close();

        } else
        {
            Debug.LogWarning("No file in the given path");
        }

    }


}
