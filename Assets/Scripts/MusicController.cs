using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour
{
    public AudioSource AudioSource;
    public AudioSource PetTheDogSource;
    public AudioClip MainMenuThemeClip;
    public AudioClip LevelTheme;
    public AudioClip PowerupTheme;
    public AudioClip EndLevelTheme;
    public AudioClip OutOfBoundsClip;
    public AudioClip DamageClip;
    public AudioClip JumpClip;
    public AudioClip SpeedBoostClip;
    public AudioClip ToolKitClip;
    public AudioClip ReadySetGoClip;
    public AudioClip MenuButtonHoverClip;
    public AudioClip[] PetTheDogSounds; // assigned in inspector
    public AudioClip MainMenuToPlayGameDogBarkClip; //Pet the Dog (PTD) Sound - also used for Main Menu to Start
    public AudioClip PetTheDogPantingClip; // PTD Sound

    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().buildIndex == 1) // if it's the main menu or first cut scene
        {
            AudioSource.clip = MainMenuThemeClip; // assign the main menu theme to clip in the first audio source
            AudioSource.Play(); //have it play (loop is set in the inspector)
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2) // if it's the first level, play the level theme
        {
            AudioSource.clip = LevelTheme; // assign the main menu theme to clip in first audiosource
            AudioSource.Play(); //have it play (loop is set in the inspector)
        }

    }

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

    
        public void PetTheDogPanting()
    {
        AudioSource.PlayOneShot(PetTheDogPantingClip, 0.7F);
    }

    public void Toolkit()
    {
        AudioSource.PlayOneShot(ToolKitClip, 0.7F);
    }
}
