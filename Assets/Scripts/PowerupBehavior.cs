using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupBehavior : MonoBehaviour
{

    public PlayerController PlayerController;
    public MusicController MusicController;
    public Inventory Inventory;
    public int PowerupID; //0 is mush, 1 is invinciblity, 2 is skates, 3 is toolkit.
    private SpriteRenderer powerupSpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        PlayerController.onDeath += OnRespawn;
        powerupSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer
                == LayerMask.NameToLayer("Player"))
        {
            Inventory.AddItem(PowerupID);
            powerupSpriteRenderer.enabled = false;
        }
    }

    private void OnDisable()
    {
        PlayerController.onDeath -= OnRespawn;
    }

    void OnRespawn()
    {
        powerupSpriteRenderer.enabled = true;
    }
}
