using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class PetTheDog : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    //attached to an invisible (Color Alpha = 0) image over the dogs in title screen

    public MusicController musicController;
    public GameManagerSupport gameManagerSupport;
    public Animator petTheDogTextRecord;
    public GameObject canvas;
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    public bool mouseDown = false;

    void Start()
    {
        musicController = FindObjectOfType<MusicController>();
        gameManagerSupport = FindObjectOfType<GameManagerSupport>();
        canvas = GameObject.Find("Canvas");
        petTheDogTextRecord = ObjectFinder.FindObject(canvas, "DogPats").transform.GetChild(1).GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        mouseDown = false;
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        mouseDown = true;
        if (!musicController.PetTheDogSource.isPlaying) // if dog isn't making sound
        {
            petTheDogTextRecord.SetTrigger("Pet");
            musicController.PetTheDogSource.clip = musicController.PetTheDogSounds[Random.Range(0, musicController.PetTheDogSounds.Length)];
            musicController.PetTheDogSource.Play();
            gameManagerSupport.petTheDogUIUpdate();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        mouseDown = false;
    }
}
