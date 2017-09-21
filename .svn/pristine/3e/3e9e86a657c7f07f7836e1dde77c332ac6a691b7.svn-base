using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameMode : MonoBehaviour
{
	public bool Running { get; set; }

	public GameMode() 
	{
		Running = false;
	}

	public virtual bool Enter() 
	{
		return true;
	}

	public virtual void Leave() 
	{
	}

	public virtual void Tick() 
	{
	}

	public virtual void OnConnecting()
	{
	}

	public virtual void OnConnected()
	{
	}

	public virtual void OnDisconnected()
	{
	}

	public virtual void OnError(Int32 err)
	{
	}

	public virtual void OnTimeOut()
	{
	}

	public virtual void OnClosed()
	{
	}

	public virtual void OnRefresh()
	{
	}
}

