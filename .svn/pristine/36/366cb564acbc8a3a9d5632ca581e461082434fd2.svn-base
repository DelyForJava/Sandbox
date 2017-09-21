using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;

public delegate void dlg_OnAssetBundleDownLoadOver();
/// <summary>  
/// 加载AssetBundle  
/// </summary>  
public class LoadAssetBundle : SingletonBehaviour<LoadAssetBundle>
{
    //public override void Init()
    //{

    //}

    //不同平台下StreamingAssets的路径设置  
    public static string PathURL;
    void Start()
    {
        PathURL =
#if UNITY_ANDROID
        "jar:file://" + Application.dataPath + "!/assets/";  
#elif UNITY_IPHONE
        Application.dataPath + "/Raw/";    
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR
        "file://" + Application.dataPath + "/StreamingAssets/";
#else
        string.Empty;
#endif
    }
    //5.0版本打包时候选中需要打包的东西然后设置右下角名称,同个/设置多集目录,后面的框标记后缀(后缀不重要)  
    //打包时候的目标文件夹,假设目标文件夹名称为"WJJ",那么会生成"WJJ"和"WJJ.manifest"两个文件  
    //其中WJJ.manifest文件没有用,只是用来看的,WJJ是一个assetbundle包,里面包含了整个文件夹的依赖信息  
    //可以先加载这个东西,然后获取到依赖关系后逐步加载  
    //递一般归加载并保存到Application.persistentDataPath  
    //注意用GetDirectDependencies递归,不要用GetAllDependencies,因为已经包含孙子儿子又会加载孙子,重复加载了  
    //简单用法直接获取不要用GetAllDependencies,然后倒序加载      

    /// <summary>  
    /// 下载资源到本地包括它的依赖项  
    /// </summary>  
    /// <param name="AssetsHost">根目录地址</param>  
    /// <param name="RootAssetsName"></param>  
    /// <param name="AssetName"></param>  
    /// <param name="savePath"></param>  
    public void DownLoadAssets2LocalWithDependencies(string AssetsHost, string RootAssetsName, string AssetName, string savePath, dlg_OnAssetBundleDownLoadOver OnDownloadOver = null)
    {
        StartCoroutine(DownLoadAssetsWithDependencies2Local(AssetsHost, RootAssetsName, AssetName, savePath, OnDownloadOver));
       
    }

    /// <summary>  
    ///   //从服务器下载到本地  
    /// </summary>  
    /// <param name="AssetsHost">服务器路径</param>  
    /// <param name="RootAssetsName">总依赖文件目录路径</param>  
    /// <param name="AssetName">请求资源名称</param>  
    /// <param name="saveLocalPath">保存到本地路径,一般存在Application.persistentDataPath</param>  
    /// <returns></returns>  
    IEnumerator DownLoadAssetsWithDependencies2Local(string AssetsHost, string RootAssetsName, string AssetName, string saveLocalPath, dlg_OnAssetBundleDownLoadOver OnDownloadOver = null)
    {
        WWW ServerManifestWWW = null;        //用于存储依赖关系的 AssetBundle  
        AssetBundle LocalManifestAssetBundle = null;    //用于存储依赖关系的 AssetBundle  
        AssetBundleManifest assetBundleManifestServer = null;  //服务器 总的依赖关系      
        AssetBundleManifest assetBundleManifestLocal = null;   //本地 总的依赖关系  

        if (RootAssetsName != "")    //总依赖项为空的时候去加载总依赖项  
        {
            ServerManifestWWW = new WWW(AssetsHost + "/" + RootAssetsName);

            Debug.Log("___当前请求总依赖文件~\n");

            yield return ServerManifestWWW;
            if (ServerManifestWWW.isDone)
            {
                //加载总的配置文件  
                assetBundleManifestServer = ServerManifestWWW.assetBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
                Debug.Log("___当前请求总依赖文件~\n");
            }
            else
            {
                throw new Exception("总依赖文件下载失败~~~\n");
            }
        }

        //获取需要加载物体的所有依赖项  
        string[] AllDependencies = new string[0];
        if (assetBundleManifestServer != null)
        {
            //根据名称获取依赖项  
            AllDependencies = assetBundleManifestServer.GetAllDependencies(AssetName);
        }

        //下载队列 并获取每个资源的Hash值  
        Dictionary<string, Hash128> dicDownloadInfos = new Dictionary<string, Hash128>();
        for (int i = AllDependencies.Length - 1; i >= 0; i--)
        {
            dicDownloadInfos.Add(AllDependencies[i], assetBundleManifestServer.GetAssetBundleHash(AllDependencies[i]));
        }
        dicDownloadInfos.Add(AssetName, assetBundleManifestServer.GetAssetBundleHash(AssetName));
        if (assetBundleManifestServer != null)   //依赖文件不为空的话下载依赖文件  
        {
            Debug.Log("Hash:" + assetBundleManifestServer.GetHashCode());
            dicDownloadInfos.Add(RootAssetsName, new Hash128(0, 0, 0, 0));
        }

        //卸载掉,无法同时加载多个配置文件  
        ServerManifestWWW.assetBundle.Unload(true);

        if (File.Exists(saveLocalPath + "/" + RootAssetsName))
        {
            LocalManifestAssetBundle = AssetBundle.LoadFromFile(saveLocalPath + "/" + RootAssetsName);
            assetBundleManifestLocal = LocalManifestAssetBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        }

        foreach (var item in dicDownloadInfos)
        {
            if (!CheckLocalFileNeedUpdate(item.Key, item.Value, RootAssetsName, saveLocalPath, assetBundleManifestLocal))
            {
                Debug.Log("无需下载:" + item.Key);
                continue;
            }
            else
            {
                DeleteFile(saveLocalPath + "/" + item.Key);
            }

            //直接加载所有的依赖项就好了  
            WWW wwwAsset = new WWW(AssetsHost + "/" + item.Key);
            //获取加载进度  
            while (!wwwAsset.isDone)
            {
                Debug.Log(string.Format("下载 {0} : {1:N1}%", item.Key, (wwwAsset.progress * 100)));
                yield return new WaitForSeconds(0.2f);
            }
            //保存到本地  
            SaveAsset2LocalFile(saveLocalPath, item.Key, wwwAsset.bytes, wwwAsset.bytes.Length);

        }

        if (LocalManifestAssetBundle != null)
        {
            LocalManifestAssetBundle.Unload(true);
        }

        if (OnDownloadOver != null)
        {
            OnDownloadOver();
        }
    }

