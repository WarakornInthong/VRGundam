using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum CheckMethod{
        Distance = 0,
        Trigger = 1 
    }
public class ScenePartLoader : MonoBehaviour
{
    public Transform player;
    public CheckMethod checkMethod;
    public float loadRange;

    private bool isLoaded = false;
    private bool shouldLoad = false;
    
    void Start()
    {
        isLoaded = IsAlreadyLoaded();
    }

    private bool IsAlreadyLoaded(){
        if(SceneManager.sceneCount > 0){
            for(int i = 0 ; i < SceneManager.sceneCount ; i++){
                Scene scene = SceneManager.GetSceneAt(i);
                if(scene.name == gameObject.name){
                    return true;
                }
            }
            return false;
        }
        else{
            return false;
        }
    }

    void Update()
    {
        if(checkMethod == CheckMethod.Distance){
            DistanceCheck();
        }
        else if(checkMethod == CheckMethod.Trigger){
            TriggerCheck();
        }
    }

    private void DistanceCheck(){
        if(Vector3.Distance(player.position, transform.position) < loadRange){
                LoadScene();
        }
        else{
                UnloadScene();
        }
    }

    private void TriggerCheck(){
        if(shouldLoad){
            LoadScene();
        }
        else{
            UnloadScene();
        }
    }

    private void LoadScene(){
        if(!isLoaded){
            SceneManager.LoadSceneAsync(gameObject.name, LoadSceneMode.Additive);
            isLoaded = true;
        }

    }

    private void UnloadScene(){
        if(isLoaded){
            SceneManager.UnloadSceneAsync(gameObject.name);
            isLoaded = false;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            shouldLoad = true;
        }
    }

    private void OnTriggerExit(Collider other){
        if(other.CompareTag("Player")){
            shouldLoad = false;
        }
    }

}
