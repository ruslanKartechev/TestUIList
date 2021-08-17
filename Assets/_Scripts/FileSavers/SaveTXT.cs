using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class SaveTXT : MonoBehaviour, ISaveTXT
{
    public void SaveLists(List<CustomList> lists, string path)
    {
        if (File.Exists(path))
        {
            string textOutPut = "";
            foreach (CustomList ls in lists)
            {
                textOutPut += ls.name + ": ";
                foreach (string s in ls.list)
                {
                    textOutPut += s + ", ";
                }
                textOutPut += "\n";
            }
            StreamWriter writer = new StreamWriter(path, false);
            writer.Write(textOutPut);
            writer.Close();
        } else
        {
            Debug.LogWarning("No file in the given path");
        }


    }


}
