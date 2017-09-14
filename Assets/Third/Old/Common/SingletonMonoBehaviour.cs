using UnityEngine;

/// <summary>
/// This is a MonoBehaviour singleton base class implementation for those need Coroutines.
/// </summary>
public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
	protected SingletonMonoBehaviour() {}

	private static T _instance = null;

	private static object instance_lock_ = new object();

	public static T Instance
	{
		get
		{
			if (_instance == null)
			{	
				lock(instance_lock_)
				{
					if (_instance == null)
					{
						T instance = (T) FindObjectOfType(typeof(T));
						if (instance == null)
						{
							GameObject singleton_object = new GameObject();
							instance = singleton_object.AddComponent<T>();
							singleton_object.name = "(SingletonMonoBehaviour) "+ typeof(T).ToString();

							Debug.Log("[SingletonMonoBehaviour] An instance of " + typeof(T) + 
								" is needed in current scene, so '" + singleton_object +
								"' was created with no DontDestroyOnLoad.");
						} 
						else 
						{
							if (FindObjectsOfType(typeof(T)).Length == 1) 
							{
								Debug.Log("[SingletonMonoBehaviour] Using instance already created: " +
									instance.gameObject.name);
							}
							else
							{
								Debug.LogError("[SingletonMonoBehaviour] Something went really wrong " +
									" - there should never be more than 1 singleton!" +
									" Reopenning the scene might fix it.");
							} 
						} 
						_instance = instance;
					}
				}
			}
			return _instance;
		}
	}
		
	public void OnDestroy () {
		_instance = null;
	}
}
