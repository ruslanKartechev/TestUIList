using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListSaver : MonoBehaviour
{
    private ISaveJSON jsonSaver;
    private ISaveXML xmlSaver;
    private ISaveTXT txtSaver;
    private string TXTpath;
    private string XMLpath;
    private string JSONpath;
    public ListManager listmanagerHandle;
    void Awake()
    {
        jsonSaver = GetComponent<ISaveJSON>();
        xmlSaver = GetComponent<ISaveXML>();
        txtSaver = GetComponent<ISaveTXT>();
        XMLpath = Application.streamingAssetsPath + "/XMLOutput.xml";
        JSONpath = Application.streamingAssetsPath + "/JSONOutput.json";
        TXTpath = Application.streamingAssetsPath + "/TextOutput.txt";
    }

    public void SaveJSON()
    {
        jsonSaver.SaveLists(listmanagerHandle.stringLists, JSONpath);
    }
    public void SaveXML()
    {
        xmlSaver.SaveLists(listmanagerHandle.stringLists, XMLpath);

    }
    public void SaveTXT()
    {
        txtSaver.SaveLists(listmanagerHandle.stringLists, TXTpath);
    }


}
