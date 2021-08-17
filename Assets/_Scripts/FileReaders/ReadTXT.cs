using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class ReadTXT : MonoBehaviour, IReadTXT
{
    private string inpText = null;

    public void ReadFile(string path)
    {
        if (File.Exists(path))
        {
            inpText = File.ReadAllText(path);
            string[] lines = ParseLines(inpText);
            List<string> attributes_1 = ReadItems(lines[0], "firstNames");
            List<string> attributes_2 = ReadItems(lines[1], "lastNames");
            Person[] personArr = new Person[attributes_1.Count];
            for (int i=0; i<attributes_1.Count; i++)
            {
                Person temp = new Person(attributes_1[i], attributes_2[i]);
                personArr[i] = temp;
            }
            People peopleObj = new People(personArr);
            CreateListFromPeople.CreatePeopleNamesList(peopleObj.people, "FirstNames", "LastNames");
            
        }
        else
        {
            Debug.Log("No file in the given path");
        }

    }
    private List<string> ReadItems(string line,string attributeName)
    {
        List<string> items = new List<string>();
        string[] words = line.Split(' ');
        foreach(string s in words)
        {
            if(s.Contains(attributeName) == false)
            {
                if (s.Contains(","))
                {
                    string temp = s.Remove(s.IndexOf(','),1);
                    items.Add(temp);
                }
                else
                {
                    items.Add(s);
                }
               
            }
        }


    return items;            
    }
    private string[] ParseLines(string inp)
    {
        string[] lines = inp.Split('\n');
        return lines;
    }
}
