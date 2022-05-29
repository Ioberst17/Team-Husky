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
    public AudioClip ReadySetGoClip;
    public AudioClip MenuButtonHoverClip;
    public AudioClip[] PetTheDogSounds; // assigned in inspector
    public AudioClip MainMenuToPlayGameDogBarkClip; //Pet the Dog (PTD) Sound - also used for Main Menu to Start
    public AudioClip PetTheDogPantingClip; // PTD Sound

    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0) // if it's the main menu
        {
            AudioSource.clip = MainMenuThemeClip; // assign the main menu theme to clip
            AudioSource.Play(); //have it play (loop is set in the inspector)
        }
    }
    public void MainMenuTheme()
    {
        AudioSource.PlayOneShot(MainMenuThemeClip);
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
}
