using UnityEngine;
using System.Collections;

public class LoadingScreen : MonoBehaviour 
{
    int level;

	IEnumerator Start()
	{
        level = PlayerPrefs.GetInt("Level");

        if (level == 1)
        {
            AsyncOperation async = Application.LoadLevelAsync("Level01");
            yield return async;
        }

        else if (level == 2)
        {
            AsyncOperation async = Application.LoadLevelAsync("Level02");
            yield return async;
        }
	}
}
