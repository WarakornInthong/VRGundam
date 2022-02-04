using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameGamemanager : MonoBehaviour
{
    public Vector3 setPosition;

    public void Quit(){
        Application.Quit();
    }

    public void PlayGame(){
        StartCoroutine(LoadMap());
    }

    IEnumerator LoadMap(){
        AsyncOperation operation = SceneManager.LoadSceneAsync("WareHouse", LoadSceneMode.Additive);
        while(!operation.isDone){
            yield return null;
        }
        if(operation.isDone){
            SceneManager.UnloadSceneAsync("StartScene");
            GameObject.Find("XR Origin").transform.position = setPosition;
        }
        

    }
}
