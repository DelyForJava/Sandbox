using System;
using System.Collections;  
using System.Collections.Generic;   
using System.Threading;  
using System.Linq;  
using UnityEngine;

public class Loom : MonoBehaviour  
{  
    public static int maxThreads = 8;  
    static int numThreads;  
      
    private static Loom _current;  
    private int _count;  
    public static Loom Current  
    {  
        get  
        {  
            Initialize();  
            return _current;  
        }  
    }  
      
    void Awake()  
    {  
        _current = this;  
        initialized = true;  
    }  
      
    static bool initialized;  
      
    static void Initialize()  
    {  
        if (!initialized)  
        {
            if(!Application.isPlaying)  
                return;  
            initialized = true;  
            var g = new GameObject("Loom");  
            _current = g.AddComponent<Loom>();  
        }
    }  
      
	private List<Action> _actions = new List<Action>();      
	List<Action> _currentActions = new List<Action>();  

    public struct DelayedQueueItem  
    {  
        public float time;  
        public Action action;  
    }  
    private List<DelayedQueueItem> _delayed = new  List<DelayedQueueItem>();  
    List<DelayedQueueItem> _currentDelayed = new List<DelayedQueueItem>();  

	public struct ConditionQueueItem
	{
		public Func<bool> condition;
		public Action action;
	}
	private List<ConditionQueueItem> _cond = new List<ConditionQueueItem> ();
	List<ConditionQueueItem> _currentCond = new List<ConditionQueueItem> ();

    public static void QueueOnMainThread(Action action)  
    {  
        QueueOnMainThread( action, 0f);  
    }  

    public static void QueueOnMainThread(Action action, float time)  
    {  
        if(time != 0)  
        {  
            lock(Current._delayed)  
            {  
                Current._delayed.Add(new DelayedQueueItem { time = Time.time + time, action = action});  
            }  
        }  
        else  
        {  
            lock (Current._actions)  
            {  
                Current._actions.Add(action);  
            }  
        }  
    }

	public static void QueueOnMainThread(Action action, Func<bool> condition)
	{
		if (condition != null) {
			Current._cond.Add (new ConditionQueueItem{ condition = condition, action = action });
		} 
		else {
			lock (Current._actions)  
			{  
				Current._actions.Add(action);  
			}  
		}
	}
      
    public static Thread RunAsync(Action a)  
    {  
        Initialize();  
        while(numThreads >= maxThreads)  
        {  
            Thread.Sleep(1);  
        }  
        Interlocked.Increment(ref numThreads);  
        ThreadPool.QueueUserWorkItem(RunAction, a);  
        return null;  
    }  
      
    private static void RunAction(object action)  
    {  
        try  
        {  
            ((Action)action)();  
        }  
        catch  
        {  
        }  
        finally  
        {  
            Interlocked.Decrement(ref numThreads);  
        } 
    }  
    
    void OnDisable()  
    {  
        if (_current == this)  
        {  
              
            _current = null;  
        }  
    }  

    // Use this for initialization  
    void Start()  
    {
    }
      
    // Update is called once per frame  
    void Update()  
    {
		lock (_actions) {
			_currentActions.Clear ();  
			_currentActions.AddRange (_actions);  
			_actions.Clear ();  
		}
		for (int i = 0; i < _currentActions.Count; ++i) {
			_currentActions [i] ();
		} 

		lock (_delayed) {  
			_currentDelayed.Clear ();  
			_currentDelayed.AddRange (_delayed.Where (d => d.time <= Time.time));  
			foreach (var item in _currentDelayed)
				_delayed.Remove (item);  
		}
		for (int i = 0; i < _currentDelayed.Count; ++i) {
			_currentDelayed [i].action ();
		}

		lock (_cond) {
			_currentCond.Clear ();
			_currentCond.AddRange (_cond.Where (x => x.condition.Invoke () == true));
			foreach (var item in _currentCond)
				_cond.Remove (item);
		}
		for (int i = 0; i < _currentCond.Count; ++i) {
			_currentCond [i].action ();
		}
		/*
		lock (_cond) {
			_currentCond.Clear ();
			for (int i = _cond.Count - 1; i >= 0; --i) {
				if (_cond [i].condition.Invoke ()) {
					_currentCond.Add (_cond [i]);
					_cond.RemoveAt (i);
				}
			}
			for (int i = 0; i < _currentCond.Count; ++i) {
				_currentCond [i].action ();
			}
		}*/
    }  

	//Scale a mesh on a second thread  
	void ScaleMesh(Mesh mesh, float scale)  
	{  
	    //Get the vertices of a mesh  
	    var vertices = mesh.vertices;  
	    //Run the action on a new thread  
	    Loom.RunAsync(()=>{  
	        //Loop through the vertices  
	        for(var i = 0; i < vertices.Length; i++)  
	        {  
	            //Scale the vertex  
	            vertices[i] = vertices[i] * scale;  
	        }  
	        //Run some code on the main thread  
	        //to update the mesh  
	        Loom.QueueOnMainThread(()=>{  
	            //Set the vertices  
	            mesh.vertices = vertices;  
	            //Recalculate the bounds  
	            mesh.RecalculateBounds();  
	        });  
	   
	    });  
	}
}  




