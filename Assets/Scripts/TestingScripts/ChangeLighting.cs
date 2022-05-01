using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class ChangeLighting : MonoBehaviour
{
    public LightingSettings lightSetting;
    public Material skybox;
    public float intensity;
    public bool isTutorial;
    void Start()
    {
        // Debug.Log(Lightmapping.lightingSettings.name);
        // // RenderSettings.skybox = skybox;
        // Debug.Log(RenderSettings.skybox.name);
    }

    // Update is called once per frame
    void Update()
    {
        if(isTutorial){
            
            if(Lightmapping.lightingSettings != lightSetting){
                Lightmapping.lightingSettings = lightSetting;
                Debug.Log("lightingSetting");
            }
            if(RenderSettings.skybox != skybox){
                RenderSettings.skybox = skybox;
                Debug.Log("skybox");
            }
        }
    }
}
