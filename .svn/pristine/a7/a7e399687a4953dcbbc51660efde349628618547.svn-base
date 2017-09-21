using System.Collections;
using UnityEngine;
using PathologicalGames;


    public class InGame2dRes
    {
        SpawnPool _spawnPool;
        PrefabPool _prefabPool;
        string _assetBundleName = "Prefabs_InGame";

        public InGame2dRes()
        {
            _spawnPool = PoolManager.Pools.Create("InGame2dRes");
            _spawnPool.dontDestroyOnLoad = true;

            #region ui canvas
			GameClient.Instance.AssetLoader.LoadAsync(_assetBundleName, "Canvas_Combo", delegate (UnityEngine.Object obj)
            {
                PoolUtil.SingletonPrefabPool(_spawnPool, obj);
            });

			GameClient.Instance.AssetLoader.LoadAsync(_assetBundleName, "Canvas_LackSetting", delegate (UnityEngine.Object obj)
			{
				PoolUtil.SingletonPrefabPool(_spawnPool, obj);
			});

			GameClient.Instance.AssetLoader.LoadAsync(_assetBundleName, "Canvas_GameUIInfo", delegate(UnityEngine.Object obj)
            {
                PoolUtil.SingletonPrefabPool(_spawnPool, obj);
            });

			GameClient.Instance.AssetLoader.LoadAsync(_assetBundleName, "Canvas_HuPrompt", delegate(UnityEngine.Object obj)
            {
                PoolUtil.SingletonPrefabPool(_spawnPool, obj);
            });

			GameClient.Instance.AssetLoader.LoadAsync(_assetBundleName, "Canvas_GameResult", delegate(UnityEngine.Object obj)
			{
				PoolUtil.SingletonPrefabPool(_spawnPool, obj);
			});
            #endregion
        }
    }


