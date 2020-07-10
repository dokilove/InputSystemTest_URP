using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlanarReflections : MonoBehaviour
{
    private static Camera _reflectionCamera;

    private void OnEnable()
    {
        RenderPipelineManager.beginCameraRendering += ExecutePlanarReflections;
    }

    private void OnDisable()
    {
        CleanUp();
    }

    private void OnDestroy()
    {
        CleanUp();
    }

    private void CleanUp()
    {
        RenderPipelineManager.beginCameraRendering -= ExecutePlanarReflections;

        if (_reflectionCamera)
        {
            SafeDestroy(_reflectionCamera.gameObject);
        }
    }

    private static void SafeDestroy(Object obj)
    {
        if (Application.isEditor)
        {
            DestroyImmediate(obj);
        }
        else
        {
            Destroy(obj);
        }
    }

    private Camera CreateMirrorObjects()
    {
        GameObject go = new GameObject("Planar Reflections", typeof(Camera));

        Camera reflectionCamera = go.GetComponent<Camera>();
        reflectionCamera.depth = -10f;
        reflectionCamera.enabled = false;

        return reflectionCamera;
    }


    private void ExecutePlanarReflections(ScriptableRenderContext context, Camera camera)
    {
        if (_reflectionCamera == null)
            _reflectionCamera = CreateMirrorObjects();

        UniversalRenderPipeline.RenderSingleCamera(context, _reflectionCamera);
    }
}
