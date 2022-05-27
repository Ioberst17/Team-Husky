using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioSource AudioSource;
    public AudioClip LevelTheme;
    public AudioClip PowerupTheme;
    public AudioClip EndLevelTheme;
    public AudioClip OutOfBoundsClip;
    public AudioClip DamageClip;
    public AudioClip JumpClip;
    public AudioClip SpeedBoostClip;
    public AudioClip ReadySetGoClip;
    public AudioClip MenuButtonHoverClip;
    public AudioClip MainMenuToPlayGameDogBarkClip;


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
    public void outOfBoundsFunction()
    {
        AudioSource.PlayOneShot(OutOfBoundsClip);
    }
    public void DamageFunction()
    {
        AudioSource.PlayOneShot(DamageClip);
    }
    public void JumpFunction()
    {
        AudioSource.PlayOneShot(JumpClip);
    }
    public void SpeedBoostFunction()
    {
        AudioSource.PlayOneShot(SpeedBoostClip);
    }
    public void ReadySetGoFunction()
    {
        AudioSource.PlayOneShot(ReadySetGoClip);
    }

    public void MenuButtonHoverFunction()
    {
        AudioSource.PlayOneShot(MenuButtonHoverClip, 0.1F);
    }

    public void MainMenuToPlayGameDogBark()
    {
        AudioSource.PlayOneShot(MainMenuToPlayGameDogBarkClip, 0.2F);
    }
}
