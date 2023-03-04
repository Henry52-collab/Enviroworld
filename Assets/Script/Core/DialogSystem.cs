using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DialogSystem : MonoBehaviour
{
    [HideInInspector]public bool isWaitingForUserInput = false;
    public static DialogSystem instance;
    public ELEMENTS elements;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    //Say something and show it on the speech box
    public void say(string speech,string speaker)
    {
       stop();
       speaking = StartCoroutine(Speaking(speech,speaker));
    }

    public void stop()
    {
        if (isSpeaking)
        {
            StopCoroutine(speaking);
        }
        speaking = null;
    }
    public bool isSpeaking { get { return speaking != null;} }
    Coroutine speaking = null;
    IEnumerator Speaking(string targetSpeech,string speaker)
    {
        speechPanel.SetActive(true);
        speechText.text = "";
        speakerNameText.text = speaker;
        isWaitingForUserInput = false;
        while(speechText.text != targetSpeech)
        {
            speechText.text += targetSpeech[speechText.text.Length];
            yield return new WaitForEndOfFrame();
        }
        //text finished
        isWaitingForUserInput = true;
        while (isWaitingForUserInput) {yield return new WaitForEndOfFrame();}
        stop();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    //Stores the UI components
    [System.Serializable]
    public class ELEMENTS
    {
        public GameObject speechPanel;
        public TMP_Text speakerNameText;
        public TMP_Text speechText;
    }

    public GameObject speechPanel { get { return elements.speechPanel; } }
    public TMP_Text speakerNameText { get{ return elements.speakerNameText; } }
    public TMP_Text speechText { get{ return elements.speechText; } }
}
