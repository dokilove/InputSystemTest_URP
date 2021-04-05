using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public string addressToAdd;

    private SceneInstance m_LoadedScene;

    private void OnTriggerEnter(Collider other)
    {
        AddScene();
    }

    private void OnTriggerExit(Collider other)
    {
        RemoveScene();
    }

    public void AddScene()
    {
        if (!string.IsNullOrEmpty(addressToAdd))
        {
            Addressables.LoadSceneAsync(addressToAdd, LoadSceneMode.Additive).Completed += OnSceneLoaded;
        }
    }

    public void RemoveScene()
    {
        Addressables.UnloadSceneAsync(m_LoadedScene).Completed += OnSceneUnloaded;
    }

    void OnSceneLoaded(AsyncOperationHandle<SceneInstance> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            m_LoadedScene = obj.Result;
        }
    }

    void OnSceneUnloaded(AsyncOperationHandle<SceneInstance> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            m_LoadedScene = new SceneInstance();
        }
    }
}
