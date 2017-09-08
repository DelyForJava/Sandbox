using UnityEngine;
	
/// <summary>
/// This is a MonoBehaviour singleton base class implementation for those need Coroutines.
/// </summary>
public class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
	protected SingletonBehaviour() {}

	private static T _instance = null;
 
	private static object instance_lock_ = new object();
 
	public static T Instance
	{
		get
		{
			if (applicationIsQuitting) {
				Debug.LogWarning("[SingletonBehaviour] Instance '"+ typeof(T) +
					"' already destroyed on application quit." +
					" Won't create again - returning null.");
				return null;
			}
 
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
							singleton_object.name = "(SingletonBehaviour) "+ typeof(T).ToString();
							
							///If the Unity is in play mode.	
				            if (Application.isPlaying) 
							{
								DontDestroyOnLoad(singleton_object);
							}
	 
//							Debug.Log("[SingletonBehaviour] An instance of " + typeof(T) + 
//								" is needed in the scene, so '" + singleton_object +
//								"' was created with DontDestroyOnLoad.");
						} 
						else 
						{
							if (FindObjectsOfType(typeof(T)).Length == 1) 
							{
								Debug.Log("[SingletonBehaviour] Using instance already created: " +
									instance.gameObject.name);
							}
							else
							{
								Debug.LogError("[SingletonBehaviour] Something went really wrong " +
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
 
	private static bool applicationIsQuitting = false;
	/// <summary>
	/// When Unity quits, it destroys objects in a random order.
	/// In principle, a SingletonBehaviour is only destroyed when application quits.
	/// If any script calls Instance after it have been destroyed, 
	///   it will create a buggy ghost object that will stay on the Editor scene
	///   even after stopping playing the Application. Really bad!
	/// So, this was made to be sure we're not creating that buggy ghost object.
	/// </summary>
	public void OnDestroy () {
		applicationIsQuitting = true;
	}
}
