using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExtraAction : FiniteTimeAction
{
	ExtraAction()
	{
	}
	
	static public ExtraAction Create()
	{
		ExtraAction obj = new ExtraAction ();
		return obj;
	}
}

public class ActionInterval : FiniteTimeAction
{
	public float mElapsed;
	//public float mDuration;
	public bool mFirstTick;
	
	/*
	//extension in GridAction
	void setAmplitudeRate(float amp);
	float getAmplitudeRate(void);
	*/
	public ActionInterval() 
	{
	}
	
	public bool InitWithDuration(float d) {
		Duration = d;
		if (Duration == 0.0f)
			Duration = Mathf.Epsilon;
		mElapsed = 0.0f;
		mFirstTick = true;
		return true;
	}
	
	override public bool IsDone() {
		return mElapsed >= Duration;
	}
	
	//deprecated
	override public void Stop() {
		Target = null;
		OriginalTarget = null;
	}
	
	override public void Step(float dt) 
	{
		if (mFirstTick)
		{
			mFirstTick = false;
			mElapsed = 0;
		}
		else
		{
			mElapsed += dt;
		}
		
		this.Update(Mathf.Max (0.0f, Mathf.Min(1.0f, mElapsed / Mathf.Max(Duration, Mathf.Epsilon) ) ) );
	}
	
	override public void StartWithTarget(GameObject target) {
		base.StartWithTarget (target);
		mElapsed = 0.0f;
		mFirstTick = true;
	}
	
	public ActionInterval Reverse() 
	{
		return null;
	}
	
	override public void Update(float t) {
		Debug.Log ("UAction Update");
	}
	
	static public ActionInterval Create() {
		ActionInterval obj = new ActionInterval ();
		obj.InitWithDuration (0.0f);
		return obj;
	}
};

public class SequenceAction : ActionInterval
{
	float mSplit;
	int mLast;
	FiniteTimeAction[] mActions;
	
	SequenceAction()
	{
		mActions = new FiniteTimeAction[]{null, null};
	}
	
	static public SequenceAction CreateWithTwoActions(FiniteTimeAction action1, FiniteTimeAction action2)
	{
		SequenceAction obj = new SequenceAction ();
		obj.InitWithDuration (0.0f);
		
		UnityEngine.Debug.Assert (action1 != null);
		UnityEngine.Debug.Assert (action2 != null);
		
		obj.Duration = action1.Duration + action2.Duration;
		
		obj.mActions[0] = action1;
		obj.mActions[1] = action2;
		
		return obj;
	}
	
	static public SequenceAction Create(List<FiniteTimeAction> actionList)
	{
		SequenceAction ret = null;
		
		do 
		{
			int count = actionList.Count;
			if(count == 0)
				break;
			
			FiniteTimeAction prev = actionList[0];
			
			if (count > 1)
			{
				for (int i = 1; i < count; ++i)
				{
					prev = CreateWithTwoActions(prev, actionList[i]);
				}
			}
			else
			{
				// If only one action is added to Sequence, make up a Sequence by adding a simplest finite time action.
				prev = CreateWithTwoActions(prev, ExtraAction.Create());
			}
			ret = (SequenceAction)prev;
			
		}while (false);
		
		return ret;
	}
	
	override public void StartWithTarget (GameObject target)
	{
		base.StartWithTarget (target);
		mSplit = mActions[0].Duration / Duration;
		mLast = -1;
	}
	
	override public void Stop()
	{
		// Issue #1305
		if( mLast != - 1)
		{
			mActions[mLast].Stop();
		}
		base.Stop ();
	}
	
