using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public Transform player;
    public float Hleeway;
    public float Vleeway;
    private float leadtimer = 0;
    private float previousPositionX;
    private float previousPositionY;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(player.position.x, player.position.y, -5);
        previousPositionX = transform.position.x;
        previousPositionY = transform.position.y;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (((transform.position.x) - (player.position.x)) > Hleeway)
        {
            transform.position = new Vector3(transform.position.x - ((transform.position.x - player.position.x) - Hleeway), transform.position.y, -5);
        }
        if (((transform.position.x) - (player.position.x)) < -Hleeway)
        {
            transform.position = new Vector3(transform.position.x - ((transform.position.x - player.position.x) + Hleeway), transform.position.y, -5);
        }

        if (player.position.y > previousPositionY)
        {
            if(leadtimer < 5)
            {
                leadtimer += player.position.y - previousPositionY;
                if (leadtimer > 5)
                {
                    leadtimer = 5;
                }
            }
        }
        if (player.position.y < previousPositionY)
        {
            if (leadtimer > -5)
            {
                leadtimer += player.position.y - previousPositionY;
                if (leadtimer < -5)
                {
                    leadtimer = -5;
                }
            }
        }

        //Debug.Log(leadtimer);
        transform.position = new Vector3(transform.position.x, player.position.y + (leadtimer / 2.5f), -5);
        previousPositionY = player.position.y;



        //if (((transform.position.y) - (player.position.y)) > Vleeway)
        //{
        //    transform.position = new Vector3(transform.position.x, transform.position.y - ((transform.position.y - player.position.y) - Vleeway + 2), -5);
        //}
        //if (((transform.position.y) - (player.position.y)) < -Vleeway)
        //{
        //    transform.position = new Vector3(transform.position.x, transform.position.y - ((transform.position.y - player.position.y) + Vleeway + 2), -5);
        //}
        //transform.position = new Vector3(player.position.x, player.position.y, -5);
    }
}
