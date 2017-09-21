using System.Collections;
using UnityEngine;
using PathologicalGames;

public class PoolUtil
{
	public static void SingletonPrefabPool (SpawnPool spawnPool, Object obj)
	{
		GameObject prefab = (GameObject)obj;
		PrefabPool prefabPool = new PrefabPool(prefab.transform);

		prefabPool.preloadAmount = 1;		// default initialize one prefab
		prefabPool.limitInstances = false;	// open limit
		prefabPool.limitFIFO = false;		// close infinite clone prefab
		prefabPool.limitAmount = 1;			// limit max prefab in pool
		prefabPool.cullDespawned = true;	// open auto despawn mode
		prefabPool.cullAbove = 1;			// how many prefabs finally keeped
		prefabPool.cullDelay = 5;			// how long to clean once
		prefabPool.cullMaxPerPass = 5;		// how many to clean every time

		spawnPool._perPrefabPoolOptions.Add(prefabPool);
		spawnPool.CreatePrefabPool(prefabPool);
	}
}
