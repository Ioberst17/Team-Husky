using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using Coffee.UIEffects;

public class OverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public MusicController musicController;
    public UIShiny shinyUI;

    void Start()
    {
        musicController = FindObjectOfType<MusicController>();
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        musicController.MenuButtonHoverFunction();
        var shinyUI = gameObject.GetComponent<UIShiny>();
        shinyUI.Play();
    }
    public void OnPointerExit(PointerEventData eventData)
    {

    }

}