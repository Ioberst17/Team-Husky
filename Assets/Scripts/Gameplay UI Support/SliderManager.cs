using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    public Transform[]children;
    public List<Transform> kids;
    public Button controlMenuButton;
    private int currentIndex = 0;
    public Button nextButton;
    public Button backButton;

    // Start is called before the first frame update
    void Awake()
    {
        children = GetComponentsInChildren<Transform>();

        //controlMenuButton.onClick.AddListener(OpenFirstGameObject);
        foreach (Transform child in transform)
        {
            Debug.Log(child.name);
            kids.Add(child);
        }
    }

    // Update is called once per frame
    public void OpenFirstGameObject()
    {
        for(int i = 0; i < kids.Count; i++)
        {
            kids[i].gameObject.SetActive(false);
        }

        kids[0].gameObject.SetActive(true);
        currentIndex = 0;
    }

    public void OpenNextGameObject()
    {
        kids[currentIndex].gameObject.SetActive(false);
        if(currentIndex + 1 >= kids.Count)
        {
            kids[0].gameObject.SetActive(true);
            currentIndex = 0;
        }
        else
        {
            kids[currentIndex + 1].gameObject.SetActive(true);
            currentIndex++;
        }  
        
    }

    public void OpenPrevGameObject()
    {
        kids[currentIndex].gameObject.SetActive(false);
        if (currentIndex - 1 < 0)
        {
            kids[kids.Count - 1].gameObject.SetActive(true);
            currentIndex = kids.Count - 1;
        }
        else
        {
            kids[currentIndex - 1].gameObject.SetActive(true);
            currentIndex--;
        }
        
    }
}