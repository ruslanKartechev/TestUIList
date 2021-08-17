using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;
public class SaveXML : MonoBehaviour, ISaveXML
{
    public void SaveLists(List<CustomList> lists, string path)
    {
        XmlDocument output = new XmlDocument();
        XmlElement root = output.CreateElement("People");
        People people = CreatePeopleFromList.CreatePeopleObjFromList(lists);
        foreach (Person p in people.people)
        {
            XmlElement person = output.CreateElement("Person");
            XmlElement firstName = output.CreateElement("firstName");
            firstName.InnerText = p.firstName;
            XmlElement lastName = output.CreateElement("lastName");
            lastName.InnerText = p.lastName;
            person.AppendChild(firstName);
            person.AppendChild(lastName);
            root.AppendChild(person);
        }


        output.AppendChild(root);
        output.Save(path);
    }
    
}