	override public void Update (float t)
	{
		int found = 0;
		float new_t = 0.0f;
		
		if( t < mSplit ) {
			// action[0]
			found = 0;
			if( mSplit != 0 )
				new_t = t / mSplit;
			else
				new_t = 1;
			
		} else {
			// action[1]
			found = 1;
			if ( mSplit == 1 )
				new_t = 1;
			else
				new_t = (t-mSplit) / (1 - mSplit );
		}
		
		if ( found==1 ) {
			
			if( mLast == -1 ) {
				// action[0] was skipped, execute it.
				mActions[0].StartWithTarget(Target);
				mActions[0].Update(1.0f);
				mActions[0].Stop();
			}
			else if( mLast == 0 )
			{
				// switching to action 1. stop action 0.
				mActions[0].Update(1.0f);
				mActions[0].Stop();
			}
		}
		else if(found==0 && mLast==1 )
		{
			// Reverse mode ?
			// XXX: Bug. this case doesn't contemplate when _last==-1, found=0 and in "reverse mode"
			// since it will require a hack to know if an action is on reverse mode or not.
			// "step" should be overriden, and the "reverseMode" value propagated to inner Sequences.
			mActions[1].Update(0);
			mActions[1].Stop();
		}
		// Last action found and it is done.
		if( found == mLast && mActions[found].IsDone() )
		{
			return;
		}
		
		// Last action found and it is done
		if( found != mLast )
		{
			mActions[found].StartWithTarget(Target);
		}
		
		mActions[found].Update(new_t);
		mLast = found;
	}
}

public class Repeat : ActionInterval
{
};

public class RepeatForever : ActionInterval
{
};

public class Spawn : ActionInterval
{
	public FiniteTimeAction One { get; set; }
	public FiniteTimeAction Two { get; set; }
	
	Spawn() {}
	
	bool initWithTwoActions(FiniteTimeAction action1, FiniteTimeAction action2)
	{
		UnityEngine.Debug.Assert(action1 != null, "");
		UnityEngine.Debug.Assert(action2 != null, "");
		
		bool ret = false;
		
		float d1 = action1.Duration;
		float d2 = action2.Duration;
		
		if (base.InitWithDuration(Mathf.Max(d1, d2)))
		{
			One = action1;
			Two = action2;
			
			if (d1 > d2)
			{
				Two = SequenceAction.CreateWithTwoActions(action2, DelayTime.Create(d1 - d2));
			} 
			else if (d1 < d2)
			{
				One = SequenceAction.CreateWithTwoActions(action1, DelayTime.Create(d2 - d1));
			}
			
			ret = true;
		}
		
		return ret;
	}
	
	public static Spawn Create(List<FiniteTimeAction> arrayOfActions)
	{
		Spawn ret = null;
		do 
		{
			int count = arrayOfActions.Count;
			UnityEngine.Debug.Assert(count>0);
			FiniteTimeAction prev = arrayOfActions[0];
			if (count > 1)
			{
				for (int i = 1; i < arrayOfActions.Count; ++i)
				{
					prev = createWithTwoActions(prev, arrayOfActions[i]);
				}
			}
			else
			{
				// If only one action is added to Spawn, make up a Spawn by adding a simplest finite time action.
				prev = createWithTwoActions(prev, ExtraAction.Create());
			}
			ret = (Spawn)prev;
		}while (false);
		
		return ret;
	}
	
	static Spawn createWithTwoActions(FiniteTimeAction action1, FiniteTimeAction action2)
	{
		Spawn spawn = new Spawn();
		spawn.initWithTwoActions(action1, action2);
		return spawn;
	}
	
	public Spawn Reverse()
	{
		return null;
	}
	
	override public void StartWithTarget(GameObject target) 
	{
	}
	
	override public void Stop() 
	{
	}
	
	override public void Update(float time) 
	{
		
	}
};

public class RotateTo : ActionInterval
{
		Vector3 _dstAngle;
		Vector3 _startAngle;
		Vector3 _diffAngle;

		public static RotateTo Create(float duration, Vector3 dstAngle)
		{
			RotateTo rotateto = new RotateTo();
			if(rotateto.InitWithDuration(duration, dstAngle))
				return rotateto;
			return null;
		}

		public bool InitWithDuration(float duration, Vector3 dstAngle)
		{
			if (base.InitWithDuration (duration)) {
				_dstAngle = dstAngle;
				return true;
			}
			return false;
		}
			
		void calculateAngles()
		{
		}

		override public void StartWithTarget(GameObject target)
		{
			base.StartWithTarget (target);
			_startAngle = target.transform.localEulerAngles;
			_diffAngle = _dstAngle - _startAngle;
			calculateAngles ();			
		}

		override public void Update(float time)
		{
			if (Target != null)
			{
				Debug.Log (_diffAngle);
				Vector3 euler = _startAngle + _diffAngle * time;
				Target.transform.localEulerAngles = euler;
			}
		}
};

public class RotateBy : ActionInterval
{
		Vector3 _axis;
		float _deltaAngle;
		Space _space;

