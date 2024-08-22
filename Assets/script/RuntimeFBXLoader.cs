using UnityEngine;
using TriLibCore;
using TriLibCore.General;
using TriLibCore.Interfaces;
using System.Collections;
using System.Collections.Generic;
using TriLibCore.Utils;
using SFB;
using System; // Standalone File Browser

public class RuntimeFBXLoader : MonoBehaviour
{
    public Transform modelParent;  // Parent transform to hold instantiated models
    public static RuntimeFBXLoader Instance { get; private set; }
    private GameObject tempObj;
    public event Action<GameObject> OnModelLoaded;

    private void Awake()
    {
        Instance = this;
    }

    public void OpenFileBrowser()
    {
        var extensions = new[] {
            new ExtensionFilter("3D Model Files", "fbx")
        };
        var paths = StandaloneFileBrowser.OpenFilePanel("Open Model", "", extensions, false);
        if (paths.Length > 0)
        {
            LoadFBXModel(paths[0]);
        }
    }

    public GameObject getTempObj()
    {
        return tempObj;
    }

    private void LoadFBXModel(string path)
    {
        if (System.IO.File.Exists(path))
        {
            var assetLoaderOptions = AssetLoader.CreateDefaultLoaderOptions();
            AssetLoader.LoadModelFromFile(
                path,
                onLoad: OnLoad,
                onMaterialsLoad: OnMaterialsLoad,
                onProgress: OnProgress,
                onError: OnError,
                wrapperGameObject: modelParent.gameObject,
                assetLoaderOptions: assetLoaderOptions
            );
        }
        else
        {
            Debug.LogError("File does not exist: " + path);
        }
    }

    private void OnLoad(AssetLoaderContext assetLoaderContext)
    {
        if (assetLoaderContext.RootGameObject != null)
        {
            tempObj = assetLoaderContext.RootGameObject;
            OnModelLoaded?.Invoke(tempObj);
            //Debug.Log("Model loaded successfully: " + assetLoaderContext.RootGameObject.name);
        }
        else
        {
            Debug.LogError("Model loading failed.");
        }
    }

    private void OnMaterialsLoad(AssetLoaderContext assetLoaderContext)
    {
        
    }

    private void OnProgress(AssetLoaderContext assetLoaderContext, float progress)
    {
        
    }

    private void OnError(IContextualizedError error)
    {
        Debug.LogError("Loading error: " + error.GetInnerException().Message);
    }
}