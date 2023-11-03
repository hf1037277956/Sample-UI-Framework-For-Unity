using System;
using System.Collections.Generic;
using UnityEngine;

public class AssetBundleManager : MonoSingleton<AssetBundleManager>
{
    // AB包缓存  解决AB包无法重复加载的问题 也有利于提高效率
    private Dictionary<string, AssetBundle> _abCache;

    // 主包
    private AssetBundle _mainAB = null; 
    
    // 主包中配置文件，用来获取依赖包
    private AssetBundleManifest _mainManifest = null;

    private string basePath
    {
        get
        {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
            return Application.dataPath + "/StreamingAssets/";
#elif UNITY_ANDROID
            return Application.dataPath + "!/assets/";
#endif   
        }
    }

    private string mainABName
    {
        get
        {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
            return "StandaloneWindows";
#elif UNITY_ANDROID
            return "Android";
#endif
        }
    }

    protected override void Init()
    {
        base.Init();
        
        _abCache = new Dictionary<string, AssetBundle>();
    }

    
}