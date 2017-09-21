using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionManager : Singleton<ActionManager> {

	List<ActionBase> mActionList;

	public ActionManager()
	{
		mActionList = new List<ActionBase> (10);
	}

	public void Update( float dt)
	{
		if (mActionList.Count > 0) {
			for (int i=0; i<mActionList.Count; ++i) {
				ActionBase act = mActionList [i];
				if (act.IsDone () || act.Target==null) {
					act = null;
					mActionList.RemoveAt (i);
					--i;
					continue;
				}
				act.Step (dt);
			}
		}
	}

	void Add(ActionBase act, GameObject target = null, bool paused = false)
	{
		mActionList.Add(act);
	}

	void PauseTarget(GameObject target)
	{

	}

	void ResumeTarget(GameObject target)
	{
	}

	public void RunAction(GameObject target, ActionBase action)
	{
		action.StartWithTarget (target);
		ActionManager.Instance.Add (action);
	}

	public void RunAction(GameObject target, List<FiniteTimeAction> list)
	{
		SequenceAction seq = SequenceAction.Create (list);
		RunAction (target, seq);
	}

	public void StopAll()
	{
		mActionList.Clear ();
	}
}

