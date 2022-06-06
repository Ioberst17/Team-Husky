using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour
{
    //public AudioSource AudioSource;
    public GameManager gameManager = GameManager.Instance;
    public float introDelayTime;
    public AudioSource MusicSource;
    public AudioSource MusicSource2;
    public AudioSource InvincibleMusicSource;
    public AudioSource FXSource;
    public AudioSource PetTheDogSource;
    public AudioClip MainMenuThemeClip;
    public AudioClip LevelThemeIntro;
    public AudioClip LevelThemeLoop;
    public AudioClip LevelThemeInvincibility;
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

    [SerializeField] private AudioClip SnowDamageClip;
    [SerializeField] private AudioClip RockDamageClip;
    [SerializeField] private AudioClip HazardDamageClip;
    [SerializeField] private AudioClip IceDamageClip;


    public void levelStart()
    {

    }
    public void Start()
    {
        //this will need to be updated once we have more complex levels
        // play music sources
        MusicSource.clip = LevelThemeIntro;
        MusicSource2.clip = LevelThemeLoop;
        if (LevelThemeInvincibility != null) { InvincibleMusicSource.clip = LevelThemeInvincibility; } // only needed on playable levels
        // make sure invincible theme plays, but starts at 0 volume
        if (LevelThemeInvincibility != null) {InvincibleMusicSource.volume = 0; } // only needed on playable levels
        // play music sources
        MusicSource.Play();
        MusicSource2.PlayDelayed(LevelThemeIntro.length-introDelayTime);
        MusicSource2.PlayDelayed(LevelThemeIntro.length - introDelayTime);
        // loop key music sources
        MusicSource2.loop = true;
        if (LevelThemeInvincibility != null) {InvincibleMusicSource.loop = true; } // only needed on playable levels
    }


    public void InvincibileThemeOn()
    {
        StartCoroutine(FadeAudioSource.StartFade(MusicSource2, 1F, 0)); // fade out current theme (parameter 1), over 1 second (param 2), to 0 volume (param 3)
        StartCoroutine(FadeAudioSource.StartFade(InvincibleMusicSource, 1F, 1)); // fade in Invincibility Loop (param 1), over 1 second (param 2), to volume 1 (param 3)
    }

    public void InvincibileThemeOff()
    {
        StartCoroutine(FadeAudioSource.StartFade(InvincibleMusicSource, 1F, 0)); // fade out Invincible
        StartCoroutine(FadeAudioSource.StartFade(MusicSource2, 1F, 1)); // fade in current
    }

    public void powerup()
    {

    }
    public void snowDamage()
    {
        FXSource.PlayOneShot(SnowDamageClip, 15.33f);
    }
    public void rockDamage()
    {
        FXSource.PlayOneShot(RockDamageClip, 8.33f);
    }
    public void hazardDamage()
    {
        //FXSource.PlayOneShot(HazardDamageClip, 10.33f);
    }
    public void iceDamage()
    {
        FXSource.PlayOneShot(IceDamageClip, 10.33f);
    }
    public void levelEnd()
    {
        MusicSource.Stop();
        MusicSource2.Stop();
        InvincibleMusicSource.Stop();
        MusicSource.clip = EndLevelThemeIntro;
        MusicSource2.clip = EndLevelThemeLoop;
        MusicSource.Play();
        MusicSource2.volume = 1;
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
        FXSource.PlayOneShot(JumpClip, 2f);
    }
    public void SpeedBoostFunction()
    {
        FXSource.PlayOneShot(SpeedBoostClip, 2f);
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
        FXSource.PlayOneShot(PetTheDogPantingClip, 1.7F);
    }

    public void Toolkit()
    {
        FXSource.PlayOneShot(ToolKitClip, 3.7F);
    }
    public void GoldenSkates()
    {
        FXSource.PlayOneShot(GoldenSkatesClip, 5.7F);
    }
    public void PowerupPickup()
    {
        FXSource.PlayOneShot(PowerupClip, 3.7F);
        //Debug.Log("got powerup sound played");
    }
}
