using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public DialogSystem dialogue;
    // Start is called before the first frame update
    void Start()
    {
        dialogue = DialogSystem.instance;
    }

    public string[] s = new string[]
    {
        "Hi, how are you?:Avira",
        "Its' lovely weather today.",
        "To be honest, I'm glad its not snowing anymore"
    };

    int index = 0;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(!dialogue.isSpeaking || dialogue.isWaitingForUserInput)
            {
                if(index >= s.Length)
                {
                    return;
                }

                Say(s[index]);
                index++;
            }
        }
    }
    void Say(string s)
    {
        string[] parts = s.Split(";");
        string speech = parts[0];
        string speaker = (parts.Length >= 2) ? parts[1] : "";
        dialogue.say(speech, speaker);
    }
}
