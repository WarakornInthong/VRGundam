using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScreen : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float intensity = 0.0f;
    [SerializeField] private Color color = Color.black;
    [SerializeField] private Material fadeMaterial = null;
    private LayerSwitcher layerSwitcher;
    public bool loadOnStart = true;
    public float fadeDuration = 2;

    // private void OnRenderImage(RenderTexture src, RenderTexture dest) {
    //     fadeMaterial.SetFloat("_Intensity", intensity);
    //     fadeMaterial.SetColor("_FadeColor", color);
    //     Graphics.Blit(src, dest, fadeMaterial);
    // }

    void Start() {
        layerSwitcher = GetComponent<LayerSwitcher>();
        StartFadeIn();
        if(loadOnStart)
            SceneLoader.Instance.LoadNewScene("scene2");
    }

    public Coroutine StartFadeIn()
    {
        // GetComponent<LayerSwitcher>().SwitchToLoadLayer();
        StopAllCoroutines();
        return StartCoroutine(Fade(0,1));
    }

    private IEnumerator FadeIn()
    {
        while (intensity <= 1.0f)
        {
            intensity += speed * Time.deltaTime;
            yield return null;
        }
    }

    public Coroutine StartFadeOut()
    {
        // GetComponent<LayerSwitcher>().ResetLayer();
        StopAllCoroutines();
        return StartCoroutine(Fade(1,0));
    }

    private IEnumerator FadeOut()
    {
        while (intensity >= 0.0f)
        {
            intensity -= speed * Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator Fade(float alphaIn, float alphaOut){
        fadeMaterial.SetColor("_baseColor", color);
        float timer = 0;
        while(timer <= fadeDuration){
            intensity = Mathf.Lerp(alphaIn, alphaOut, timer / fadeDuration);
            fadeMaterial.SetFloat("_alpha", intensity);
            timer += Time.deltaTime;

            yield return null;
        }
        
        fadeMaterial.SetFloat("_alpha", alphaOut);
    
    }
}
