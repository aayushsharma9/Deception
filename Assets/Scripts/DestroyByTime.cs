using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour
{
	public float lifetime;
	float t = 0;
    public GameObject GameController;

    void Start()
    {
        GameController = GameObject.Find("GameController");
    }

	void  Update ()
	{
		t += Time.deltaTime;

		if (t >= lifetime)
        {
            GameController.GetComponent<MouseControl>().BlockCount--;
            Destroy(gameObject, 0);
            t = 0;
        }
    }
}