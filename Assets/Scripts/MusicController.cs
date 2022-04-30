using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioSource AudioSource;
    public AudioClip LevelTheme;
    public AudioClip PowerupTheme;
    public AudioClip EndLevelTheme;

    public void levelStart()
    {
        //this will need to be updated once we have more complex levels
        AudioSource.PlayOneShot(LevelTheme);
    }
    public void powerup()
    {

    }
    public void levelEnd()
    {
        AudioSource.Stop();
        AudioSource.PlayOneShot(EndLevelTheme);
    }
}
