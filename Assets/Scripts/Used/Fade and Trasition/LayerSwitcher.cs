using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerSwitcher : MonoBehaviour
{
    public LayerMask targetLayer;
    private LayerMask originalLayer;
    public GameObject fadeScreen;
    Camera mainCamera;

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
        originalLayer = Camera.main.cullingMask;
    }



    public void SwitchToLoadLayer()
    {
        fadeScreen.layer = LayerMask.NameToLayer("LoadingMode");
        mainCamera.cullingMask = targetLayer;
        // gameObject.layer = LayerMask.NameToLayer(targetLayer);
    }

    public void ResetLayer()
    {
        fadeScreen.layer = 0;
        mainCamera.cullingMask = originalLayer;
    }
}