    /// <summary>  
    /// 检测本地文件是否存在已经是否是最新  
    /// </summary>  
    /// <param name="AssetName"></param>  
    /// <param name="RootAssetsName"></param>  
    /// <param name="localPath"></param>  
    /// <param name="serverAssetManifestfest"></param>  
    /// <param name="CheckCount"></param>  
    /// <returns></returns>  
    bool CheckLocalFileNeedUpdate(string AssetName, Hash128 hash128Server, string RootAssetsName, string localPath, AssetBundleManifest assetBundleManifestLocal)
    {
        Hash128 hash128Local;
        bool isNeedUpdate = false;
        if (!File.Exists(localPath + "/" + AssetName))
        {
            return true;   //本地不存在,则一定更新  
        }

        if (!File.Exists(localPath + "/" + RootAssetsName))   //当本地依赖信息不存在时,更新  
        {
            isNeedUpdate = true;
        }
        else   //总的依赖信息存在切文件已存在  对比本地和服务器两个文件的Hash值  
        {
            if (hash128Server == new Hash128(0, 0, 0, 0))
            {
                return true;  //保证每次都下载总依赖文件  
            }
            hash128Local = assetBundleManifestLocal.GetAssetBundleHash(AssetName);
            //对比本地与服务器上的AssetBundleHash  版本不一致就下载  
            if (hash128Local != hash128Server)
            {
                isNeedUpdate = true;
            }
        }
        return isNeedUpdate;
    }

    /// <summary>  
    /// 非递归式加载指定AB,并加载依赖项,并返回目标GameObject  
    /// </summary>  
    /// <param name="RootAssetsName"></param>  
    /// <param name="AssetName"></param>  
    /// <param name="LocalPath"></param>  
    public AssetBundle GetLoadAssetFromLocalFile(string RootAssetsName, string AssetName, string PrefabName, string LocalPath)
    {
        AssetBundle assetBundle = AssetBundle.LoadFromFile(LocalPath + "/" + RootAssetsName);
        AssetBundleManifest assetBundleManifest = assetBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");

        string[] AllDependencies = assetBundleManifest.GetAllDependencies(AssetName);

        for (int i = AllDependencies.Length - 1; i >= 0; i--)
        {
            AssetBundle assetBundleDependencies = AssetBundle.LoadFromFile(LocalPath + "/" + AllDependencies[i]);
            assetBundleDependencies.LoadAllAssets();
        }

        AssetBundle assetTarget = AssetBundle.LoadFromFile(LocalPath + "/" + AssetName);
        //return assetTarget.LoadAsset<GameObject>(PrefabName);
        return assetTarget;
    }

    /// <summary>  
    /// 递归加载本地所有依赖项  
    /// </summary>  
    /// <param name="RootAssetsName"></param>  
    /// <param name="AssetName"></param>  
    /// <param name="LocalPath"></param>  
    AssetBundleManifest assetBundleManifestLocalLoad;   //递归加载时候用  
    public void RecursionLoadAssetFromLocalFile(string RootAssetsName, string AssetName, string LocalPath, int RecursionCounter)
    {
        if (RecursionCounter++ == 0)
        {
            //加载本地Manifest获取依赖项  
            assetBundleManifestLocalLoad = AssetBundle.LoadFromFile(LocalPath + "/" + RootAssetsName).LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        }

        //当前AssetName所有依赖项  
        string[] AllDependencies = assetBundleManifestLocalLoad.GetDirectDependencies(AssetName);

        for (int i = 0; i < AllDependencies.Length; i++)
        {
            RecursionLoadAssetFromLocalFile(RootAssetsName, AllDependencies[i], LocalPath, RecursionCounter);
        }

        AssetBundle assetBundle = AssetBundle.LoadFromFile(LocalPath + "/" + AssetName);
        assetBundle.LoadAllAssets();
    }

    /// <summary>  
    /// 将文件模型创建到本地  
    /// </summary>  
    /// <param name="path"></param>  
    /// <param name="name"></param>  
    /// <param name="info"></param>  
    /// <param name="length"></param>  
    void SaveAsset2LocalFile(string path, string name, byte[] info, int length)
    {
        Stream sw = null;
        FileInfo fileInfo = new FileInfo(path + "/" + name);
        if (fileInfo.Exists)
        {
            fileInfo.Delete();
        }

        //如果此文件不存在则创建  
        sw = fileInfo.Create();
        //写入  
        sw.Write(info, 0, length);

        sw.Flush();
        //关闭流  
        sw.Close();
        //销毁流  
        sw.Dispose();

        Debug.Log(name + "成功保存到本地~");
    }

    /// <summary>  
    /// 删除文件  
    /// </summary>  
    /// <param name="path"></param>  
    void DeleteFile(string path)
    {
        File.Delete(path);
    }
}