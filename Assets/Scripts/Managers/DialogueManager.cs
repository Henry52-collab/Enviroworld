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
        data = CSVReader.Read ("DialogueSheet"); 
    }

    // Update is called once per frame
    public void GetNextLine()
    {
        print (data[i]["Scene"] + ", " +data[i]["Person"] + ", " +data[i]["Pose"] + ", " +data[i]["Text"]);
        dialogueText.text = (string)data[i]["Text"];
        nameText.text = (string)data[i]["Person"];
        handleChar();
        
        i++;
    }

    public void handleChar(){
        print((string)data[i]["Scene"]);
        foreach(char c in (string)data[i]["Scene"])
        {
            print(c);
            switch (c)
            {
                case '#':
                    print(visualManager.CharacterInScene("YOU"));
                    if(visualManager.CharacterInScene("YOU"))
                    {
                        visualManager.RemoveCharacter("YOU");
                    }
                    else
                    {
                        visualManager.AddCharacter("YOU","idle",true);
                    }
                    break;
                case '$':
                    if(visualManager.CharacterInScene("BARRY")){
                        visualManager.RemoveCharacter("BARRY");
                    }
                    else{
                        visualManager.AddCharacter("BARRY","idle",false);
                    }
                    break;
                case '%':
                    if(visualManager.CharacterInScene("SIENNA")){
                        visualManager.RemoveCharacter("SIENNA");
                    }
                    else{
                        visualManager.AddCharacter("SIENNA","idle",false);
                    }
                    break;
                case '^':
                    if(visualManager.CharacterInScene("CYRUS")){
                        visualManager.RemoveCharacter("CYRUS");
                    }
                    else{
                        visualManager.AddCharacter("CYRUS","idle",false);
                    }
                    break;                
                case '&':
                    if(visualManager.CharacterInScene("CASSANDRA")){
                        visualManager.RemoveCharacter("CASSANDRA");
                    }
                    else{
                        visualManager.AddCharacter("CASSANDRA","idle",false);
                    }
                    break;
                case '*':
                    if(visualManager.CharacterInScene("HONEY")){
                        visualManager.RemoveCharacter("HONEY");
                    }
                    else{
                        visualManager.AddCharacter("HONEY","idle",false);
                    }
                    break;
                case '<':
                    if(visualManager.CharacterInScene("HONEY_BIG_OIL")){
                        visualManager.RemoveCharacter("HONEY_BIG_OIL");
                    }
                    else{
                        visualManager.AddCharacter("HONEY_BIG_OIL","idle",false);
                    }
                    break; 
                case '>':
                    if(visualManager.CharacterInScene("BIG_OIL")){
                        visualManager.RemoveCharacter("BIG_OIL");
                    }
                    else{
                        visualManager.AddCharacter("BIG_OIL","idle",false);
                    }
                    break; 
                case '~':
                    visualManager.NextScene();
                    break;
                case '!':
                    StringBuilder sb = new StringBuilder((string)data[i]["Text"]);
                    string temp = (string)data[i]["Text"];
                    sb[temp.IndexOf("X")] = '#';
                    sb[temp.IndexOf("Y")] = '$';
                    sb[temp.IndexOf("Z")] = '%';
                    nameText.text = sb.ToString();
                    break;                                                          
                default:
                    break;
            }
        }

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