		Quaternion _orignRotation;

		public static RotateBy Create(float duration, Vector3 axis, float deltaAngle, Space space)
		{
			RotateBy rotateby = new RotateBy();
			if(rotateby.InitWithDuration(duration, axis, deltaAngle, space))
				return rotateby;
			return null;
		}

		public bool InitWithDuration(float duration, Vector3 axis, float deltaAngle, Space space)
		{
			if (base.InitWithDuration (duration)) {
				_axis = axis;
				_deltaAngle = deltaAngle;
				_space = space;
				return true;
			}
			return false;
		}

		override public void StartWithTarget(GameObject target)
		{
			base.StartWithTarget (target);		
			_orignRotation = target.transform.rotation;
		}

		override public void Update(float time)
		{
			if (Target != null)
			{
				Target.transform.rotation = _orignRotation;
				Target.transform.Rotate (_axis, _deltaAngle * time, _space);
			}
		}
};


public class MoveBy : ActionInterval
{
	public Vector3 PositionDelta;
	public Vector3 StartPosition;
	public Vector3 PreviousPosition;
	protected bool _isLocal;

	static public MoveBy Create(float duration, Vector3 deltaPosition, bool isLocal = false)
	{
		MoveBy ret = new MoveBy ();
		ret.InitWithDuration (duration, deltaPosition, isLocal);
		return ret;
	}

	public MoveBy Reverse() 
	{
		return MoveBy.Create (Duration, new Vector3 (-PositionDelta.x, -PositionDelta.y, -PositionDelta.z));
	}

	override public void StartWithTarget(GameObject target) 
	{
		base.StartWithTarget (target);
		PreviousPosition = StartPosition = (_isLocal == true ? target.transform.localPosition : target.transform.position);
	}

	override public void Update(float time)
	{
		if (Target)
		{
			#if CC_ENABLE_STACKABLE_ACTIONS
			Point currentPos = _target->getPosition();
			Point diff = currentPos - _previousPosition;
			_startPosition = _startPosition + diff;
			Point newPos =  _startPosition + (_positionDelta * t);
			_target->setPosition(newPos);
			_previousPosition = newPos;
			#else
			//_target->setPosition(_startPosition + _positionDelta * t);
			Vector3 newPos = StartPosition + PositionDelta * time;
			if(_isLocal)
			{
				Target.transform.localPosition = newPos;
			}
			else
			{
				Target.transform.position = newPos;
			}
			#endif // CC_ENABLE_STACKABLE_ACTIONS
		}
	}

	public MoveBy() {}

	public bool InitWithDuration(float duration, Vector3 deltaPosition, bool isLocal)
	{
		if (base.InitWithDuration (duration)) {
			PositionDelta = deltaPosition;
			_isLocal = isLocal;
			return true;
		}
		return false;
	}
};

public class MoveTo : MoveBy
{
	public Vector3 EndPos;
	float mCurve;
	
	MoveTo()
	{
	}
	
	static public MoveTo Create(float duration, Vector3 pos, bool isLocal = false, float curve = 0.0f)
	{
		MoveTo ret = new MoveTo ();
		ret.InitWithDuration (duration, pos, isLocal);
		ret.mCurve = curve;
		return ret;
	}

	/*
	public MoveTo Reverse()
	{
		MoveTo act = MoveTo.Create(Duration, new Vector3(-mDeltaPos.x, -mDeltaPos.y, mDeltaPos.z));
		return act;
	}*/
	
	override public void StartWithTarget(GameObject target) 
	{
		base.StartWithTarget (target);
		PositionDelta = EndPos - (_isLocal == true ? target.transform.localPosition : target.transform.position);
	}

	override public void Update(float t)
	{
		if (Target != null) {
			base.Update(t);
			Vector3 newPos = (_isLocal == true ? Target.transform.localPosition : Target.transform.position);
			if(mCurve>0.0f) {
				newPos.y = newPos.y + mCurve * Mathf.Sin(t * 3.1415926f);
				if(_isLocal)
				{
					Target.transform.localPosition = newPos;
				}
				else
				{
					Target.transform.position = newPos;
				}
			}
		}
	}

