using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] VisualManager visualManager;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] TextMeshProUGUI nameText;
    List<Dictionary<string,object>> data;
    int i = 0;

    void Awake() {
        data = CSVReader.Read ("Dialogue - Sheet1"); 
    }

    // Update is called once per frame
    public void GetNextLine()
    {
        print (data[i]["Scene"] + ", " +data[i]["Person"] + ", " +data[i]["Pose"] + ", " +data[i]["Text"]);
        dialogueText.text = (string)data[i]["Text"];
        nameText.text = (string)data[i]["Person"];
        handleScene((string)data[i]["Scene"]);
        i++;
    }

    private void handleScene(string sceneName){
        if(sceneName.Length > 0 && sceneName[0] == '!'){
            StringBuilder sb = new StringBuilder((string)data[i]["Text"]);
            string temp = (string)data[i]["Text"];
            sb[temp.IndexOf("X")] = '#';
            sb[temp.IndexOf("Y")] = '$';
            sb[temp.IndexOf("Z")] = '%';
            nameText.text = sb.ToString();
        }else{
            visualManager.NextScene();
        }
    }

    private void handlePerson(string personName){
        visualManager.CharacterDeliverLine(personName, "idle");
    }
}
