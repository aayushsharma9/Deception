using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ReachExit2 : MonoBehaviour
{
    public bool reached;
    public GameObject GameController, gameManager;
    public Transform[] exit;
    public GameObject RunnerOne, RunnerTwo, BonusBall;
    public Text LevelCount;
    public float MoveSpeed;
    public int r;
    UnityEngine.AI.NavMeshAgent nav;
    float t;
    int temp, otherExit, levelCount;
    bool paused, countDown;
    GameManager GManager;
    ReachExit1 otherScript;

    void Start()
    {
        reached = false;
        gameManager = GameObject.Find("GameManager");
        GManager = gameManager.GetComponent<GameManager>();
        levelCount = 0;
        paused = false;
        t = 0;
        levelCount = 1;
        exit[0] = GameObject.FindGameObjectWithTag("Exit1").transform;
        exit[1] = GameObject.FindGameObjectWithTag("Exit2").transform;
        exit[2] = GameObject.FindGameObjectWithTag("Exit3").transform;
        exit[3] = GameObject.FindGameObjectWithTag("Exit4").transform;
        exit[4] = GameObject.FindGameObjectWithTag("Exit5").transform;
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        MoveSpeed = nav.speed;
        otherScript = RunnerOne.GetComponent<ReachExit1>();
        r = Random.Range(0, 5);
    }

    void OnTriggerEnter (Collider Other)
    {
        temp = r;

        if (Other.tag == "PlayerWall")
        {
            while (temp == r || otherExit == r || BonusBall.GetComponent<BonusReachExit>().r == r)
            {
                r = Random.Range(0, 5);
            }
        }

        if (Other.tag == "Finish")
        {
            reached = true;
            GameController.SetActive(false);
            RunnerOne.SetActive(false);
            BonusBall.SetActive(false);
            gameObject.SetActive(false);
        }

    }

    void Update()
    {
        otherExit = otherScript.r;
        paused = GManager.paused;
        countDown = GManager.Counting;

        if (!countDown && !paused)
        {
            t += Time.deltaTime;
        }

        if (t >= 60 && !paused && !countDown && levelCount <= 3)
        {
            MoveSpeed += 2;
            levelCount++;
            t = 0;
        }

        nav.speed = MoveSpeed;
        nav.SetDestination(exit[r].position);
        CheckPause();
        CheckReach();
    }

    void CheckPause()
    {
        if (paused || countDown)
        {
            Time.timeScale = 0;
        }

        else
        {
            Time.timeScale = 1;
        }
    }

    void CheckReach()
    {
        if (reached)
        {
            Time.timeScale = 0;
        }
    }
}
