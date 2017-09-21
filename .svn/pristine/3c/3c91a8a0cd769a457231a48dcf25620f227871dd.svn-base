using System.Collections;
using UnityEngine;
using PathologicalGames;


public class InGameEffectRes
{
    SpawnPool _commonPool;
    
	string _assetBundleName = "Prefabs_GameEffect";

	public InGameEffectRes()
    {
		_commonPool = PoolManager.Pools.Create("InGameEffectRes");
        _commonPool.dontDestroyOnLoad = true;

        #region ui canvas
//		ResourceManager.Instance.LoadAsync(_assetBundleName, "FX_Canvas", delegate(UnityEngine.Object obj)
//        {
//            PoolUtil.SingletonPrefabPool(_commonPool, obj);
//        });

		GameClient.Instance.AssetLoader.LoadAsync(_assetBundleName, "FX_PengGang", delegate(UnityEngine.Object obj)
		{
			PoolUtil.SingletonPrefabPool(_commonPool, obj);
		});
		
		GameClient.Instance.AssetLoader.LoadAsync(_assetBundleName, "FX_Dingque", delegate(UnityEngine.Object obj)
		{
			PoolUtil.SingletonPrefabPool(_commonPool, obj);
		});
		
		GameClient.Instance.AssetLoader.LoadAsync(_assetBundleName, "FX_Indictor 1", delegate(UnityEngine.Object obj)
		{
			PoolUtil.SingletonPrefabPool(_commonPool, obj);
		});

		GameClient.Instance.AssetLoader.LoadAsync(_assetBundleName, "FX_kong", delegate(UnityEngine.Object obj)
		{
			PoolUtil.SingletonPrefabPool(_commonPool, obj);
		});
		
		GameClient.Instance.AssetLoader.LoadAsync(_assetBundleName, "FX_pong", delegate(UnityEngine.Object obj)
		{
			PoolUtil.SingletonPrefabPool(_commonPool, obj);
		});
		
		GameClient.Instance.AssetLoader.LoadAsync(_assetBundleName, "FX_rain", delegate(UnityEngine.Object obj)
		{
			PoolUtil.SingletonPrefabPool(_commonPool, obj);
		});
		
		GameClient.Instance.AssetLoader.LoadAsync(_assetBundleName, "FX_Tornado", delegate(UnityEngine.Object obj)
		{
			PoolUtil.SingletonPrefabPool(_commonPool, obj);
		});
		
		GameClient.Instance.AssetLoader.LoadAsync(_assetBundleName, "FX_win", delegate(UnityEngine.Object obj)
		{
			PoolUtil.SingletonPrefabPool(_commonPool, obj);
		});
		
		GameClient.Instance.AssetLoader.LoadAsync(_assetBundleName, "FX_zimo", delegate(UnityEngine.Object obj)
		{
			PoolUtil.SingletonPrefabPool(_commonPool, obj);
		});
		
        #endregion
    }
}


