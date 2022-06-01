//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine.SceneManagement;
//using UnityEngine;
//using UnityEditor;
//using UnityEditor.Events;
//using UnityEngine.UI;
//using UnityEngine.Events;


// Do not use, incomplete
/*
[ExecuteInEditMode]
public class ChangeButtons : MonoBehaviour
{
    private const string MenuItemName = "Utilities/ChangeButtons";
    MusicController musicController = FindObjectOfType<MusicController>();
    public AudioSource audioSrc;
    [SerializeField]
    public UnityEvent unityEvent;
    UnityAction methodDelegate;
    bool eventAdded = false;

    [MenuItem(MenuItemName)]
    private static void ButtonChangerMain(MusicController musicController) //iterate through all scenes and get all root objects
    {
        var sceneMatches = 0;
        for (var i = 0; i < SceneManager.sceneCount; i++)
        {
            var scene = SceneManager.GetSceneAt(i);
            var gos = new List<GameObject>(scene.GetRootGameObjects());
            foreach (var go in gos)
            {
                sceneMatches += ButtonChangerSupport(go.GetComponentsInChildren<Button>(), musicController); 
            }
        }
    }

    private static int ButtonChangerSupport(IEnumerable<Button> buttons, MusicController musicControllerloc) //iterate overall buttons
    {
        var matches = 0;
        foreach (var button in buttons)
        {
            var go = new GameObject();
            UnityAction<MusicController> action = new UnityAction<MusicController>(musicControllerloc.MenuButtonHoverFunction());
            UnityEventTools.AddObjectPersistentListener<MusicController>(button.OnPointerEnter(), action, button);
            matches++;
        }
        return matches;
    }

    void UpdateInspector()
    {
        if (!eventAdded)
        {
            audioSrc = GetComponent<AudioSource>();
            unityEvent = new UnityEvent();
            methodDelegate = System.Delegate.CreateDelegate(typeof(UnityAction), musicController, "MenuButtonHoverFunction") as UnityAction;
            UnityEventTools.AddPersistentListener(unityEvent, methodDelegate);
            eventAdded = true;
        }
    }

}*/