	public bool InitWithDuration(float duration, Vector3 pos, bool isLocal)
	{
		if (base.InitWithDuration (duration)) {
			EndPos = pos;
			_isLocal = isLocal;
			return true;
		}
		return false;
	}
}

public class SkewTo : ActionInterval
{
};

public class SkewBy : SkewTo
{
};

public class JumpBy : ActionInterval
{
	public Vector3 StartPosition { get; set; }
	public Vector3 Delta { get; set; }
	public float Height { get; set; }
	public int Jumps { get; set; }
	public Vector3 PreviousPos { get; set; }

	static public JumpBy Create(float duration, Vector3 position, float height, int jumps)
	{
		JumpBy jumpBy = new JumpBy();
		jumpBy.InitWithDuration(duration, position, height, jumps);
		return jumpBy;
	}

	public JumpBy Reverse() 
	{
		return JumpBy.Create(Duration, new Vector3(-Delta.x, -Delta.y, -Delta.z), Height, Jumps);
	}

	override public void StartWithTarget(GameObject target) 
	{
		base.StartWithTarget(target);
		PreviousPos = StartPosition = target.transform.position;
	}

	override public void Update(float t) 
	{
		// parabolic jump (since v0.8.2)
		if (Target) {
			//float frac = fmodf (t * Jumps, 1.0f);
			float frac = t * Jumps % 1.0f;
			float y = Height * 4 * frac * (1 - frac);
			y += Delta.y * t;
			
			float x = Delta.x * t;
			float z = Delta.z * t;
#if CC_ENABLE_STACKABLE_ACTIONS
			Point currentPos = _target->getPosition();
			
			Point diff = currentPos - _previousPos;
			_startPosition = diff + _startPosition;
			
			Point newPos = _startPosition + Point(x,y);
			_target->setPosition(newPos);
			
			_previousPos = newPos;
#else
			//Target->setPosition(_startPosition + Point(x,y));
			Target.transform.position = StartPosition + new Vector3 (x, y, z);
#endif // !CC_ENABLE_STACKABLE_ACTIONS
		}
	}

	public JumpBy() {}
	
	public bool InitWithDuration(float duration, Vector3 position, float height, int jumps)
	{
		UnityEngine.Debug.Assert(jumps>=0, "Number of jumps must be >= 0");
		
		if (base.InitWithDuration(duration) && jumps>=0)
		{
			Delta = position;
			Height = height;
			Jumps = jumps;			
			return true;
		}
		
		return false;

	}
};

public class JumpTo : JumpBy
{
	static public JumpTo Create(float duration, Vector3 position, float height, int jumps)
	{
		JumpTo jumpTo = new JumpTo ();
		jumpTo.InitWithDuration (duration, position, height, jumps);
		return jumpTo;
	}

	override public void StartWithTarget(GameObject target)
	{
		base.StartWithTarget (target);
		Delta = new Vector3(Delta.x - StartPosition.x, Delta.y - StartPosition.y, Delta.z - StartPosition.z);
	}

	public JumpTo Reverse() 
	{
		return null;
	}

	JumpTo() {}
};

/** Bezier configuration structure
 */
/*
typedef struct _ccBezierConfig {
	//! end position of the bezier
	Point endPosition;
	//! Bezier control point 1
	Point controlPoint_1;
	//! Bezier control point 2
	Point controlPoint_2;
} ccBezierConfig;

public class BezierBy : public ActionInterval
{
};

public class BezierTo : public BezierBy
{
};
*/
public class ScaleTo : ActionInterval
{
};

public class ScaleBy : ScaleTo
{
};

public class Blink : ActionInterval
{
};

public class FadeTo : ActionInterval
{
};

public class FadeIn : FadeTo
{
};

public class FadeOut : FadeTo
{
};

public class TintTo : ActionInterval
{
};

public class TintBy : ActionInterval
{
};

public class DelayTime : ActionInterval
{
	DelayTime()
	{
	}
	
	static public DelayTime Create(float d)
	{
		DelayTime obj = new DelayTime ();
		obj.InitWithDuration (d);
		return obj;
	}
	
	public DelayTime Reverse()
	{
		return DelayTime.Create (Duration);
	}
	
	override public void Update(float t)
	{
		return;
	}
}

public class ReverseTime : ActionInterval
{
};

/*deprecated
public class Animate : public ActionInterval
{
};
*/

public class TargetedAction : ActionInterval
{
};
