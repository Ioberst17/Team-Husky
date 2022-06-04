
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
    public GameManagerSupport gameManagerSupport;
    private TextWriter.TextWriterSingle textWriterSingle;
    private AudioSource dialogueAudioSource;
    public string[] dialogueArray;
    private int arrayTracker = 0;
    public Text continueText;
    public GameObject canvas;
    public Text contextText;
    public Text theEndText;

    private void Awake() {
        gameManagerSupport = GameObject.FindObjectOfType<GameManagerSupport>();
        dialogueText = transform.Find("Dialogue").Find("DialogueText").GetComponent<Text>();
        /*if (SceneManager.GetActiveScene().buildIndex == 1) // This is all handled via script on text now
        {
            canvas = GameObject.Find("Canvas");
            contextText = ObjectFinder.FindObject(canvas, "ContextText").GetComponent<Text>();
        }*/
        if (SceneManager.GetActiveScene().buildIndex == 7) // if final cutscene needs The End
        {
            canvas = GameObject.Find("Canvas");
            theEndText = ObjectFinder.FindObject(canvas, "TheEndText").GetComponent<Text>();
        }
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 1:
                dialogueArray = new string[]
            {
                "“Have you ever heard the tale of Darth Musher? It's not a story that anyone would tell y-”",
                "“Come on, Grandpa! Just tell us the real story.”",
                "“Alright, alright - I'll tell you about How I Met Your Grandmoth-”",
                "“Grandpa!”",
                "“... alright, here's the story - it was many years ago now”",
                "“It was a day like any other, until I got the call to deliver medicine up to Nome”",
                "“It wasn't going to be easy, but I packed and readied myself before leaving town”"
            };
                break;
            case 3:
                dialogueArray = new string[]
            {
                "“The easy part of the journey was over, next we'd have to make it through the Ice Caverns”"
            };
                break;
            case 5:
                dialogueArray = new string[]
            {
                "“We made it out just in time. The dogs and I were tired, but we'd have to make it through one last push to get to Nome”",
                "“Unfortunate for us, this last sprint would really test our mettle”"
            };
                break;
            case 7:
                dialogueArray = new string[]
            {
                "“And that you see, is the story of how I beat the Elite Four”",
                "“... Gramps, it's amazing how you can be so unfunny, yet still cool”",
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
                    StartCoroutine(TheEndRoutine());
                }
                else
                {
                    gameManagerSupport.gameManagerLoader(SceneManager.GetActiveScene().buildIndex+1); // need to use this function to enable screen transitions + saving scene history
                    //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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

    /*IEnumerator ContextTextFade() // handled on script attached to object
    {
        yield return new WaitForSeconds(3F);
        contextText.GetComponent<Animator>().SetTrigger("FadeOut");
        yield return null;
    }*/

    IEnumerator TheEndRoutine()
    {
        theEndText.GetComponent<Animator>().SetTrigger("FadeIn");
        yield return new WaitForSeconds(2.5F);
        gameManagerSupport.gameManagerLoader(0);
        yield return null;
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
   

}
