using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject MainMenuPanel, LevelConfigPanel, CreditsPanel, TutorialPanel, ExitConfirmationPanel; //Panels
    [SerializeField]
    private GameObject soundToggle, Level1Toggle, Level2Toggle;
    //public GameObject CyanToggle, GreenToggle, PurpleToggle, WhiteToggle, soundToggle, Level1Toggle, Level2Toggle;
    //public GameObject L1Cyan, L1Green, L1Magenta, L1White;
    //public GameObject L2Cyan, L2Green, L2Magenta, L2White;
    string GlowColor;
    bool SoundMute, LevelChange;
    int Level, Sound;

    void Start()
    {
        //PlayerPrefs.DeleteAll();
        MainMenuPanel.SetActive(true);
        LevelConfigPanel.SetActive(false);
        CreditsPanel.SetActive(false);
        TutorialPanel.SetActive(false);
        GlowColor = PlayerPrefs.GetString("GlowColor");
        Sound = PlayerPrefs.GetInt("Sound");
        Level = PlayerPrefs.GetInt("Level");
        CheckSound();
        //CheckColor();
        CheckLevel();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ConfirmExit();
        }

        if (Input.GetKeyDown(KeyCode.Home))
        {
            Application.Quit();
        }
    }

    public void SetColor(string color)
    {
        GlowColor = color;
        PlayerPrefs.SetString("GlowColor", GlowColor);

        //CheckColor();
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void SoundToggle()
    {
        SoundMute = !SoundMute;
        AudioSource audio = gameObject.GetComponent<AudioSource>();

        if (SoundMute)
        {
            Sound = 1;
            PlayerPrefs.SetInt("Sound", Sound);
            audio.mute = true;
        }

        if (!SoundMute)
        {
            Sound = 2;
            PlayerPrefs.SetInt("Sound", Sound);
            audio.mute = false;
        }
    }

    public void OpenTutorial()
    {
        TutorialPanel.SetActive(true);
        MainMenuPanel.SetActive(false);
    }

    public void OpenLevelConfig ()
    {
        //CheckColor();
        LevelConfigPanel.SetActive(true);
        MainMenuPanel.SetActive(false);
    }

    void CheckSound()
    {
        AudioSource audio = gameObject.GetComponent<AudioSource>();
        Sound = PlayerPrefs.GetInt("Sound");

        while (true)
        {
            //Initializing Sound Toggle
            if (Sound == 2)
            {
                soundToggle.GetComponent<Toggle>().isOn = true;
                audio.mute = false;
                break;
            }

            else if (Sound == 1)
            {
                soundToggle.GetComponent<Toggle>().isOn = false;
                audio.mute = true;
                break;
            }

            else
            {
                Sound = 2;
                PlayerPrefs.SetInt("Sound", Sound);
            }
        }
    }

    public void CreditsEnable()
    {
        MainMenuPanel.SetActive(false);
        CreditsPanel.SetActive(true);
    }

    public void LevelConf()
    {
        CheckLevel();
        //CheckColor();
        LevelConfigPanel.SetActive(true);
    }

    public void CheckLevel()
    {
        Level = PlayerPrefs.GetInt("Level");
        //CheckColor();

        switch(Level)
        {
            case 1:
                Level1Toggle.GetComponent<Toggle>().isOn = true;
               // Level2Toggle.GetComponent<Toggle>().isOn = false;
                break;

            case 2:
                //Level1Toggle.GetComponent<Toggle>().isOn = false;
                Level2Toggle.GetComponent<Toggle>().isOn = true;
                break;

            default:
                changeLevelTo(1);
                break;
        }
    }

    public void Play()
    {
        SceneManager.LoadScene("Loading", LoadSceneMode.Single);
        //Application.LoadLevel("Loading");
    }

    public void changeLevelTo(int n)
    {
        Level = n;
        PlayerPrefs.SetInt("Level", Level);
    }

    public void GoBack()
    {
        LevelConfigPanel.SetActive(false);
        CreditsPanel.SetActive(false);
        ExitConfirmationPanel.SetActive(false);
        TutorialPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
    }

    public void ConfirmExit()
    {
        MainMenuPanel.SetActive(false);
        ExitConfirmationPanel.SetActive(true);
    }

    /*void CheckColor()
    {
        while (true)
        {
            if (GlowColor == "Cyan")
            {
                CyanToggle.GetComponent<Toggle>().isOn = true;
                L1Cyan.SetActive(true);
                L1Green.SetActive(false);
                L1Magenta.SetActive(false);
                L1White.SetActive(false);
                L2Cyan.SetActive(true);
                L2Green.SetActive(false);
                L2Magenta.SetActive(false);
                L2White.SetActive(false);
                break;
            }

            if (GlowColor == "Green")
            {
                GreenToggle.GetComponent<Toggle>().isOn = true;
                L1Cyan.SetActive(false);
                L1Green.SetActive(true);
                L1Magenta.SetActive(false);
                L1White.SetActive(false);
                L2Cyan.SetActive(false);
                L2Green.SetActive(true);
                L2Magenta.SetActive(false);
                L2White.SetActive(false);
                break;
            }

            if (GlowColor == "Purple")
            {
                PurpleToggle.GetComponent<Toggle>().isOn = true;
                L1Cyan.SetActive(false);
                L1Green.SetActive(false);
                L1Magenta.SetActive(true);
                L1White.SetActive(false);
                L2Cyan.SetActive(false);
                L2Green.SetActive(false);
                L2Magenta.SetActive(true);
                L2White.SetActive(false);
                break;
            }

            if (GlowColor == "White")
            {
                WhiteToggle.GetComponent<Toggle>().isOn = true;
                L1Cyan.SetActive(false);
                L1Green.SetActive(false);
                L1Magenta.SetActive(false);
                L1White.SetActive(true);
                L2Cyan.SetActive(false);
                L2Green.SetActive(false);
                L2Magenta.SetActive(false);
                L2White.SetActive(true);
                break;
            }

            else
            {
                GlowColor = "Cyan";
                PlayerPrefs.SetString("GlowColor", GlowColor);
            }
        }

        PlayerPrefs.Save();
    }*/
}
