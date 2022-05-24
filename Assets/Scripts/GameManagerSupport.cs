using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSupport : MonoBehaviour
{
    // used for buttons, finds GameManager and loads its functions in scenes
    // this is because otherwise, buttons that link to GameManager would not be able to find the reference to GameManager if added in the hierarchy and then in play mode

    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void gameManagerLoader(int sceneID)
    {
        gameManager.LoadScene(sceneID);
    }

    public void gameManagerDataClear()
    {
        gameManager.clearData();
    }

    public void HandleButtonClick()
    {
        if (gameManager != null)
        {
            gameManager.clearData();
        }
        else
        {
            Debug.Log("Can't find game manager instance");
        }
    }
}