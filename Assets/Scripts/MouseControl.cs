using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MouseControl : MonoBehaviour
{
    public GameObject Block;
    public Text BlockInfo;
    public int limit;
    public GameObject GlowImage;
    public int BlockCount;
    int BlocksRemaining;


    void Start()
    {
        BlockCount = 0;
    }

    void Update()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePosition;
        BlocksRemaining = limit - BlockCount;
        BlockInfo.text = "" + BlocksRemaining;

        if (BlocksRemaining == 3)
        {
            GlowImage.GetComponent<Transform>().localScale = new Vector3(5f, 5f, 1.0f);
        }

        if (BlocksRemaining == 2)
        {
            GlowImage.GetComponent<Transform>().localScale = new Vector3(4f, 4f, 1.0f);
        }

        if (BlocksRemaining == 1)
        {
            GlowImage.GetComponent<Transform>().localScale = new Vector3(3f, 3f, 1.0f);
        }

        if (BlocksRemaining == 0)
        {
            GlowImage.GetComponent<Transform>().localScale = new Vector3(0f, 0f, 1.0f);
        }


        if (BlockCount < limit)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    Instantiate(Block, hit.point, transform.rotation);
                    BlockCount++;
                }
            }
        }
    }
}