using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PetTheDog : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    //attached to an invisible (Color Alpha = 0) image over the dogs in menus / title screens

    // for the MusicController need to make sure dog sounds are attached in inspector of music controller
    // drag MusicController onto PetTheDogSource in inspector
    // also make sure that the level music is playing using the MusicController.clip function; otherwise clips will not play
    public MusicController musicController; 
    public GameManagerSupport gameManagerSupport;
    public GameManager gameManager;
    public Animator petTheDogTextRecord;
    public GameObject canvas;
    public Texture2D cursorTexture; // assigned in inspector*
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    public bool mouseDown = false;

    void Start()
    {
        musicController = FindObjectOfType<MusicController>();
        gameManagerSupport = FindObjectOfType<GameManagerSupport>();
        gameManager = GameManager.Instance;
        if (SceneManager.GetActiveScene().buildIndex == 0) // only used on the menu page
        {
            canvas = GameObject.Find("Canvas");
            petTheDogTextRecord = ObjectFinder.FindObject(canvas, "DogPats").transform.GetChild(1).GetComponent<Animator>();
        }
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
            if (SceneManager.GetActiveScene().buildIndex == 0) // only used on the menu page
            {
                petTheDogTextRecord.SetTrigger("Scale");
                gameManagerSupport.petTheDogUIUpdate();
            }
            else
            {
                gameManager.gameData.patsToTheDog++;
            }
            musicController.PetTheDogSource.clip = musicController.PetTheDogSounds[Random.Range(0, musicController.PetTheDogSounds.Length)];
            musicController.PetTheDogSource.Play();
            Debug.Log("dog pet");
            
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        mouseDown = false;
    }
}
