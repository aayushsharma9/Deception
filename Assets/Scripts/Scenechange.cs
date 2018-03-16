using UnityEngine;
using System.Collections;

public class Scenechange : MonoBehaviour
{
    public string scene;
    public int time;

	void Start()
    {
        StartCoroutine(ChangeSceneToAfter(scene, time));
    }

    public IEnumerator ChangeSceneToAfter(string scene, int t)
    {
        yield return new WaitForSeconds(t);
        Application.LoadLevel(scene);
    }
}
