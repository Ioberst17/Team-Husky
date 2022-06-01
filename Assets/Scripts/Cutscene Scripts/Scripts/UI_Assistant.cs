
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CutScene.Utils;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System;

public class UI_Assistant : MonoBehaviour {

    private Text dialogueText;
    private TextWriter.TextWriterSingle textWriterSingle;
    private AudioSource dialogueAudioSource;
    public string[] dialogueArray;
    private int arrayTracker = 0;
    public Text continueText;

    private void Awake() {
        dialogueText = transform.Find("Dialogue").Find("DialogueText").GetComponent<Text>();
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 1:
                dialogueArray = new string[]
            {
                "Grandma was telling us all about the dog sled run across the vast ice of Alaska…1",
                "“And then,” Grandma said, “he turned himself into a pickle.” She paused to laugh. “It was the funniest shit I’d ever seen.”",
            };
                break;
            case 3:
                dialogueArray = new string[]
            {
                "Grandma was telling us all about the dog sled run across the vast ice of Alaska…3",
                "“And then,” Grandma said, “he turned himself into a pickle.” She paused to laugh. “It was the funniest shit I’d ever seen.”",
            };
                break;
            case 5:
                dialogueArray = new string[]
            {
                "Grandma was telling us all about the dog sled run across the vast ice of Alaska…5",
                "“And then,” Grandma said, “he turned himself into a pickle.” She paused to laugh. “It was the funniest shit I’d ever seen.”",
            };
                break;
            case 7:
                dialogueArray = new string[]
            {
                "Grandma was telling us all about the dog sled run across the vast ice of Alaska…7",
                "“And then,” Grandma said, “he turned himself into a pickle.” She paused to laugh. “It was the funniest shit I’d ever seen.”",
            };
                break;
        }
        //dialogueAudioSource = transform.Find("DialogueAudio").GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (textWriterSingle != null && textWriterSingle.IsActive()) //make sure there aren't multiple dialogue text writers running simultaneously
            {
                // Currently active TextWriter
                textWriterSingle.WriteAllAndDestroy();
            }

            if (arrayTracker < dialogueArray.Length) // iterate through the cutscenes dialogue
            {
                string message = dialogueArray[arrayTracker];
                arrayTracker++;
                //StartTalkingSound();
                textWriterSingle = TextWriter.AddWriter_Static(dialogueText, message, .05f, true, true, StopTalkingSound);
                if (textWriterSingle.IsActive())
                {
                    continueText.gameObject.SetActive(false);
                }
            }
            else // once the array is finished, load the next scene
            {
                if (SceneManager.GetActiveScene().buildIndex + 1 == 8)
                {
                    SceneManager.LoadScene(0);
                }
                else
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                
            }
        }
        if (textWriterSingle != null && !textWriterSingle.IsActive()) //if textwriter isn't typing
        {
            if (!textWriterSingle.IsActive())
            {
                Invoke("turnContinueTextOn", 0.5f); //
            }
        }
    }

    public void turnContinueTextOn() {continueText.gameObject.SetActive(true);}

    public void turnContinueTextOff() { continueText.gameObject.SetActive(false); }

    private void StartTalkingSound() {
        dialogueAudioSource.Play();
    }


    private void StopTalkingSound() {
        //dialogueAudioSource.Stop();
        Debug.Log("you need audio");
    }

    private void Start() {
        //TextWriter.AddWriter_Static(messageText, "This is the assistant speaking, hello and goodbye, see you next time!", .1f, true);
    }

}
