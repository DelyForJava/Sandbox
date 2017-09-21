using System.Collections;
using UnityEngine;

public interface UIController
{
	/// <summary>
	/// 重置controller中 除了对prefab资源引用之外的 成员变量
	/// 引用prefab上的资源的成员变量，再unload中进行清楚
	/// </summary>
	void Reset ();
	void OnRefresh();

	/// <summary>
	/// 加载UI的prefab
	/// </summary>
	bool Load ();

	/// <summary>
	/// 卸载UI的prefab，只清除对prefab资源的引用
	/// </summary>
	void Unload ();

	//deprecatd later
	bool OpenViewRoot ();
	void CloseViewRoot ();

	bool Open();
	void Close();
}

