using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MouseControl : MonoBehaviour
{
    public GameObject Block;
    public int limit;
    public int BlockCount;


    void Start()
    {
        BlockCount = 0;
    }

    void Update()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePosition;

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