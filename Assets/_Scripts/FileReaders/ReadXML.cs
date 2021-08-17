using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
public class ReadXML : MonoBehaviour, IReadXML
{
    private string inpText = null;
    public void ReadFile(string path)
    {
        if (File.Exists(path))
        {

            inpText = File.ReadAllText(path);
            XmlDocument dataXML = new XmlDocument();
            dataXML.LoadXml(inpText);
            XmlNodeList nodes = dataXML.SelectNodes("/People/Person");
            People peopleArr = new People();
            peopleArr.people = new Person[nodes.Count];
            for(int i=0; i<nodes.Count; i++)
            {
                string attribute_1 = nodes[i].SelectSingleNode("firstName").InnerText;
                string attribute_2 = nodes[i].SelectSingleNode("lastName").InnerText;
                Person temp = new Person(attribute_1, attribute_2);
                peopleArr.people[i] = temp;
            }
            CreateListFromPeople.CreatePeopleNamesList(peopleArr.people, "FirstNames", "LastNames");

        } else
        {
            Debug.LogWarning("No file in the given path");
        }
    }

}
