using UnityEngine;

using zcode.AssetBundlePacker;

namespace Bean.Hall
{
    public  class ResourcesManager: zcode.AssetBundlePacker.ResourcesManager
    {
        public static void Load<T>(string asset, T result) where T : Object
        {

#if UNITY_EDITOR
            if (LoadPattern.ResourcesLoadPattern == emLoadPattern.EditorAsset || LoadPattern.ResourcesLoadPattern == emLoadPattern.All)
            {
                result = LoadAssetAtPath<T>(asset);
                if (result != null)
                    return;
            }
#endif
            if (LoadPattern.ResourcesLoadPattern == emLoadPattern.AssetBundle || LoadPattern.ResourcesLoadPattern == emLoadPattern.All)
            {
                result = AssetBundleManager.Instance.LoadAsset<T>(asset);
                if (result != null)
                    return;
            }
            if (LoadPattern.ResourcesLoadPattern == emLoadPattern.Original || LoadPattern.ResourcesLoadPattern == emLoadPattern.All)
            {
                result = LoadResources<T>(asset);
                if (result != null)
                    return;
            }

        }
    }

}