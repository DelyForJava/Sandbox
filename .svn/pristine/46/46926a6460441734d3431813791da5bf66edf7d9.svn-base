using UnityEngine;

/// <summary>
/// Singleton base class for T is not subclass of MonoBehaviour.
/// </summary>
public class Singleton<T> where T : new()
{
	protected Singleton() {}

	private static T _instance;
 
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
						T instance = new T();

						Debug.Log("[Singleton] An instance of " + typeof(T) + " is created.");

						_instance = instance;
					}
				}
			}
			return _instance;
		}
	}
}
