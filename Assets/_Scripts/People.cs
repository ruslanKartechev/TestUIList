using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Person
{
    public string firstName;
    public string lastName;
    public Person()
    {
        firstName = null;
        lastName = null;
    }
    public Person(string att_1, string att_2)
    {
        firstName = att_1;
        lastName = att_2;
    }
}

[System.Serializable]
public class People
{
    public Person[] people;
    public People()
    {
        people = null;
    }
    public People(Person[] peoplearr)
    {
        people = peoplearr;
    }
}
public class CreateListFromPeople: People
{
    public static void CreatePeopleNamesList(Person[] personArr, string attName_1, string attName_2)
    {
        List<string> att_1 = new List<string>();
        List<string> att_2 = new List<string>();
        foreach (Person p in personArr)
        {
            att_1.Add(p.firstName);
            att_2.Add(p.lastName);
        }
        ListManager.CreateList(att_1, attName_1);
        ListManager.CreateList(att_2, attName_2);
    }

}

public class CreatePeopleFromList : People
{
    public static People CreatePeopleObjFromList(List<CustomList> lists)
    {
        if(lists.Count == 0)
        {
            Debug.LogWarning("Passing empty lists");
            return null;
        }
        Person[] personArr = new Person[lists[0].size];
        List<string> att_1 = new List<string>();
        List<string> att_2 = new List<string>();
        foreach(CustomList ls in lists)
        {
            if(ls.name.ToLower().Contains("firstname"))
            {
                att_1 = ls.list;
            } else if(ls.name.ToLower().Contains("lastname"))
            {
                att_2 = ls.list;
            }
        }
        for(int i =0; i<att_1.Count; i++)
        {
            string attribute_1 = att_1[i];
            string attribute_2 = null;
            if (att_2[i] != null)
                 attribute_2 = att_2[i];
            Person p = new Person(attribute_1, attribute_2);
            personArr[i] = p;
            
        }
        People temp = new People(personArr);

        return temp;
    }

}