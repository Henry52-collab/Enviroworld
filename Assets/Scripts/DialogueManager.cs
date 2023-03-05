using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    List<Dictionary<string,object>> data;
    int i = 0;

    void Awake() {
        data = CSVReader.Read ("Dialogue - Sheet1"); 
    }

    // Update is called once per frame
    public void GetNextLine()
    {
        print (data[i]["Scene"] + ", " +data[i]["Person"] + ", " +data[i]["Pose"] + ", " +data[i]["Text"]);
        i++;
    }
}
