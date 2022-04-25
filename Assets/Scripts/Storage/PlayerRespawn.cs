using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;
    //[SerializeField] private Transform checkPoint;
    //private Transform respawnPoint;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
            //player.transform.position = respawnPoint.transform.position;
            
    }
}
