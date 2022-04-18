using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehavior : MonoBehaviour
{
    public bool isStart;
    public bool isQuit;
   
    void OnMouseUp()
    {
        if (isStart)
        {
            
            SceneManager.LoadScene("SampleScene");
        }
        if (isQuit)
        {
            Application.Quit();
        }
    }
}
