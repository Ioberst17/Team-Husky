using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour
{
    //public AudioSource AudioSource;
    public AudioSource MusicSource;
    public AudioSource MusicSource2;
    public AudioSource FXSource;
    public AudioSource PetTheDogSource;
    public AudioClip MainMenuThemeClip;
    public AudioClip LevelThemeIntro;
    public AudioClip LevelThemeLoop;
    public AudioClip PowerupTheme;
    public AudioClip EndLevelThemeIntro;
    public AudioClip EndLevelThemeLoop;
    public AudioClip OutOfBoundsClip;
    public AudioClip DamageClip;
    public AudioClip JumpClip;
    public AudioClip PowerupClip;
    public AudioClip SpeedBoostClip;
    public AudioClip ToolKitClip;
    public AudioClip GoldenSkatesClip;
    public AudioClip ReadySetGoClip;
    public AudioClip MenuButtonHoverClip;
    public AudioClip[] PetTheDogSounds; // assigned in inspector
    public AudioClip MainMenuToPlayGameDogBarkClip; //Pet the Dog (PTD) Sound - also used for Main Menu to Start
    public AudioClip PetTheDogPantingClip; // PTD Sound

    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().buildIndex == 1) // if it's the main menu or first cut scene
        {
            levelStart();
        }

    }

    public void levelStart()
    {
        //this will need to be updated once we have more complex levels
        MusicSource.clip = LevelThemeIntro;
        MusicSource2.clip = LevelThemeLoop;
        MusicSource.Play();
        MusicSource2.PlayDelayed(LevelThemeIntro.length-0.25f);
        MusicSource2.loop = true;
    }
    public void powerup()
    {

    }
    public void levelEnd()
    {
        MusicSource.Stop();
        MusicSource2.Stop();
        MusicSource.clip = EndLevelThemeIntro;
        MusicSource2.clip = EndLevelThemeLoop;
        MusicSource.Play();
        MusicSource2.PlayDelayed(EndLevelThemeIntro.length - 0.125f);
        MusicSource2.loop = true;
    }
    public void outOfBoundsFunction()
    {
        FXSource.PlayOneShot(OutOfBoundsClip);
    }
    public void DamageFunction()
    {
        FXSource.PlayOneShot(DamageClip, 0.33f);
    }
    public void JumpFunction()
    {
        FXSource.PlayOneShot(JumpClip);
    }
    public void SpeedBoostFunction()
    {
        FXSource.PlayOneShot(SpeedBoostClip);
    }
    public void ReadySetGoFunction()
    {
        //FXSource.PlayOneShot(ReadySetGoClip);
    }

    public void MenuButtonHoverFunction()
    {
        FXSource.PlayOneShot(MenuButtonHoverClip, 0.1F);
    }

    public void MainMenuToPlayGameDogBark()
    {
        FXSource.PlayOneShot(MainMenuToPlayGameDogBarkClip, 1.0F);
    }
    
        public void PetTheDogPanting()
    {
        FXSource.PlayOneShot(PetTheDogPantingClip, 0.7F);
    }

    public void Toolkit()
    {
        FXSource.PlayOneShot(ToolKitClip, 0.7F);
    }
    public void GoldenSkates()
    {
        FXSource.PlayOneShot(GoldenSkatesClip, 0.7F);
    }
    public void PowerupPickup()
    {
        FXSource.PlayOneShot(PowerupClip, 0.7F);
        //Debug.Log("got powerup sound played");
    }
}
