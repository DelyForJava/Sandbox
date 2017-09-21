using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameLoading : MonoBehaviour 
{
	static public int level = 1;
	static public int lastLevel = 0;

	static public bool FinishLoading = false;

	public delegate void StartLoadingHandler(GameLoading gl);
	static public event StartLoadingHandler OnStartLoading;
	public delegate void LoadingHandler();
	static public event LoadingHandler OnLoading;
	public delegate void FinishLoadingHandler(GameLoading gl);
	static public event FinishLoadingHandler OnFinishLoading;

	AsyncOperation async;

	IEnumerator Start()
	{
        Debug.Log("!!!!!!!!!!!!!!!!!!GameLoading Start ");
		yield return new WaitForEndOfFrame();

		int currentLoadingLevel = 0;

		if (GameLoading.level > 0) {

			//Debug.LogError( "Start" );

			currentLoadingLevel = GameLoading.level;

			FinishLoading = false;

			if (OnStartLoading != null)
				OnStartLoading (this);

			yield return loading();

			//maybe last coroutine still run
			if(currentLoadingLevel != GameLoading.level)
			{
				FinishLoading = false;
				Debug.LogError("#" + currentLoadingLevel + ":" + GameLoading.level);
			}
			else
			{
				Debug.Log("!!!!!!!!!!!!!!!!!!switch scene " + GameLoading.level);

				async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(GameLoading.level);

				yield return levelLoading (async);
			}
		}
		else {
			Debug.LogError("error -> no loading level && loading name!");
		}
	}

	IEnumerator loading()
	{
		while (GameLoading.FinishLoading == false) {
			if (OnLoading != null)
				OnLoading ();
			yield return new WaitForEndOfFrame ();
		}
	}

	IEnumerator levelLoading(AsyncOperation op)
	{
		Debug.Log("Start Auto Level Loading -> " + GameLoading.level);

		int toProgress = 0;

		int progress = 0;
		int endProgress = 100;
		
		while(op.progress < 0.9f) {
			toProgress = (int)op.progress * 100;
			while(progress < toProgress) {
				++endProgress;
				if(OnLoading != null)
					OnLoading();
				yield return new WaitForEndOfFrame();
			}
			// fix dead lock
			yield return new WaitForEndOfFrame ();
		}

		while (progress < 100) {
			++progress;
			if (OnLoading != null)
				OnLoading ();
			yield return new WaitForEndOfFrame ();
		}

		if(OnFinishLoading != null)
		{
			OnFinishLoading(this);
		}
	}

	static public void SwitchScene(int level, System.Action callback = null)
	{
        //UIDebugViewController.Instance.Close();
		GameLoading.lastLevel = GameLoading.level;	
		UnityEngine.SceneManagement.SceneManager.LoadScene (0);
		GameLoading.level = level;
	}
}
