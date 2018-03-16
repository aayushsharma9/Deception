using UnityEngine;
using System.Collections;

public class RestartOnClick : MonoBehaviour 
{
	void OnMouseOver()
	{
		if (Input.GetMouseButtonDown (0)) {
			Application.LoadLevel (Application.loadedLevel);
		}
	}
}
