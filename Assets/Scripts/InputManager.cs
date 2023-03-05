using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private string last = "";
    [SerializeField] private VisualManager visualManager;
    [SerializeField] private DialogueManager dialogueManager;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T)){
            print("T");
            visualManager.NextScene();
        }
        if(Input.GetKeyDown(KeyCode.A)){
            print("A");
            last = "Eric";
            visualManager.AddCharacter(last, "idle", true);
        }
        if(Input.GetKeyDown(KeyCode.S)){
            print("S");
            last = "Tim";
            visualManager.AddCharacter(last, "idle", true);
        }
        if(Input.GetKeyDown(KeyCode.D)){
            print("D");
            last = "Tomio";
            visualManager.AddCharacter(last, "idle", true);
        }
        if(Input.GetKeyDown(KeyCode.F)){
            print("F");
            visualManager.RemoveCharacter(last);
        }
        if(Input.GetKeyDown(KeyCode.G)){
            print("G");
            visualManager.CharacterDeliverLine(last, "confused");
        }
        if(Input.GetKeyDown(KeyCode.H)){
            print("H");
            visualManager.CharacterDeliverLine(last, "angery");
        }
        if(Input.GetKeyDown(KeyCode.J)){
            print("J");
            last = "Marc";
            visualManager.AddCharacter(last, "idle", false);
        }
        if(Input.GetKeyDown(KeyCode.N)){
            //print("N");
            dialogueManager.GetNextLine();
        }
    }
}
