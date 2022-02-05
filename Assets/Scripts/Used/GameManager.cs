using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject menu;
    private InputDevice device;
    private bool menuIsUsed = false;
    private bool isPress;
    void Awake()
    {
        // StartCoroutine(LoadMap());
    }


    void Update()
    {
        device = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        device.TryGetFeatureValue(CommonUsages.menuButton, out bool pressMenu);
        if(pressMenu != isPress){
            // menuIsUsed = !menuIsUsed;
            if(pressMenu && !menuIsUsed){
                OpenMenu();
            }
            else if(pressMenu && menuIsUsed){
                CloseMenu();
            }
            // Debug.Log(isPress);
            isPress = pressMenu;
        }
    }

    IEnumerator LoadMap(){
        AsyncOperation operation = SceneManager.LoadSceneAsync("StartScene", LoadSceneMode.Additive);
        while(!operation.isDone){
            yield return null;
        }
        if(operation.isDone){
            // GameObject.Find("XR Origin").transform.position = new Vector3(0.832f , -28, -0.889f);
            Debug.Log("Start");
        }
        

    }

    public void OpenMenu(){
        menu.GetComponent<Animator>().SetBool("IsUsed", true);
        // Debug.Log("Open");
        menuIsUsed = true;
    }

    public void CloseMenu(){
        menu.GetComponent<Animator>().SetBool("IsUsed", false);
        // Debug.Log("Close");
        menuIsUsed = false;
    }

    public void ExitGame(){
        Application.Quit();
    }
}
