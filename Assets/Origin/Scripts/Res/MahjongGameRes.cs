using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using PathologicalGames;

public class MahjongGameRes {

	SpawnPool spawnPool;
	PrefabPool prefabPool;
	string _assetBundleName = "Prefabs_InGame";
	//string pathRoot = "Prefabs_InGame";

	// Use this for initialization
	public MahjongGameRes () {

		spawnPool = PoolManager.Pools.Create ("mahjongres");
		spawnPool.dontDestroyOnLoad = true;
		// spawnPool.dontReparent = true;
		// spawnPool.gameObject.transform.parent = Camera.main.transform;

		//--bam crak dot
		string[] BAMS_CRAKS_DOTS = new string[]{ "Bam", "Crak", "Dot" };
		for (int i = 0; i < 3; ++i) {
			for (int j = 1; j <= 9; ++j) {
				string assetName = BAMS_CRAKS_DOTS [i] + "_" + j;
					GameClient.Instance.AssetLoader.LoadAsync (_assetBundleName, BAMS_CRAKS_DOTS [i] + "s/" + assetName, delegate(UnityEngine.Object obj) {
					GameObject prefab = (GameObject)obj;
					//if(!spawnPool._perPrefabPoolOptions.Contains(prefabPool))
					{
						prefabPool = new PrefabPool (prefab.transform);

						prefabPool.preloadAmount = 20;
						prefabPool.preloadTime = false;
						prefabPool.preloadFrames = 0;
						prefabPool.preloadDelay = 0;
						prefabPool.limitInstances = false;
						prefabPool.limitAmount = 20;
						prefabPool.limitFIFO = false;
						prefabPool.cullDespawned = false;
						prefabPool.cullAbove = 10;
						prefabPool.cullDelay = 5;
						prefabPool.cullMaxPerPass = 5;

						spawnPool._perPrefabPoolOptions.Add (prefabPool);
						spawnPool.CreatePrefabPool (prefabPool);
					}
				});

			}
		}

		//--dragons
		string[] DRAGONS = new string[]{ "Blank", "Green", "Red", "White" };
		for (int i = 0; i < 4; ++i) {
			string assetName = "Dragon_" + DRAGONS [i];
			GameClient.Instance.AssetLoader.LoadAsync (_assetBundleName, "Dragons/" + assetName, delegate(UnityEngine.Object obj) {
				GameObject prefab = (GameObject)obj;
				//if(!spawnPool._perPrefabPoolOptions.Contains(prefabPool))
				{
					prefabPool = new PrefabPool (prefab.transform);

					prefabPool.preloadAmount = 4;
					prefabPool.preloadTime = false;
					prefabPool.preloadFrames = 0;
					prefabPool.preloadDelay = 0;
					prefabPool.limitInstances = false;
					prefabPool.limitAmount = 4;
					prefabPool.limitFIFO = false;
					prefabPool.cullDespawned = true;
					prefabPool.cullAbove = 10;
					prefabPool.cullDelay = 5;
					prefabPool.cullMaxPerPass = 5;

					spawnPool._perPrefabPoolOptions.Add (prefabPool);
					spawnPool.CreatePrefabPool (prefabPool);
				}
			});

		}

		//--winds
		string[] WINDS = new string[]{ "East", "North", "South", "West" };
		for (int i = 0; i < 4; ++i) {
			string assetName = "Wind_" + WINDS [i];

			GameClient.Instance.AssetLoader.LoadAsync (_assetBundleName, "Winds/" + assetName, delegate(UnityEngine.Object obj) {
				GameObject prefab = (GameObject)obj;
				//if(!spawnPool._perPrefabPoolOptions.Contains(prefabPool))
				{
					prefabPool = new PrefabPool (prefab.transform);

					prefabPool.preloadAmount = 4;
					prefabPool.preloadTime = false;
					prefabPool.preloadFrames = 0;
					prefabPool.preloadDelay = 0;
					prefabPool.limitInstances = false;
					prefabPool.limitAmount = 4;
					prefabPool.limitFIFO = false;
					prefabPool.cullDespawned = true;
					prefabPool.cullAbove = 10;
					prefabPool.cullDelay = 5;
					prefabPool.cullMaxPerPass = 5;

					spawnPool._perPrefabPoolOptions.Add (prefabPool);
					spawnPool.CreatePrefabPool (prefabPool);
				}
			});
		}

		// dice
		GameClient.Instance.AssetLoader.LoadAsync (_assetBundleName, "DiceGroup", delegate (UnityEngine.Object obj) {
			GameObject prefab = (GameObject)obj;
			//if(!spawnPool._perPrefabPoolOptions.Contains(prefabPool))
			{
				prefabPool = new PrefabPool (prefab.transform);

				//默认初始化两个Prefab
				prefabPool.preloadAmount = 1;
				//开启限制
				prefabPool.limitInstances = false;
				//关闭无限取Prefab
				prefabPool.limitFIFO = false;
				//限制池子里最大的Prefab数量
				prefabPool.limitAmount = 1;
				//开启自动清理池子
				prefabPool.cullDespawned = true;
				//最终保留
				prefabPool.cullAbove = 10;
				//多久清理一次
				prefabPool.cullDelay = 5;
				//每次清理几个
				prefabPool.cullMaxPerPass = 5;

				spawnPool._perPrefabPoolOptions.Add (prefabPool);
				spawnPool.CreatePrefabPool (prefabPool);
			}
		});
	}
}
