using UnityEngine;
using System.Collections;

public class BonusReachExit : MonoBehaviour
{
    public bool reached;
    public GameObject GameController, gameManager;
    public GameObject RunnerOne, RunnerTwo;
    public Transform[] exit;
    public int r;
    public float MoveSpeed;
    UnityEngine.AI.NavMeshAgent nav;
    public float t;
    int temp, levelCount;
    bool paused, countDown;
    GameManager GManager;

    void Start()
    {
        reached = false;
        gameManager = GameObject.Find("GameManager");
        GManager = gameManager.GetComponent<GameManager>();
        paused = false;
        exit[0] = GameObject.FindGameObjectWithTag("Exit1").transform;
        exit[1] = GameObject.FindGameObjectWithTag("Exit2").transform;
        exit[2] = GameObject.FindGameObjectWithTag("Exit3").transform;
        exit[3] = GameObject.FindGameObjectWithTag("Exit4").transform;
        exit[4] = GameObject.FindGameObjectWithTag("Exit5").transform;
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        RandomizeExit();
    }

    void OnTriggerEnter(Collider Other)
    {
        temp = r;

        if (Other.tag == "PlayerWall")
        {
            RandomizeExit();
        }

        if (Other.tag == "Finish")
        {
            reached = true;
        }
    }

    void Update()
    {
        if (gameObject.activeSelf == false)
        {
            r = 5;
        }

        paused = GManager.paused;
        countDown = GManager.Counting;
        nav.SetDestination(exit[r].position);
        CheckPause();
        nav.speed = MoveSpeed;
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

    public void RandomizeExit()
    {
        do
        {
            r = Random.Range(0, 5);
        }
        while (RunnerOne.GetComponent<ReachExit1>().r == r || RunnerTwo.GetComponent<ReachExit2>().r == r || temp == r);
    }
}