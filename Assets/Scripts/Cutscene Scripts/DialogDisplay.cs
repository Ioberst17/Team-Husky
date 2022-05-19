using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// controller for the conversation
public class DialogDisplay : MonoBehaviour
{
    public Conversation conversation;

    public GameObject speakerLeft;
    public GameObject speakerRight;

    private SpeakerUI speakerUILeft;
    private SpeakerUI speakerUIRight;

    private int activeLineIndex = 0;

    void Start()
    {
        // assign speakers
        speakerUILeft = speakerLeft.GetComponent<SpeakerUI>();
        speakerUIRight = speakerRight.GetComponent<SpeakerUI>();

        // assign speaker conversations
        speakerUILeft.Speaker = conversation.speakerLeft;
        speakerUIRight.Speaker = conversation.speakerRight;
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            AdvanceConversation();
        }
    }

    void AdvanceConversation()
    {
        if (activeLineIndex < conversation.lines.Length)
        {
            DisplayLine();
            activeLineIndex += 1;
        }
        else
        {
            speakerUILeft.Hide();
            speakerUIRight.Hide();
            activeLineIndex = 0; // resets line to first line of the conversation
        }
    }

    // Assumes only two characters
    void DisplayLine()
    {
        Line line = conversation.lines[activeLineIndex];
        Character character = line.character;

        if (speakerUILeft.SpeakerIs(character))
        {
            SetDialog(speakerUILeft, speakerUIRight, line.text);
        }
        else
        {
            SetDialog(speakerUIRight, speakerUILeft, line.text);
        }

        void SetDialog(
            SpeakerUI activeSpeakerUI,
            SpeakerUI inactiveSpeakerUI,
            string text)
        {
            activeSpeakerUI.Dialog = text;
            activeSpeakerUI.Show();
        }
    }
}
