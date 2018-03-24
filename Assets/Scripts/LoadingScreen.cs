using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour 
{
    int level;

	IEnumerator Start()
	{
        level = PlayerPrefs.GetInt("Level");
        AsyncOperation async;

        switch(level)
        {
            case 1:
                async = SceneManager.LoadSceneAsync("Level01");
                yield return async;
                break;

            case 2:
                async = SceneManager.LoadSceneAsync("Level02");
                yield return async;
                break;
        }
	}
}
