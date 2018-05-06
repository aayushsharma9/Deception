using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager: MonoBehaviour 
{
    const float COUNT_DOWN_WAIT_TIME = 0.7f;
	public bool paused;
	public GameObject PauseButton, PauseScreen, GameOver, GameController;
    public GameObject oCamera, soundToggle; 
    public TextMeshProUGUI GameOverFinalScore, GameOverHighScore;
	public bool reachedOne, reachedTwo, SameExit; 
	public int score;
    public float scoreTimer;
	public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI countText;
    public GameObject RunnerOne, RunnerTwo, BonusRunner;
    public  float timer;
    int HighScore, Sound;
    float scoreInterval;
    int level;
    bool gameOver;
    ReachExit1 RunnerOneScript;
    ReachExit2 RunnerTwoScript;
    BonusReachExit BonusRunnerScript;
    string RGlowColor;
    Color RColor;
    public GameObject CountDown;
    public bool Counting;
    public Light ScoreSpotlight;

    void Start()
    {
        RGlowColor = PlayerPrefs.GetString("GlowColor");

        switch(RGlowColor)
        {
            case "Cyan":
                RColor = Color.cyan;
                break;

            case "Green":
                RColor = Color.green;
                break;

            case "Purple":
                RColor = Color.green;
                break;

            case "White":
                RColor = Color.white;
                break;

        }

        ScoreSpotlight.color = RColor;
        ScoreSpotlight.bounceIntensity = 8;

        Application.targetFrameRate = 30;
        //PlayerPrefs.DeleteAll();
        Time.timeScale = 0;
        ScoreSpotlight.GetComponent<Animator>().enabled = false;
        //Taking saved Highscore
        level = PlayerPrefs.GetInt("Level");

        switch(level)
        {
            case 1:
                HighScore = PlayerPrefs.GetInt("L1HighScore");
                break;

            case 2:
                HighScore = PlayerPrefs.GetInt("L2HighScore");
                break;

        }
   
		score = 0;
        CheckSound();

        //Initializing other variables 
        RunnerOneScript = RunnerOne.GetComponent <ReachExit1> ();
		RunnerTwoScript = RunnerTwo.GetComponent <ReachExit2> ();
        BonusRunnerScript = BonusRunner.GetComponent <BonusReachExit> ();
        RunnerOneScript.r = 3;
        RunnerTwoScript.r = 4;
		Physics.IgnoreCollision (RunnerOne.GetComponent<Collider> (), RunnerTwo.GetComponent<Collider> ()); //Runners will pass through each other 
        // PauseScreen = GameObject.FindGameObjectWithTag ("PauseScreen"); 
        // GameOver = GameObject.FindGameObjectWithTag("GameOverPanel");
        PauseButton.SetActive(false);        
        CountDown.SetActive(false);
		GameOver.SetActive(false);
		PauseScreen.SetActive (false);
        BonusRunner.SetActive(false);
		paused = false;
        gameOver = false;
        timer = 0;
        scoreInterval = 0.1f;

        //COUNTING
        countText = CountDown.GetComponent<TextMeshProUGUI>();
        StartCoroutine(GetReady());
    }

    void Update()
	{
        reachedOne = RunnerOneScript.reached; //Checking reached at every frame
        reachedTwo = RunnerTwoScript.reached;

        if (reachedOne == true || reachedTwo == true)
        {
            gameOver = true;
			GameOver.SetActive (true);
			GameOverFinalScore.text = "Your Score: " + score;
            GameOverHighScore.text = "High Score: " + HighScore;
            Time.timeScale = 0;
            ScoreSpotlight.GetComponent<Animator>().enabled = false;
            ScoreSpotlight.color = RColor;
        }
        
        if (!gameOver && !paused && !Counting)
        {
            Timer();
        }
        
        if (RunnerOneScript.r == RunnerTwoScript.r)
        {
            SameExit = true;
            RunnerOneScript.r = Random.Range(0, 5);
            RunnerTwoScript.r = Random.Range(0, 5);
        }

        else
        {
            SameExit = false;
        }
	}

	void Timer()
	{
		scoreTimer += Time.deltaTime;
        timer += Time.deltaTime;
        
        if (timer >= 30)
        {
            Bonus(true);
            timer = 0;            
        }

        if (BonusRunnerScript.reached)
        {
            Bonus(false);
            BonusRunnerScript.reached = false;
            timer = 0;
        }

        if (scoreTimer >= scoreInterval)
        {
            score++;
            scoreTimer = 0;
        }

        if (HighScore < score)
        {
            HighScore = score;

            switch(level)
            {
                case 1:
                    PlayerPrefs.SetInt("L1HighScore", HighScore);
                    break;

                case 2:
                    PlayerPrefs.SetInt("L2HighScore", HighScore);
                    break;
            }
        }

		ScoreText.text = "SCORE: " + score;
	}

	public void ChangeSceneTo(string Scene) 
	{
        SceneManager.LoadScene(Scene);
	}

    public void Pause ()
	{
		paused = !paused;

		if (paused) //Pause everything
		{
            CheckSound();
            oCamera.GetComponent<BlurOptimized>().enabled = true;
            Time.timeScale = 0;
			AudioSource audio = GameController.GetComponent<AudioSource> ();
			audio.Pause ();
            GameController.GetComponent<MouseControl>().enabled = false;
            PauseButton.SetActive(false);
			PauseScreen.SetActive (true);
		} 

		else // Play everything
        {
            CheckSound();
            oCamera.GetComponent<BlurOptimized>().enabled = false;
            StartCoroutine(GetReady());
            Time.timeScale = 1;
            PauseScreen.SetActive (false);
		}
	}

    IEnumerator GetReady()
    {
        oCamera.GetComponent<BlurOptimized>().enabled = true;       
        Counting = true;
        AudioSource audio = GameController.GetComponent<AudioSource>();
        audio.Pause();
        GameController.GetComponent<MouseControl>().enabled = false;
        CountDown.SetActive(true);

        countText.text = "3";
        yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(COUNT_DOWN_WAIT_TIME));
        countText.text = "2";
        yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(COUNT_DOWN_WAIT_TIME));
        countText.text = "1";
        yield return StartCoroutine(CoroutineUtil.WaitForRealSeconds(COUNT_DOWN_WAIT_TIME));

        countText.text = null;
        CountDown.SetActive(false);
        Counting = false;
        oCamera.GetComponent<BlurOptimized>().enabled = false;       
        //Activating everything
        audio.Play();
        PauseButton.SetActive(true);
        GameController.GetComponent<MouseControl>().enabled = true;
        Time.timeScale = 1;
    }


    void CheckSound()
    {
        AudioSource audio = GameController.GetComponent<AudioSource>();
        Sound = PlayerPrefs.GetInt("Sound");

        switch(Sound)
        {
            case 1:
                audio.mute = true;
                soundToggle.GetComponent<Toggle>().isOn = false;
                break;

            case 2:
                audio.mute = false;
                soundToggle.GetComponent<Toggle>().isOn = true;
                break;

        }
    }

    void Bonus(bool active)
    {
        if (active)
        {
            BonusRunner.SetActive(true);
            scoreInterval = 0.05f;
            ScoreSpotlight.GetComponent<Animator>().enabled = true;
            BonusRunnerScript.RandomizeExit();
        }
        
        else
        {
            BonusRunnerScript.r = 5;
            BonusRunner.SetActive(false);
            scoreInterval = 0.1f;
            BonusRunner.GetComponent<Transform>().position = new Vector3(0.0f, 0.0f, 0.0f);
            ScoreSpotlight.GetComponent<Animator>().enabled = false;
            ScoreSpotlight.color = RColor;
        }
    }
}
