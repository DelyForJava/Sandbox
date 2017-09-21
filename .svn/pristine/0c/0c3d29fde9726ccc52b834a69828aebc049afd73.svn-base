using System.Collections;
using UnityEngine;
using PathologicalGames;


public class Common2dRes
{
    SpawnPool _commonPool;
    
	string _assetBundleName = "Debug";

    public Common2dRes()
    {
        _commonPool = PoolManager.Pools.Create("Common2dRes");
        _commonPool.dontDestroyOnLoad = true;

        #region ui canvas
		GameClient.Instance.AssetLoader.LoadAsync(_assetBundleName, "Canvas_Debug", delegate(UnityEngine.Object obj)
        {
            PoolUtil.SingletonPrefabPool(_commonPool, obj);
        });

		GameClient.Instance.AssetLoader.LoadAsync(_assetBundleName, "Canvas_ImgSprite", delegate(UnityEngine.Object obj)
		{
			PoolUtil.SingletonPrefabPool(_commonPool, obj);
		});
		
		GameClient.Instance.AssetLoader.LoadAsync(_assetBundleName, "GroupCard", delegate(UnityEngine.Object obj)
		{
			PoolUtil.SingletonPrefabPool(_commonPool, obj);
		});
		
		GameClient.Instance.AssetLoader.LoadAsync(_assetBundleName, "Canvas_Tips", delegate(UnityEngine.Object obj)
			{
				PoolUtil.SingletonPrefabPool(_commonPool, obj);
			});
		
        #endregion
    }
}


