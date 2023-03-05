using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DIALOGUE
{
    public class ConversationManager : MonoBehaviour
    {
        private Coroutine process = null;
        private TextArchitect architect = null;
        public bool isRunning => process != null;
        private DialogueSystem dialogSystem => DialogueSystem.instance;
        private bool userPrompt = false;
        public ConversationManager(TextArchitect architect)
        {
            this.architect = architect;
            dialogSystem.onUserPrompt_Next += OnUserPrompt_Next;
        }

        private void OnUserPrompt_Next()
        {
            userPrompt = true;
        }
        public void StartConversation(List<string> conversation)
        {
            StopConversation();
            process = dialogSystem.StartCoroutine(RunningConversation(conversation));

        }

        public void StopConversation()
        {
            if (!isRunning) return;
            dialogSystem.StopCoroutine(process);
            process = null;
        }

        IEnumerator RunningConversation(List<string> conversation)
        {
            for (int i = 0; i < conversation.Count; i++)
            {
                if (conversation[i] == string.Empty)
                    continue;
                DIALOGUE_LINE line = DialogueParser.Parse(conversation[i]);
                if (line.dialogue != "")
                    yield return Line_RunDialogue(line);
                if (line.hasCommands)
                    yield return Line_RunDialogue(line);
            }
        }

        IEnumerator Line_RunDialogue(DIALOGUE_LINE line)
        {
            if (line.hasSpeaker)
                dialogSystem.ShowSpeakerName(line.speaker);
            else
                dialogSystem.HideSpeakerName();
            //Build dialog
            yield return BuildDialogue(line.dialogue);

            //Wait for user input
            yield return WaitForUserInput();
        }

        IEnumerator Line_RunCommands(DIALOGUE_LINE line)
        {
            Debug.Log(line.commands);
            yield return null;
        }

        IEnumerator BuildDialogue(string dialogue)
        {
            architect.Build(dialogue);
            while (architect.isBuilding)
            {
                if (userPrompt)
                {
                    if (!architect.hurryUp)
                        architect.hurryUp = true;
                    else
                        architect.ForceComplete();
                    userPrompt=false;
                }
                yield return null;
            }
        }

        IEnumerator WaitForUserInput()
        {
            while (!userPrompt)
                yield return null;
            userPrompt = false;
        }
    }
}

