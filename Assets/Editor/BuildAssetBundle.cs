using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class BuildAssetBundle : MonoBehaviour
{
    static string path = Application.streamingAssetsPath + "/AssetBundle";
    static string versionFilePath = path + "/Version.txt";

    //[MenuItem("AssetBundle/Build")]
    static void BuildABs()
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);

        CreateManifest();
    }

    //[MenuItem("AssetBundle/CreateManifest")]
    static void CreateManifest()
    {
        string[] directoryEntries;
        AssetBundle.UnloadAllAssetBundles(true);
        AssetBundle manifestBundle = AssetBundle.LoadFromFile(path+ "/AssetBundle");
        if (manifestBundle != null)
        {
            AssetBundleManifest manifest = (AssetBundleManifest)manifestBundle.LoadAsset("AssetBundleManifest");
            string[] allbundle = manifest.GetAllAssetBundles();
            string fileinfo = "";

            foreach (string assetname in allbundle)
            {
                Hash128 has = manifest.GetAssetBundleHash(assetname);
                fileinfo += assetname + "," + has.ToString() + "\n";
                // Debug.Log(has.GetHashCode().ToString());  

            }

            int v = HandleVersion();

            FileStream fs = new FileStream(path + "/Manifest"+ v +".txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            //开始写入  
            sw.Write(fileinfo);
            //清空缓冲区  
            sw.Flush();
            //关闭流  
            sw.Close();
            fs.Close();

        }
    }
    static int HandleVersion()
    {
        StreamWriter sw;
        int v = 0;
        if (File.Exists(versionFilePath))
        {
            string version = File.ReadAllText(versionFilePath);
            v = int.Parse(version);
            v += 1;
            File.WriteAllText(versionFilePath, v.ToString());
        }
        else
        {
            sw = File.CreateText(versionFilePath);
            v = 1;
            sw.Write(v);
            sw.Close();
        }
        return v;
    }

}