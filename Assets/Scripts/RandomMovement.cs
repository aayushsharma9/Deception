using UnityEngine;
using System.Collections;

public class RandomMovement : MonoBehaviour 
{
	public float speed;
	int h, v;

	void Awake()
	{
		h = Random.Range (-2, 2);
		v = Random.Range (-2, 2);
		while (true) 
		{
			if (h == 2)
				h = 1;
			if (h == -2)
				h = -1;
			if (v == 2)
				v = 1;
			if (v == -2)
				v = 2;

			if (h != 0 && v != 0 || h == 0 && v == 0) 
			{
				h = Random.Range (-2, 2);
				v = Random.Range (-2, 2);

				if (h == 2)
					h = 1;
				if (h == -2)
					h = -1;
				if (v == 2)
					v = 1;
				if (v == -2)
					v = 2;
			}

			else
			{
				break;
			}
		}		
	}

	void OnTriggerEnter (Collider Wall)
	{
		if (Wall.tag == "UserWall")
		{
			speed = speed * -1;
		}

		/*if (Wall.tag == "MazeWall") 
		{
			if (h == 0)
			{
				h = v;
				v = 0;
			}

			if (v == 0)
			{
				v = h;
				h = 0;
			}
		}*/
	}
	
	void FixedUpdate ()
	{
		GetComponent <Rigidbody> ().velocity = new Vector3 (h, v, 0.0f) * speed;
	}
}
