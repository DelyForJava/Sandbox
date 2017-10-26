using UnityEngine;

/// <summary>
/// This is a MonoBehaviour singleton base class implementation for those need Coroutines.
/// </summary>
public class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    protected SingletonBehaviour() { }

    private static T _instance = null;

    private static object instance_lock_ = new object();

    public static T Instance
    {
        get
        {

            //instance_ = GameObject.FindObjectOfType(typeof(T)) as T;
            if (_instance == null)
                _instance = new GameObject("SingletonOf" + typeof(T).ToString(), typeof(T)).GetComponent<T>();

            DontDestroyOnLoad(_instance);

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
    public void OnDestroy()
    {
        //applicationIsQuitting = true;
    }
}
