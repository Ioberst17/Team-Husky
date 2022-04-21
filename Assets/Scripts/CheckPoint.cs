using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    // have we been triggered?
    bool triggered;
    [SerializeField] private SpriteRenderer triggerIndicator;
    [SerializeField] private Transform spawnPoint;

    void Awake()
    {
        triggered = false;
    }
    // called whenever another collider enters our zone (if layers match)
    void OnTriggerEnter2D(Collider2D collider)
    {


        // check we haven't been triggered yet!
        if (!triggered)
        {
            
            if (collider.gameObject.layer
                == LayerMask.NameToLayer("Player"))
            {
                Trigger();
            }
        }
    }
    void Trigger()
    {
        triggerIndicator.color = new Color(1, 0, 0, 1);
        triggered = true;
        spawnPoint.transform.position = this.transform.position;
        Debug.Log("Checkpoint Triggered");
    }
    
    
}
