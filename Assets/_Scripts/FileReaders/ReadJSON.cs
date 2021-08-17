using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ReadJSON : MonoBehaviour, IReadJSON
{

    private string JsonText = null;

    public void ReadFile(string path)
    {
        if (File.Exists(path))
        {
            JsonText = File.ReadAllText(path);
            People people = JsonUtility.FromJson<People>(JsonText);
            CreateListFromPeople.CreatePeopleNamesList(people.people, "FirstNames", "LastNames");
        }
        else
        {
            Debug.LogWarning("No file in the given path");
        }

    }

    
}
