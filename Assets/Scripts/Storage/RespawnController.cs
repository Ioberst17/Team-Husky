using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnController : MonoBehaviour
{
    public CheckPoint respawningCheckPoint = null;

    public delegate void MyDelegate();
    public event MyDelegate onRespawn;
    Vector2 initialPosition;

    void Awake()
    {
        initialPosition = transform.position;
        //respawningCheckPoint.onRespawn += OnRespawn;

        //if (checkpoint == null)
        //{
            //Debug.LogWarning("You forgot to assign a checkpoint to " + gameObject.ToString());
        //}
    }
    public void OnRespawn()
    {
        transform.position = initialPosition;
        onRespawn();
    }
}
