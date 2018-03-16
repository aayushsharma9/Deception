using UnityEngine;
using System.Collections;

public class Pointer: MonoBehaviour 
{
	public void Update()
	{
		var PointerOffset = new Vector3 (0.0f, 0.0f, 5.3f);
		var mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		transform.position = mousePosition + PointerOffset;
	}
}
