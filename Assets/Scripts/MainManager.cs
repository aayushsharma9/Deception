using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class MainManager : MonoBehaviour
{
    public GameObject GameController, GameManager;
    public GameObject CyanSphere1, PurpleSphere1, GreenSphere1, WhiteSphere1, CyanSphere2, PurpleSphere2, GreenSphere2, WhiteSphere2;
    public Light RunnerOneGlow1, RunnerOneGlow2, RunnerTwoGlow1, RunnerTwoGlow2;
    public string GlowColor;
    bool SoundMute;
    int Sound;

    void Start()
    {
        //Initializing Toggles
        GlowColor = PlayerPrefs.GetString("GlowColor");
        CheckColor();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.LoadLevel("Main Menu");
        }      

        if (Input.GetKeyDown(KeyCode.Home))
        {
            Application.Quit();
        }
    }

    public void BackToMainMenu()
    {
        Application.LoadLevel("Main Menu");
    }

    public void SoundToggle()
    {
        SoundMute = !SoundMute;

        if (SoundMute)
        {
            AudioSource audio = GameController.GetComponent<AudioSource>();
            audio.mute = true;
            Sound = 1;
            PlayerPrefs.SetInt("Sound", Sound);
        }

        if (!SoundMute)
        {
            AudioSource audio = GameController.GetComponent<AudioSource>();
            audio.mute = false;
            Sound = 2;
            PlayerPrefs.SetInt("Sound", Sound);
        }
    }
    
    public void Exit()
    {
        Application.Quit();
    }
    
    public void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    void CheckColor()
    {
        if (GlowColor == "Cyan")
        {
            RunnerOneGlow1.color = Color.cyan;
            RunnerOneGlow2.color = Color.cyan;
            RunnerTwoGlow1.color = Color.cyan;
            RunnerTwoGlow2.color = Color.cyan;
        }

        if (GlowColor == "Green")
        {
            RunnerOneGlow1.color = Color.green;
            RunnerOneGlow2.color = Color.green;
            RunnerTwoGlow1.color = Color.green;
            RunnerTwoGlow2.color = Color.green;
        }

        if (GlowColor == "Purple")
        {
            RunnerOneGlow1.color = Color.magenta;
            RunnerOneGlow2.color = Color.magenta;
            RunnerTwoGlow1.color = Color.magenta;
            RunnerTwoGlow2.color = Color.magenta;
        }

        if (GlowColor == "White")
        {
            RunnerOneGlow1.color = Color.white;
            RunnerOneGlow2.color = Color.white;
            RunnerTwoGlow1.color = Color.white;
            RunnerTwoGlow2.color = Color.white;
        }

        PlayerPrefs.Save();
    }
}     