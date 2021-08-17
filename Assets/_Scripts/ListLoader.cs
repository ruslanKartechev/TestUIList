using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListLoader : MonoBehaviour
{
    private string Jsonpath;
    private string XMLpath;
    private string TXTpath;
    private IReadJSON jsonReader;
    private IReadXML xmlReader;
    private IReadTXT txtReader;
    void Awake()
    {
        jsonReader = GetComponent<IReadJSON>();
        xmlReader = GetComponent<IReadXML>();
        txtReader = GetComponent<IReadTXT>();
        Jsonpath = Application.streamingAssetsPath + "/JsonInput.json";
        XMLpath = Application.streamingAssetsPath + "/XMLInput.xml";
        TXTpath = Application.streamingAssetsPath + "/TXTInput.txt";
    }
    public void ReadJSONfile()
    {
        ListManager.ClearAll();
        jsonReader.ReadFile(Jsonpath);
    }
    public void ReadXMLfile()
    {
        ListManager.ClearAll();
        xmlReader.ReadFile(XMLpath);
    }
    public void ReadTXTfile()
    {
        ListManager.ClearAll();
        txtReader.ReadFile(TXTpath);
    }


}
